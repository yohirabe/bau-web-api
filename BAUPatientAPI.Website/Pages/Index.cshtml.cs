using BAUPatientAPI.Website.Contracts;
using BAUPatientAPI.Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAUPatientAPI.Website.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(
            ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
    }
}