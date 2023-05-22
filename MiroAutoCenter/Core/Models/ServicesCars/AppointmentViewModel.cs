namespace MiroAutoCenter.Core.Models.ServicesCars
{
    public class AppointmentViewModel
    {
        public string DayOfWeek { get; set; }
        public List<AppointmentHour> AvailableHours { get; set; }
        public int ServiceDuration { get; set; }
    }

}
