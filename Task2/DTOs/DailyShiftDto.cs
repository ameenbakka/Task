namespace Task2.DTOs
{
    public class DailyShiftDto
    {
        public DateTime Date { get; set; }
        public string ShiftName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
