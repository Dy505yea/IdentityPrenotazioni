using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IdentityPrenotazioni.Models;
using IdentityPrenotazioni.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace IdentityPrenotazioni.Controllers;

public class PrenotazioneController:Controller
{
    private readonly ILogger<UtenteController> _logger;
    private Database Db;

    

	private readonly ApplicationDbContext _context;
	private readonly UserManager<AppUser> _userManager;

    public PrenotazioneController(ILogger<UtenteController> logger, ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        _logger = logger;
		_context = context;
		_userManager = userManager;
    }

    /// <summary>
    /// Metodo per prenotare, i dati necessari verranno processati e si cambierà la prenotazione richiesta
    /// </summary>
    /// <param name="nome">Nome della persona che richiede la prenotazione</param>
    /// <param name="cognome">Cognome della persona che richiede la prenotazione</param>
    /// <param name="email">Email dell'utente</param>
    /// <param name="data">Data completa, giorno e ora attaccati, "|" li separa</param>
    /// <param name="evento">Nome dell'evento o servizio richiesto</param>
    /// <returns>Se trova una prenotazione dalla data, va nella pagina di conferma</returns>
    /// In futuro verrà modificato il funzionamento degli eventi/servizi e entreranno in campo gli enti
    public ActionResult PrenotaPrenotazione(string nome, string cognome, string email, string data, string evento)
    {
        Db=new Database();
        //separo la data in giorno ed ora (il giorno equivale alla data nel modello)
        string giorno; string ora;
        string[] datidata = data.Split('|');
        giorno = datidata[0];
        ora = datidata[1];
        //ritiro la prenotazione tramite la data
        var prenotazione=Db.DammiPrenotazione(giorno, ora);
        if(prenotazione==null)
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        Db.PrendiPrenotazione(prenotazione.Id, nome, cognome, email, evento);
        return View("Conferma", new ConfermaViewModel{ReturnUrl="Index", Message="Prenotazione effettuata con successo", ReturnController="Home"});
    }
    /// <summary>
    /// Metodo per annullare una prenotazione: resettare i dati accessibili all'utente e renderlo disponibile
    /// </summary>
    /// <returns>Reindirizzamento alla conferma</returns>
    public ActionResult AnnullaPrenotazione(int idPrenotazione, string returnUrl, string returnController)
    {
        Db=new Database();
        string email=User.Identity?.Name!;
        Db.AnnullaPrenotazione(idPrenotazione);
        //questo controllo è unicamente fatto in caso fosse un utente a richiamarlo, perchè se è un utente ma non ha altre prenotazioni associate, è un caso speciale
        if(!User.IsInRole("User")||Db.DammiPrenotazioniUtente(email).Any())
            return View("Conferma", new ConfermaViewModel{ReturnUrl=returnUrl, Message="Prenotazione annullata", ReturnController=returnController});
        return View("Conferma", new ConfermaViewModel{ReturnUrl="Index", Message="Prenotazione annullata, non ha altri appuntamenti", ReturnController="Home"});
    }
    public ActionResult ModificaPrenotazione(int idPrenotazione, string returnUrl, string returnController, string nome, string cognome, string email, string data, string evento)
    {
        Db=new Database();
        //separo la data in giorno ed ora (il giorno equivale alla data nel modello)
        string giorno; string ora;
        string[] datidata = data.Split('|');
        giorno = datidata[0];
        ora = datidata[1];
        //ritiro la prenotazione tramite la data
        var prenotazione=Db.DammiPrenotazione(giorno, ora);
        Db.ModificaPrenotazione(prenotazione.Id, idPrenotazione, nome, cognome, evento, email);
        
        return View("Conferma", new ConfermaViewModel{ReturnUrl=returnUrl, Message="Prenotazione aggiornata con successo", ReturnController=returnController});
    }
    [Authorize(Roles = "Admin, Ente")]
    public ActionResult CancellaPrenotazione(int idPrenotazione, string returnUrl, string returnController)
    {
        //similar to annullaPrenotazione, maqui viene rimosso dai dati
        Db=new Database();
        Db.EliminaPrenotazione(idPrenotazione);
        if(Db.DammiPrenotazioni().Any())
            return View("Conferma", new ConfermaViewModel{ReturnUrl=returnUrl, Message="Disponibilità eliminata", ReturnController=returnController});
        return View("Conferma", new ConfermaViewModel{ReturnUrl="Index", Message="Disponibilità eliminata, non son presenti altre disponibilità", ReturnController="Home"});
    }
}