using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace IdentityPrenotazioni.Data;

public class Database : IdentityDbContext<AppUser>
{
    //roba a caso
    public DbSet<Prenotazione> Prenotazioni { get; set; } // DbSet rappresenta una tabella del database in memoria tramite il modello Cliente recupera i dati
    public DbSet<Evento> Eventi { get; set; }
    public DbSet<Ente> Enti { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // OnCofiguring imposta le opzioni del database in memoria
    {
        optionsBuilder.UseSqlite("Data Source=Prenotazioni.sqlite"); // UseInMemoryDatabase imposta il database in memoria
    }

    //Area Eventi


    public void ResetDB()
    {
        var cont = Eventi.ToList();
        Eventi.RemoveRange(cont);
        var json = System.IO.File.ReadAllText("wwwroot/json/servizicomposti.json");
        var eventi = JsonConvert.DeserializeObject<List<Evento>>(json)!;
        foreach (var evento in eventi)
        {
            Eventi.Add(evento);
        }
        SaveChanges();
    }
    public bool CheckEventi(List<Evento> eventi)
    {
        //per ora si verifica che gli eventi esistano
        //magari in futuro aggregherò un check per vedere se gli eventi rispettano degli standard
        //o persino se ci sono incongruenze
        if (eventi == null)
        {
            Console.WriteLine("La tabella non esiste");
            return false;
        }
        return true;
    }
    public List<Evento> DammiEventi()
    {
        var eventi = Eventi.ToList();
        return eventi;
    }



    //Area prenotazioni

    public void ResetPrenotazioni()
    {
        var cont = Prenotazioni.ToList();
        Prenotazioni.RemoveRange(cont);
        var json = System.IO.File.ReadAllText("wwwroot/json/prenotazioni.json");
        var prenotazioni = JsonConvert.DeserializeObject<List<Prenotazione>>(json)!;
        int initial = 1;
        foreach (var preno in prenotazioni)
        {
            if (!Prenotazioni.Contains(preno))
            {
                var i = preno.Id;
                if (i == 0)
                {
                    i = IdMancante(initial, prenotazioni);
                    initial = i+1;
                }
                Prenotazioni.Add(new Prenotazione { Id = i, Prenotato = preno.Prenotato, Data = preno.Data, Ora = preno.Ora, Cognome = preno.Cognome, Nome = preno.Nome, Email = preno.Email, NomeEvento = preno.NomeEvento });
            }
        }
        SaveChanges();
    }
    public List<Prenotazione> DammiPrenotazioni()
    {
        var prenotazioni = Prenotazioni.ToList();
        return prenotazioni;
    }
    public int IdMancante(int starter, List<Prenotazione> prenotazioni)
    {
        bool done = false;
        do
        {
            done = true;
            foreach (var prenotazione in prenotazioni)
            {
                if (prenotazione.Id == starter)
                {
                    done = false;
                    starter++;
                }
            }
        }
        while (!done);
        return starter;
    }
    /// <summary>
    /// Per gli utenti: aggiorna una prenotazione con i dati dell'utente e lo rende occupato
    /// </summary>
    /// <param name="idPrenotazione">id della prenotazione da aggiornare</param>
    /// <param name="nome">nome dell'utente richiedente</param>
    /// <param name="cognome">cognome dell'utente richiedente</param>
    /// <param name="email">email dell'utente richiedente</param>
    /// <param name="servizio">nome dell'evento o servizio della prenotazione</param>
    public void PrendiPrenotazione(int idPrenotazione, string nome, string cognome, string email, string servizio)
    {
        //che ricevo? data ed ora della prenotazione.... actually l'id in sè
        //dall'id rintraccio la prenotazione
        var prenotazione=Prenotazioni.FirstOrDefault(p=> p.Id==idPrenotazione);
        prenotazione.Prenotato=true;
        prenotazione.Nome=nome;
        prenotazione.Cognome=cognome;
        prenotazione.Email=email;
        prenotazione.NomeEvento=servizio;
        //pensavo anche di associare l'evento del database...
        var evento=Eventi.FirstOrDefault(e=> e.Nome==servizio);
        prenotazione.Evento=evento;
        SaveChanges();
        //la prendo e l'aggiorno
        //salvo
    }
    public Prenotazione DammiPrenotazione(int id)
    {
        return Prenotazioni.FirstOrDefault(p=>p.Id==id);
    }
    public List<Prenotazione> DammiPrenotazioniUtente(string email)
    {
        return Prenotazioni.Where(p=>p.Email==email).ToList();
    }
    public Prenotazione DammiPrenotazione(string data, string ora)
    {
        return Prenotazioni.FirstOrDefault(p=>p.Data==data&&p.Ora==ora);
    }
    public void AnnullaPrenotazione(int idPrenotazione)
    {
        var prenotazione=Prenotazioni.FirstOrDefault(p=>p.Id==idPrenotazione);
        prenotazione.Prenotato=false;
        prenotazione.Nome=null;
        prenotazione.Cognome=null;
        prenotazione.Email=null;
        prenotazione.NomeEvento=null;
        if(prenotazione.Evento!=null)
            prenotazione.Evento.Id=0;
        prenotazione.Evento=null;
        SaveChanges();
    }
    public void ModificaPrenotazione(int idNuovo, int idVecchio, string nome, string cognome, string servizio, string email)
    {
        //id nuovo è la prenotazione che vien scelta, ognuna è differenziata più per l'orario e l'ente
        var prenotazione=Prenotazioni.FirstOrDefault(p=>p.Id==idNuovo);
        //si mettono i nuovi dati nei campi modificabili
        prenotazione.Nome=nome;
        prenotazione.Cognome=cognome;
        prenotazione.Email=email;
        prenotazione.NomeEvento=servizio;
        prenotazione.Prenotato=true;
        var evento=Eventi.FirstOrDefault(e=> e.Nome==servizio);
        prenotazione.Evento=evento;
        //nel caso l'id della prenotazione modificata è diversa
        //vuol dire che la data e/o ente son diversi, di conseguenza si annulla la prenotazione precedente a questa
        if(idNuovo!=idVecchio)
        {
            AnnullaPrenotazione(idVecchio);
        }
        SaveChanges();
    }
    public void EliminaPrenotazione(int id)
    {
        var prenotazione=Prenotazioni.FirstOrDefault(p=>p.Id==id);
        Prenotazioni.Remove(prenotazione);
        SaveChanges();
    }
}