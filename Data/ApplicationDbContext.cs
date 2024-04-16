using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace IdentityPrenotazioni.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public Database Db;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // OnCofiguring imposta le opzioni del database in memoria
    {
        optionsBuilder.UseSqlite("Data Source=Prenotazioni.sqlite"); // UseInMemoryDatabase imposta il database in memoria
    }
    //Il database è stato aggiunto per poi lavorare nel file Prenotazioni.sqlite come feci nell'entity framework
    public ApplicationDbContext()
    {
        Db= new Database();
    }

    //magari sarebbe meglio rivedere come richiamare diverse funzioni (per la maggior parte è proprio dal database)
    public void ControlloEventi()
    {
        var eventi = Db.DammiEventi();
        if (!Db.CheckEventi(eventi) || eventi == null)
        {
            Console.WriteLine("C'è qualche problema con gli eventi, resetto");
            Db.ResetDB();
        }
    }
    public List<Prenotazione> GetPrenotazioniDisponibili()
    {
        var prenotazioni=Db.DammiPrenotazioni();
        return prenotazioni.Where(p=>p.Prenotato==false).ToList();
    }
}
