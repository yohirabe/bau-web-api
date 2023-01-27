using System.Text;
using System.Text.Json;
using BAUPatientAPI.Website.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;

namespace BAUPatientAPI.Website.Services
{
    public class JsonFilePatientService
    {
        public string GetNewId ()
        {
            // Subject to change
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        public JsonFilePatientService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "patients.json"); }
        }

        public IEnumerable<Patient> GetPatients()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                var patients = JsonSerializer.Deserialize<Patient[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return patients ?? Enumerable.Empty<Patient>(); // Never returns null
            }
        }

        public Patient? GetPatientById(string id)
        {
            IEnumerable<Patient> patients = GetPatients();
            return patients.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Patient> GetPatientsByIncident(string incident)
        {
            IEnumerable<Patient> patients = GetPatients();
            return patients.Where(p => p.IncidentNumber == incident);
        }

        public Patient AddPatient(string incident, int status, int age, int gender, string[] conditions)
        {
            string newId = GetNewId();

            Patient patient = new Patient(newId, incident, status, age, gender, conditions);
            List<Patient> patients = GetPatients().ToList();
            patients.Add(patient);
            WriteJson(patients.ToArray());
            return patient;
        }

        public bool DeletePatient(string id) 
        {
            // returns true if a patient was removed.
            List<Patient> patients = GetPatients().ToList();
            bool removed = (0 < patients.RemoveAll(p => p.Id == id));
            // I don't know if this is good for a very large json file.
            File.WriteAllText(JsonFileName, null);
            WriteJson(patients.ToArray());
            return removed;
        }

        public IEnumerable<string> GetIncidents()
        {
            HashSet<string> incidents= new HashSet<string>();
            IEnumerable<Patient> patients = GetPatients();
            foreach (Patient patient in patients)
            {
                incidents.Add(patient.IncidentNumber);
            }
            return incidents.ToList();
        }

        public void WriteJson(IEnumerable<Patient> patients)
        {
            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Patient>>(new Utf8JsonWriter(outputStream,
                    new JsonWriterOptions
                    {
                        
                        SkipValidation = true,
                        Indented = true
                        
                    }),
                    patients);
            }
        }
    }
}
