namespace CleanArchitecture.Domin.Features.Shared
{
    public class BaseModel
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
