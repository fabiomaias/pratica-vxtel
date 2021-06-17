using System;
using System.Threading.Tasks;
using VxTel.Application.ViewModels;

namespace VxTel.Application.Interfaces.Application
{
    public interface  IEstimateApplication
    {
        Task<EstimateViewModel> EstimatePrice(string origin, string destination, int time, Guid planId);
    }
}
