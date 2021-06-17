using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VxTel.Application.Interfaces.Application;
using VxTel.Application.ModelsRequests;

namespace VxTel.WebAPI.Controllers
{
    [Route("api/estimate-prices")]
    [ApiController]
    public class EstimateController : Controller
    {
        private readonly IEstimateApplication _estimateApplication;

        public EstimateController(IEstimateApplication estimateApplication)
        {
            _estimateApplication = estimateApplication;
        }

        /// <summary>
        ///     Estimar preço das chamadas com e sem plano
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EstimateCall(EstimateRequest request) =>
            Ok(await _estimateApplication.EstimatePrice(
                    request.Origin,
                    request.Destination,
                    request.Time,
                    request.PlanId
              ));
    }
}
