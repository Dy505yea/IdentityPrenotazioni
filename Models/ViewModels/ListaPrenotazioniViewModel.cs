namespace IdentityPrenotazioni.Models;

public class ListaPrenotazioniViewModel
{
    //c'è bisogno di almeno una lista di prenotazioni, solo quello dell'utente richiedente
    //anche di un bool iniziale, in modo da verificare senza mostrare degli errori non voluti
    //per la precisione, il bool è più per evitare incorrere ad un altro problema:
    //il momento in cui l'utente annulla l'ultimo appuntamento che aveva
    //ogni riga avrà anche l'opzione di modificare o annullare una prenotazione, keep in mind this last thing
    public List<Prenotazione>? Prenotazioni {get; set;}
    public bool HoPrenotato {get; set;} //il nome potrebbe cambiare
    //non credo questi sotto mi serviranno
    public string? ReturnUrl{get; set;}
    public string? ReturnController {get; set;}
    public string? Message{get; set;}
}