using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IdentityPrenotazioni.Models;
using IdentityPrenotazioni.Data;
using Microsoft.AspNetCore.Identity;
using IdentityPrenotazioni.Data.Migrations;

namespace IdentityPrenotazioni.Controllers;

public class UtenteController:Controller
{
    private readonly ILogger<UtenteController> _logger;
    private Database Db;

    

	private readonly ApplicationDbContext _context;
	private readonly UserManager<AppUser> _userManager;

    public UtenteController(ILogger<UtenteController> logger, ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        _logger = logger;
		_context = context;
		_userManager = userManager;
    }
    
    //Prova per vedere se ho capito come funzionano le pagine in mvc, probabilmente non son manco rilevanti per il programma in sè
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    //Pagine dedicate all'utente
    public IActionResult ListaPrenotazioni()
    {
        string email=User.Identity?.Name!;
        Db= new Database();
        //prendo le prenotazionidell'utente tramite email
        var prenotazioni=Db.DammiPrenotazioniUtente(email);
        if(!prenotazioni.Any())
            return View("Error", new ErrorViewModel{Message="Lei al momento non ha nessuna prenotazione", ReturnUrl="Index", ReturnController="Home"});
        return View(new ListaPrenotazioniViewModel{Prenotazioni=prenotazioni});
    }
    public IActionResult NuovaPrenotazione()
    {
        //Le varie operazioni verranno effettuate tra il view e prenotazione controller
        return View();
    }

    //Opzioni della pagina ListaPrenotazioni
    public IActionResult ModificaPrenotazione(int id)
    {
        Db=new Database();
        var prenotazione=Db.DammiPrenotazione(id);
        return View(new PrenotazioneViewModel{Prenotazione=prenotazione, ReturnController="Utente", ReturnUrl="ListaPrenotazioni"});
    }
    public IActionResult AnnullaPrenotazione(int id)
    {
        Db=new Database();
        var prenotazione=Db.DammiPrenotazione(id);
        return View(new PrenotazioneViewModel{Prenotazione=prenotazione, ReturnController="Utente", ReturnUrl="ListaPrenotazioni"});
    }

    //prova per vedere se riesco ad aggiungerlo nella conferma
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    /*
    Pagine riguardo agli utenti:
    index=>
        Lista
        [Fatto, ma ora non c'è più l'impaginazione, ritenuta inutile per l'utente]
        =>  
            Modifica di una prenotazione
            Cancellazione di una prenotazione
            =>£
                Conferma
                [Può essere chiamato da chiunque ora]
        Nuova Prenotazione
        [nome cognome e persino email (forse vedrò di evitare di farlo rimettere a sto punto) sono da input
         evento pure dato che per ora gli enti non sono presenti nella funzionalità]
        =>£
            Conferma
            [Può essere chiamato da chiunque ora]


        Yet to be done
        *now*
        Nuova Prenotazione
        =>  (Servizi/Eventi saranno dei piccoli menù a tendina, ogni tendina avrà una lista di enti che offrono X servizio)
            Ente da cui prendere il servizio
            =>  (Ogni ente ha le proprie disponibilità, ma i servizi son intercambiabili)
            =>£
                Conferma
                [Può essere chiamato da chiunque ora]

    */
}