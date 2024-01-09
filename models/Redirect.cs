namespace MyApi.Models
{
    public class UrlRedirect
    {
        public int Id { get; set; }
        public string? OriginalUrl { get; set; }
        public string? RedirectUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}