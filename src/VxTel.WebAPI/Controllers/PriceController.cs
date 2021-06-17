using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VxTel.Application.Interfaces.Application;
using VxTel.Application.ViewModels;

namespace VxTel.WebAPI.Controllers
{
    [Route("api/price")]
    [ApiController]
    public class PriceController : Controller
    {
        private readonly IPriceApplication _priceApplication;

        public PriceController(IPriceApplication priceApplication) => _priceApplication = priceApplication;

        /// <summary>
        ///     Consultar todos os Preços
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _priceApplication.GetAll());


        /// <summary>
        ///     Consultar Preço por Id
        /// </summary>
        /// <param name="id">Guid do item de Preço</param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id) =>
            Ok(await _priceApplication.GetById(id));


        /// <summary>
        ///     Cadastrar Preço
        /// </summary>
        /// <param name="priceViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(PriceViewModel priceViewModel) =>
            Ok(_priceApplication.Add(priceViewModel));


        /// <summary>
        ///     Atualizar Preço
        /// </summary>
        /// <param name="id">Guid do Preço a ser atualizado</param>
        /// <param name="priceViewModel"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PriceViewModel priceViewModel) =>
            Ok(await _priceApplication.Update(id, priceViewModel));


        /// <summary>
        ///     Excluir Preço
        /// </summary>
        /// <param name="id">Guid do Preço a ser excluído</param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task Delete(Guid id) =>
           await _priceApplication.Remove(id);
    }
}
