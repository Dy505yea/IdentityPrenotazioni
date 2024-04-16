//from what ya remember
using System.Collections;

public class PrenotazioneViewModel
{
    //public AppUser? appUser{get; set;}
    //the other stuff you need as variable/data

    public Prenotazione Prenotazione {get; set;}
    public List<Prenotazione> Prenotazioni {get; set;}

    //questi mi servono in caso usassi pagine usate da più di un ruolo
    public string? ReturnController {get; set;}
    public string? ReturnUrl {get; set;}
}