using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebLanchesMVC.ViewModel;

namespace WebLanchesMVC.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public IActionResult Login(string returnUrl)
		{
			return View(new LoginViewModel() {
				ReturnURL = returnUrl
			});
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginVM)
		{
			if(!ModelState.IsValid)
			{
				return View(loginVM);
			}

			var user = await _userManager.FindByNameAsync(loginVM.UserName);

			if(user != null)
			{
				var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

				if(result.Succeeded)
				{
					if(string.IsNullOrEmpty(loginVM.ReturnURL))
					{
						return RedirectToAction("Index", "Home");
					}
					return RedirectToAction(loginVM.ReturnURL);
				}
			}
			ModelState.AddModelError("", "Usuário/Senha inválidos ou não existe");
			return View(loginVM);
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(LoginViewModel registerVM)
		{
			if(ModelState.IsValid)
			{
				var user = new IdentityUser() { UserName = registerVM.UserName };
				var result = await _userManager.CreateAsync(user, registerVM.Password);

				if(result.Succeeded)
				{
					// Adiciona o usuário padrão ao perfil Member
					await _userManager.AddToRoleAsync(user, "Member");
					await _signInManager.SignInAsync(user, isPersistent: false);

					return RedirectToAction("LoggedIn", "Account");
				}
			}
			return View(registerVM);
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index","Home");
		}

    }
}
