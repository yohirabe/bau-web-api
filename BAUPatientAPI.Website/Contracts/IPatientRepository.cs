using BAUPatientAPI.Website.Dto;
using BAUPatientAPI.Website.Models;

namespace BAUPatientAPI.Website.Contracts
{
    public interface IPatientRepository
    {
        public Task<IEnumerable<Patient>> GetPatients();
        public Task<Patient> GetPatient(int id);
        public Task<IEnumerable<Patient>> GetPatientsByIncident(int incident);
        public Task<IEnumerable<int>> GetUniqueIncidents();
        public Task<Patient> CreatePatient(PatientForCreationDto patient);
        public Task UpdatePatient(int id, PatientForUpdateDto patient);
        public Task DeletePatient(int id);
    }
}
