namespace Hiring_Date_API.DTOs
{
    public class HiringCreateDto
    {
        public int CompanyId { get; set; }
        public required string Activity { get; set; }
        public DateTime HiringDate { get; set; }
    }
}