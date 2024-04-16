namespace IdentityPrenotazioni.Models;

public class ConfermaViewModel
{
    public string? ReturnUrl{get; set;}
    public string? ReturnController {get; set;}
    public string? Message{get; set;}
    //you may need it in case you have to see details of any other users outside you
   // public AppUser? Utente {get; set;}

   //keep in mind, questa classe viene chiamata con minimo un indirizzo per ritorno (nel caso mvc, il nome della pagina da azionare ed il controllore) ed un messaggio per capire che cosa hai compiuto
}