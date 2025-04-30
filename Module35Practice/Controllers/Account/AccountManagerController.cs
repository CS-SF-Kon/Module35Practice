using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Module35Practice.Data.Repository;
using Module35Practice.Data.UoW;
using Module35Practice.Models.Users;
using Module35Practice.ViewModels.Account;

namespace Module35Practice.Controllers.Account;

public class AccountManagerController : Controller
{
    private IMapper _mapper;

    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private IUnitOfWork _unitOfWork;

    public AccountManagerController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [Route("Login")]
    [HttpGet]
    public IActionResult Login()
    {
        return View("Home/Login");
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [Authorize]
    [Route("MyPage")]
    [HttpGet]
    public async Task<IActionResult> MyPage()
    {
        var user = User;

        var result = await _userManager.GetUserAsync(user);

        var model = new UserViewModel(result);

        model.Friends = await GetAllFriend(model.User);

        return View("User", model);
    }

    private async Task<List<User>> GetAllFriend(User user)
    {
        var repository = _unitOfWork.GetRepository<Friend>() as FriendRepository;

        return repository.GetFriendsByUser(user);
    }

    private async Task<List<User>> GetAllFriend()
    {
        var user = User;

        var result = await _userManager.GetUserAsync(user);

        var repository = _unitOfWork.GetRepository<Friend>() as FriendRepository;

        return repository.GetFriendsByUser(result);
    }

    [Route("Edit")]
    [HttpGet]
    public IActionResult Edit()
    {
        var user = User;

        var result = _userManager.GetUserAsync(user);

        var editmodel = _mapper.Map<UserEditViewModel>(result.Result);

        return View("Edit", editmodel);
    }

    [Authorize]
    [Route("Update")]
    [HttpPost]
    public async Task<IActionResult> Update(UserEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            //user.Convert(model);

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("MyPage", "AccountManager");
            }
            else
            {
                return RedirectToAction("Edit", "AccountManager");
            }
        }
        else
        {
            ModelState.AddModelError("", "Некорректные данные");
            return View("Edit", model);
        }
    }

    [Route("Login")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {

            var user = _mapper.Map<User>(model);

            var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
        }
        return RedirectToAction("Index", "Home");
    }

    [Route("Logout")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
