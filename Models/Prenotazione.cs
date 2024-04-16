public class Prenotazione
{
    //ogni prenotazione ha il suo id, una volta creato non vien più cambiato
    public int Id { get; set; }
    public bool Prenotato { get; set; }
    public string? Data { get; set; }
    public string? Ora { get; set; }
    public string? Cognome { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set;}
    public string? NomeEvento { get; set; }
    public Evento? Evento { get; set; }
    public string? NomeEnte{get;set;}
    //ancora da aggiungere, rimarrà tale finchè gli enti non faranno effettivamente qualcosa
    //public Ente? Ente{get;set;}

    /*
    ciò che contiene una prenotazione:
    id unico, se è preso, data ed ora dell'appuntamento, informazioni di chi l'ha preso, l'evento (sia nome che oggetto), l'ente da cui l'hanno preso
    riguardo all'ente, si può usare la cosa dei ruoli per cercare ed assegnare il nome, non è necessario fare una tabella a parte
    ma il problema verrà sul fatto che ogni ente ha i suoi servizi, cosa che poi poteva combaciare con altri, magari fare una lista dei nomi degli enti
    che offrono le stesse cose, in modo che se si cerca un evento, poi si mostra chi li offre
    ora, è molto probabile che allora si faranno dei menù a tendina per ogni evento, sotto le form delle informazioni, ci sarà
    una griglia di eventi possibili, ogni cella potrà poi mostrare gli enti che offrono la stessa cosa
    solo ora me so recorda che puoi mettere tanti, ora, gli eventi possono benissimo stare molti senza lo skip per evitare di fare il reload
    intanto son quel che sono, magari ce ne saranno massimo na ventina che il sito offrirà, ma da lì ognuno puoi mostra chi li offre
    se riesci, usa entity framework per rimuovere e far altro molto più facilmente nell'area degli eventi, ma per il resto....
    */
}