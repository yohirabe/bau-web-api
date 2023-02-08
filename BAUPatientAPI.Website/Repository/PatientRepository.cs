using BAUPatientAPI.Website.Context;
using BAUPatientAPI.Website.Contracts;
using BAUPatientAPI.Website.Dto;
using BAUPatientAPI.Website.Models;
using Dapper;
using System.Data;

namespace BAUPatientAPI.Website.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DapperContext _context;

        public PatientRepository(DapperContext context) => _context = context;
        public async Task<IEnumerable<Patient>> GetPatients()
        {
            string query = "SELECT Id, Status, Incident, Estimated_age AS EstimatedAge, Gender, Created, Conditions FROM [dbo].[patients]";

            using (var connection = _context.CreateConnection())
            {
                IEnumerable<Patient> patients = await connection.QueryAsync<Patient>(query);
                return patients.ToList();
            }
        }

        public async Task<Patient> GetPatient(int id)
        {
            string query = "SELECT * FROM [dbo].[patients] WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                Patient patient = await connection.QuerySingleOrDefaultAsync<Patient>(query, new { id });
                return patient;
            }
        }

        public async Task<Patient> CreatePatient(PatientForCreationDto patient)
        {
            string query = "INSERT INTO [dbo].[patients] (Incident, Status, Estimated_age, Gender, Conditions, Created) VALUES (@Incident, @Status, @Estimated_age, @Gender, @Conditions, GETDATE())" +
                "SELECT CAST(SCOPE_IDENTITY() AS int)"; // Returns the last identity value inserted in the same scope.
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Incident", patient.Incident, DbType.Int64);
            parameters.Add("Status", patient.Status, DbType.Int64);
            parameters.Add("Estimated_age", patient.EstimatedAge, DbType.Int64);
            parameters.Add("Gender", patient.Gender, DbType.Int64);
            parameters.Add("Conditions", patient.Conditions, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                int id = await connection.QuerySingleAsync<int>(query, parameters);
                DateTime created = await connection.QuerySingleOrDefaultAsync<DateTime>("SELECT Created FROM [dbo].[patients] WHERE Id = @Id", new { id });
                Patient createdPatient = new Patient
                {
                    Id = id,
                    Incident = patient.Incident,
                    Status = patient.Status,
                    EstimatedAge = patient.EstimatedAge,
                    Gender = patient.Gender,
                    Conditions = patient.Conditions,
                    Created = created
                };
                return createdPatient;
            }
        }
    }
}
