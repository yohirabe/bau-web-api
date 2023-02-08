using System.Text;
using System.Text.Json;

namespace BAUPatientAPI.Website.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int Incident { get; set; }
        public int Status { get; set; }
        public int EstimatedAge { get; set; }
        public int Gender { get; set; }
        public string Conditions { get; set; } // TODO: Change this back to list of strings
    }
}
