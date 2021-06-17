using System;
using VxTel.Domain.Common;

namespace VxTel.Domain.Entities
{
    public class Price : BaseTraceable
    {
        public Price(Guid id, string origin, string destination, double charge, DateTime createdAt, DateTime? updatedAt) : base(id, createdAt, updatedAt)
        {
            Origin = origin;
            Destination = destination;
            Charge = charge;
        }
        protected Price() { }

        public string Origin { get; private set; }
        public string Destination { get; private set; }
        public double Charge { get; private set; }

        public double CalculateCall(int minutesFromCall) =>
            minutesFromCall * Charge;
    }
}
