using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VxTel.Domain.Entities;

namespace VxTel.Application.ViewModels
{
    public class PlanViewModel
    {
        public PlanViewModel(Plan plan)
        {
            Id = plan.Id;
            Name = plan.Name;
            Minutes = plan.Minutes;
            CreatedAt = plan.CreatedAt;
            UpdatedAt = plan.UpdatedAt;
        }

        public PlanViewModel() { }

        public Guid Id { get; private set; }
        [DisplayName("Nome do Plano")]
        public string Name { get; set; }
        [DisplayName("Minutos do Plano")]
        public int Minutes { get; set; }
        public DateTime CreatedAt { get; }
        public DateTime? UpdatedAt { get; }
    }
}
