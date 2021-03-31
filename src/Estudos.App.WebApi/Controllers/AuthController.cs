using System.Linq;
using System.Threading.Tasks;
using Estudos.App.Business.Interfaces;
using Estudos.App.WebApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Estudos.App.WebApi.Controllers
{
    [Route("api/conta")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(INotificador notificador,
                            SignInManager<IdentityUser> signInManager,
                            UserManager<IdentityUser> userManager) : base(notificador)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return CustomResponse(registerUser);
            }

            result.Errors.ToList()
                .ForEach(a=> NotificarErro(a.Description));

            return CustomResponse();
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse();
            }
            else if (result.IsLockedOut)
            {
                NotificarErro("Usuário bloqueado temporáriamente por excesso de tentaivas inválidas");
            }
            else
            {
                NotificarErro("Usuário ou senha incorretos");
            }
           
            return CustomResponse();
        }
    }
}