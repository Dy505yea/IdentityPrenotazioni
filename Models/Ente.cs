public class Ente
{
    public int Id{get;set;}
    public string? Nome {get;set;}
    public string? Email {get; set;}
    //vengono creati sia una lista di nomi che di eventi in caso il db non volesse registrare più oggetti per diversi enti
    public List<string>? NomiEventi {get; set;}
    public List<Evento>? Eventi{get;set;}
}