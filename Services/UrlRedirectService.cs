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
                RedirectUrl = await GenerateRedirectUrlAsync(),
                CreatedAt = DateTime.UtcNow
            };

            _context.UrlRedirects.Add(redirect);
            await _context.SaveChangesAsync();

            return redirect;
        }


        private async Task<string> GenerateRedirectUrlAsync()
        {
            var baseUrl = "http://localhost:5186/redirected/"; // Replace with actual retrieval from config
            var uniqueId = await GenerateUniqueIdAsync();
            var finalUrl = $"{baseUrl}{uniqueId}";
            return finalUrl;
        }


        private async Task<string> GenerateUniqueIdAsync()
        {
            string uniqueId;
            bool isUnique;

            do
            {
                uniqueId = Guid.NewGuid().ToString("N"); // Replace this with your desired generation logic
                isUnique = !await _context.UrlRedirects.AnyAsync(r => r.RedirectUrl.EndsWith(uniqueId));
            } while (!isUnique);

            return uniqueId;
        }

    }
}