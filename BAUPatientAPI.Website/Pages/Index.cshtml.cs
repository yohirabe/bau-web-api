using BAUPatientAPI.Website.Models;
using BAUPatientAPI.Website.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAUPatientAPI.Website.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFilePatientService PatientService;
        public IEnumerable<Patient> Patients { get; private set; }
        public IndexModel(
            ILogger<IndexModel> logger, 
            JsonFilePatientService patientService)
        {
            _logger = logger;
            PatientService = patientService;
        }

        public void OnGet()
        {
            Patients = PatientService.GetPatients();
        }
    }
}