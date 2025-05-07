namespace Task2.Model
{
    public class AttendancePunch
    {
        public int AttendancePunchId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime PunchTime { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsGeoValidated { get; set; }
        public string Status { get; set; } // OnTime, Late, Early

        public Employee Employee { get; set; }
    }
}
