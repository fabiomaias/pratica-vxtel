using System;

namespace VxTel.Domain.Common
{
    public abstract class BaseTraceable
    {
        public BaseTraceable(Guid id, DateTime createdAt, DateTime? updatedAt)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        protected BaseTraceable() { }

        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
