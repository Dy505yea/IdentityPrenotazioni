using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser
{
    public string Nome { get; set; }
    //il nome deve essere automaticamente ciò che viene prima della chiocciola, se non viene dato prima
    public string Password { get; set; }
}