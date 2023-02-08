using BAUPatientAPI.Website.Contracts;
using BAUPatientAPI.Website.Dto;
using BAUPatientAPI.Website.Models;
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
        private readonly IPatientRepository _patientRepo;

        public PatientsController(IPatientRepository patientRepo) 
        {
            this._patientRepo = patientRepo;
        }

        [HttpGet()]
        public async Task<IActionResult> GetPatients()
        {
            // TODO: error catching
            var patients = await _patientRepo.GetPatients();
            return Ok(patients);
        }


        [HttpGet("{id}", Name = "PatientById")]
        public async Task<IActionResult> GetPatient(int id)
        {
            Patient patient = await _patientRepo.GetPatient(id);
            if (patient is null)
            {
                return NotFound();
            }
            return Ok(patient);
        }


        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] PatientForCreationDto patient)
        {
            Patient createdPatient = await _patientRepo.CreatePatient(patient);
            return CreatedAtRoute("PatientById", new { id = createdPatient.Id }, createdPatient);
        }
        //[HttpGet("{id}")]
        //public Patient? GetById(string id)
        //{
        //    return PatientService.GetPatientById(id);
        //}

        //[HttpGet("search")]
        //public IEnumerable<Patient> GetByIncident([FromQuery] string incident)
        //{
        //    return PatientService.GetPatientsByIncident(incident);
        //}

        //[HttpGet("incidents")]
        //public IEnumerable<string> GetIncidents()
        //{
        //    return PatientService.GetIncidents();
        //}

        //[HttpPost]
        //public ActionResult AddPatient(JsonObject body)
        //{
        //    // Get arguments from request body.
        //    string incident = (string)body["incident"]!;
        //    int status = (int)body["status"]!;
        //    int age = (int)body["age"]!;
        //    int gender = (int)body["gender"]!;
            
        //    string[] conditions = JsonSerializer.Deserialize<string[]>( body["conditions"]!)!;


        //    Patient patient = PatientService.AddPatient(incident, status ,age, gender, conditions);
        //    return CreatedAtAction("AddPatient" ,patient);
        //}

        //[HttpDelete("{id}")]
        //public ActionResult DeletePatient(string id)
        //{
        //    if (PatientService.DeletePatient(id)) return Ok();
        //    return NotFound();
        //}

    }
}