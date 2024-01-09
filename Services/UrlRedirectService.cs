using MyApi.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace MyApi.Services
{
    public class UrlRedirectService
    {
        private readonly MyApiDbContext _context;

        public UrlRedirectService(MyApiDbContext context)
        {
            _context = context;
        }

        public async Task<UrlRedirect> CreateRedirect(string originalUrl)
        {
            var redirect = new UrlRedirect
            {
                OriginalUrl = originalUrl,
                RedirectUrl = GenerateRedirectUrl(),
                CreatedAt = DateTime.UtcNow
            };

            _context.UrlRedirects.Add(redirect);
            await _context.SaveChangesAsync();

            return redirect;
        }

        private string GenerateRedirectUrl()
        {
            // Implement a method to generate a unique redirect URL.
            // This is a placeholder and should be replaced with your actual URL generation logic.
            return "http://example.com/redirected/" + Guid.NewGuid().ToString();
        }
    }
}