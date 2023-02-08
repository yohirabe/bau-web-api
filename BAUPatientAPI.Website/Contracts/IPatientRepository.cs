using BAUPatientAPI.Website.Dto;
using BAUPatientAPI.Website.Models;

namespace BAUPatientAPI.Website.Contracts
{
    public interface IPatientRepository
    {
        public Task<IEnumerable<Patient>> GetPatients();
        public Task<Patient> GetPatient(int id);
        public Task<Patient> CreatePatient(PatientForCreationDto patient);
    }
}
