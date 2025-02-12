namespace Hiring_Date_API.DTOs
{
    public class HiringDto
    {
        public int HiringId { get; set; }
        public required string Activity { get; set; }
        public required string CompanyName { get; set; }
        public DateTime HiringDate { get; set; }
    }
}