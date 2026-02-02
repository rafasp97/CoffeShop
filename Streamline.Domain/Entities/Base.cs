namespace Streamline.Domain.Entities
{
    public abstract class Base
    {                   
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        
        public bool IsDeleted => DeletedAt.HasValue;

        protected void MarkAsDeleted()
        {
            DeletedAt = DateTime.UtcNow;
        }

        protected void Restore()
        {
            DeletedAt = null;
        }

    }
    
}
