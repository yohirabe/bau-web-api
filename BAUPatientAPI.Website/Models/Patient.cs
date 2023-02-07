using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BAUPatientAPI.Website.Models
{
    public class Patient
    {
        public Patient(string id, string incidentNumber, int statusNumber, int estimatedAge, int gender, string[] conditions)
        {
            Id = id;
            CreatedAt = DateTime.Now;
            IncidentNumber = incidentNumber;
            StatusNumber = statusNumber;
            EstimatedAge = estimatedAge;
            Gender = gender;
            Conditions = conditions;
        }

        public string Id { get; set; }

        [JsonPropertyName("created")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("incident")]
        public string IncidentNumber { get; set; }
        [JsonPropertyName("status")]
        public int StatusNumber { get; set; }
        [JsonPropertyName("age")]
        public int EstimatedAge { get; set; }
        public int Gender { get; set; }
        public string[] Conditions { get; set; }


        public override string ToString() => JsonSerializer.Serialize<Patient>(this);
    }
}
