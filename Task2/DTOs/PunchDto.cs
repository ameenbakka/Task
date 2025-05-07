namespace Task2.DTOs
{
    public class PunchDto
    {
        public int EmployeeId { get; set; }
        public DateTime PunchTime { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
