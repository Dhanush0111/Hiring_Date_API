namespace Hiring_Date_API.Models
{
    public class Hiring
    {
        public int HiringId {  get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public int CompanyId {  get; set; } //Foreign Key

        public string Activity {  get; set; } = string.Empty;

        public virtual Company? Company_CompanyId { get; set; }

    }
}
