using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VxTel.Application.Interfaces.Application;
using VxTel.Application.ViewModels;

namespace VxTel.WebAPI.Controllers
{
    [Route("api/plan")]
    [ApiController]
    public class PlanController : Controller
    {
        private readonly IPlanApplication _planApplication;

        public PlanController(IPlanApplication planApplication) => _planApplication = planApplication;

        /// <summary>
        ///     Consultar todos os Planos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _planApplication.GetAll());


        /// <summary>
        ///     Consultar Plano por Id
        /// </summary>
        /// <param name="id">Guid do Plano</param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id) =>
            Ok(await _planApplication.GetById(id));


        /// <summary>
        ///     Cadastrar Plano
        /// </summary>
        /// <param name="planViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(PlanViewModel planViewModel) =>
            Ok(_planApplication.Add(planViewModel));


        /// <summary>
        ///     Atualizar Plano
        /// </summary>
        /// <param name="id">Guid do Plano a ser atualizado</param>
        /// <param name="planViewModel"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PlanViewModel planViewModel) =>
            Ok(await _planApplication.Update(id, planViewModel));


        /// <summary>
        ///     Excluir Plano
        /// </summary>
        /// <param name="id">Guid do Plano a ser excluído</param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task Delete(Guid id) =>
           await _planApplication.Remove(id);
    }
}
