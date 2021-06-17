using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VxTel.Domain.Entities;

namespace VxTel.Application.ViewModels
{
    public class PriceViewModel
    {
        public PriceViewModel(Price price)
        {
            Id = price.Id;
            Origin = price.Origin;
            Destination = price.Destination;
            Charge = price.Charge;
            CreatedAt = price.CreatedAt;
            UpdatedAt = price.UpdatedAt;
        }

        public PriceViewModel() { }

        public Guid Id { get; private set; }

        [DisplayName("DDD de Origem")]
        public string Origin { get; set; }
     
        [DisplayName("DDD de Destino")]
        public string Destination { get; set; }

        [DisplayName("Preço do Minuto")]
        public double Charge { get; set; }

        public DateTime CreatedAt { get; }
        public DateTime? UpdatedAt { get; }
    }
}
