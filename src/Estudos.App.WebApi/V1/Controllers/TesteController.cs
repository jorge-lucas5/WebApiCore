using Estudos.App.Business.Interfaces;
using Estudos.App.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Estudos.App.WebApi.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TesteController : MainController
    {
        public TesteController(INotificador notificador, IUser appUser) : base(notificador, appUser)
        {
        }

        public ActionResult<string> Valor()
        {
            return CustomResponse("Api v1");
        }
    }
}