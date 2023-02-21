namespace BAUPatientAPI.Website.Dto
{
    public class PatientForUpdateDto
    {
        public int Incident { get; set; }
        public int Status { get; set; }
        public int EstimatedAge { get; set; }
        public int Gender { get; set; }
        public string Conditions { get; set; }
    }
}
