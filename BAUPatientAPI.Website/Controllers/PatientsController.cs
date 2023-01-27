using BAUPatientAPI.Website.Models;
using BAUPatientAPI.Website.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace BAUPatientAPI.Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        public PatientsController(JsonFilePatientService patientService)
        {
            this.PatientService = patientService;
        }

        public JsonFilePatientService PatientService { get; }

        [HttpGet()]
        public IEnumerable<Patient> Get()
        {
            return PatientService.GetPatients();
        }

        [HttpGet("{id}")]
        public Patient? GetById(string id)
        {
            // returns null if patient wasn't found.
            return PatientService.GetPatientById(id);
        }

        [HttpGet("search")]
        public IEnumerable<Patient> GetByIncident([FromQuery] string incident)
        {
            return PatientService.GetPatientsByIncident(incident);
        }

        [HttpGet("incidents")]
        public IEnumerable<string> GetIncidents()
        {
            // Gets all unique incident numbers from patient database.
            return PatientService.GetIncidents();
        }

        [HttpPost]
        public ActionResult AddPatient(JsonObject body)
        {
            // Get arguments from request body.
            string incident = (string)body["incident"]!;
            int status = (int)body["status"]!;
            int age = (int)body["age"]!;
            int gender = (int)body["gender"]!;
            
            string[] conditions = JsonSerializer.Deserialize<string[]>( body["conditions"]!)!;


            Patient patient = PatientService.AddPatient(incident, status ,age, gender, conditions);
            return CreatedAtAction("AddPatient" ,patient);
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePatient(string id)
        {
            if (PatientService.DeletePatient(id)) return Ok();
            return NotFound();
        }

    }
}