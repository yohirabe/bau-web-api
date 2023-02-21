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

        [HttpGet("search", Name = "PatientsByIncident")]
        public async Task<IActionResult> GetPatientsByIncident([FromQuery] int incident)
        {
            var patients = await _patientRepo.GetPatientsByIncident(incident);
            return Ok(patients);
        }

        [HttpGet("incidents", Name = "UniqueIncidents")]
        public async Task<IActionResult> GetUniqueIncidents()
        {
            var incidents = await _patientRepo.GetUniqueIncidents();
            return Ok(incidents);
        }


        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] PatientForCreationDto patient)
        {
            Patient createdPatient = await _patientRepo.CreatePatient(patient);
            return CreatedAtRoute("PatientById", new { id = createdPatient.Id }, createdPatient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] PatientForUpdateDto patient)
        {
            var dbPatient = await _patientRepo.GetPatient(id);
            if (dbPatient is null) return NotFound();
            await _patientRepo.UpdatePatient(id, patient);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var dbPatient = await _patientRepo.GetPatient(id);
            if (dbPatient is null) return NotFound();
            await _patientRepo.DeletePatient(id);
            return NoContent();
        }
    }
}