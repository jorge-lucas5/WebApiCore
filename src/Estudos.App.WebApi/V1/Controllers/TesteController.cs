﻿using Estudos.App.Business.Interfaces;
using Estudos.App.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Estudos.App.WebApi.V1.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TesteController : MainController
    {
        public TesteController(INotificador notificador, IUser appUser) : base(notificador, appUser)
        {
        }

        [HttpGet]
        public ActionResult<string> Valor()
        {
            return CustomResponse("Api v1");
        }
    }
}