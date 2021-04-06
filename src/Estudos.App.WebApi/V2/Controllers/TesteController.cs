using System;
using Estudos.App.Business.Interfaces;
using Estudos.App.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Estudos.App.WebApi.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TesteController : MainController
    {
        private readonly ILogger _logger;
        public TesteController(INotificador notificador, IUser appUser, ILogger<TesteController> logger) : base(notificador, appUser)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> Valor()
        {
            throw new Exception("teste de erro "+ DateTime.Now);
            _logger.LogWarning("Log de Aviso");
            _logger.LogError("Log de Erro");
            _logger.LogCritical("Log de Problema Critico");
            return CustomResponse("Api v2");
        }
    }
}