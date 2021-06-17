using System;
using VxTel.Domain.Common;

namespace VxTel.Domain.Entities
{
    public class Plan : BaseTraceable
    {
        public Plan(Guid id, string name, int minutes, DateTime createdAt, DateTime? updatedAt) : base(id, createdAt, updatedAt)
        {
            Name = name;
            Minutes = minutes;
        }

        protected Plan() { }

        public string Name { get; private set; }
        public int Minutes { get; private set; }

        public double CalculateCall(Price price, int minutesFromCall) =>
            Minutes > minutesFromCall ? 0 : (minutesFromCall - Minutes) * (price.Charge + (price.Charge * 0.1));
    }
}
