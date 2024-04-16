namespace IdentityPrenotazioni.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    //ciò che è sovvrastante non verrà usato, ma rimarrà in caso mi serva
    public string? Message {get; set;}
    public string? ReturnUrl {get; set;}
    public string? ReturnController {get; set;}
}
