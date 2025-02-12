namespace Hiring_Date_API.DTOs
{
    public class HiringUpdateDto
    {
        public int CompanyId { get; set; }
        public required string Activity { get; set; }

        public DateTime HiringDate { get; set; }
    }
}
