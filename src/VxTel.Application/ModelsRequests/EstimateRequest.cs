using System;
using System.ComponentModel.DataAnnotations;

namespace VxTel.Application.ModelsRequests
{
    public class EstimateRequest
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int Time { get; set; }
        public Guid PlanId { get; set; }
    }
}
