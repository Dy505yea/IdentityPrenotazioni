using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IdentityPrenotazioni.Models;
using IdentityPrenotazioni.Data;
using Microsoft.AspNetCore.Identity;
using IdentityPrenotazioni.Data.Migrations;
using Microsoft.AspNetCore.Authorization;

namespace IdentityPrenotazioni.Controllers;

public class AdminController:Controller
{
    private readonly ILogger<AdminController> _logger;
    private Database Db;

    

	private readonly ApplicationDbContext _context;
	private readonly UserManager<AppUser> _userManager;
    

    public AdminController(ILogger<AdminController> logger, ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        _logger = logger;
		_context = context;
		_userManager = userManager;
        Db=_context.Db;
    }

    [Authorize(Roles = "Admin, Ente")]
    public IActionResult ListaPrenotazioni()
    {
        Db= new Database();
        var prenotazioni=Db.DammiPrenotazioni();
        if(!prenotazioni.Any())
            return View("Error", new ErrorViewModel{Message="Non son presenti prenotazioni", ReturnUrl="Index", ReturnController="Home"});
        return View(new ListaPrenotazioniViewModel{Prenotazioni=prenotazioni});
    }
    public IActionResult ModificaPrenotazione(int id)
    {
        Db=new Database();
        var prenotazione=Db.DammiPrenotazione(id);
        return View(new PrenotazioneViewModel{Prenotazione=prenotazione, ReturnController="Admin", ReturnUrl="ListaPrenotazioni"});
    }
    //cancellare una disponibilità è una cosa molto importante, meglio evitare a tutti i cost che l'utente acceda a quest'area
    [Authorize(Roles = "Admin, Ente")]
    public IActionResult CancellaDisponibilita(int id)
    {
        Db=new Database();
        var prenotazione=Db.DammiPrenotazione(id);
        return View(new PrenotazioneViewModel{Prenotazione=prenotazione, ReturnController="Admin", ReturnUrl="ListaPrenotazioni"});
    }

    //stessa cosa vale per la disponibilità, ma prbabilmente l'ente lo gestirà differentemente da come l'admin farà
    [Authorize(Roles = "Admin, Ente")]
    public IActionResult AggiungiDisponibilita()
    {
        var oggi=DateTime.Today;
        string oggiGiorno=oggi.Day.ToString("00");
        string oggiMese=oggi.Month.ToString("00");
        string oggiAnno=oggi.Year.ToString();
        //oggi avrà il formato che il view necessita: "YYY-MM-dd"
        ViewData["oggi"]=oggiAnno+"-"+oggiMese+"-"+oggiGiorno;
        //il momento in cui ente entra in scena, verrà aggiunto anche un modo per mettere l'ente
        //ma di per sè. non deve essere una chiave troppo fondamentale per le prenotazioni
        return View();
    }
    public IActionResult AnnullaPrenotazione(int id)
    {
        Db=new Database();
        var prenotazione=Db.DammiPrenotazione(id);
        return View(new PrenotazioneViewModel{Prenotazione=prenotazione, ReturnController="Admin", ReturnUrl="ListaPrenotazioni"});
    }
}