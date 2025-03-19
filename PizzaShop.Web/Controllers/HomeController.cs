// using System.Diagnostics;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using PizzaShop.Domain.DataModels;
// using PizzaShop.Service.Interface;


// namespace PizzaShop.Web.Controllers;

// public class HomeController : Controller
// {


//     private readonly ILogger<HomeController> _logger;

//      private readonly IUserServices _userServices;
//     public HomeController(ILogger<HomeController> logger,IUserServices userServices)
//     {
//         _logger = logger;
//         _userServices = userServices;
//     }

//     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//     public IActionResult Error()
//     {
//         var errorViewModel = new ErrorViewModel
//         {
//             RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
//         };
//         return View(errorViewModel);
//     }

//     public IActionResult Index()
//     {
//         return View();
//     }

//     public IActionResult Privacy()
//     {
//         return View();
//     }

//     public async Task<IActionResult> Dashboard()
//     {
//          var token = Request.Cookies["AuthToken"];
//          var email=ExtractEmailFromToken(token);

//         var user = await _userServices.GetCurrentUserAsync(email);
//         ViewBag.UserName = user?.Username ?? "Guest";

//         return View();
//     }


//     private string ExtractEmailFromToken(string token)
//     {
//         var handler = new JwtSecurityTokenHandler();
//         var jwtToken = handler.ReadJwtToken(token);
//         var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

//         return roleClaim?.Value;
//     }


//     // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//     // public IActionResult Error()
//     // {
//     //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//     // }
// }

using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.DataModels;
using PizzaShop.Service.Interface;


namespace PizzaShop.Web.Controllers;

public class HomeController : Controller
{


    private readonly ILogger<HomeController> _logger;

    private readonly IUserServices _userServices;
    public HomeController(ILogger<HomeController> logger, IUserServices userServices)
    {
        _logger = logger;
        _userServices = userServices;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var errorViewModel = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };
        return View(errorViewModel);
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    private async Task SetUserProfileInViewBag()
    {
        var token = Request.Cookies["AuthToken"];
        var email = _userServices.ExtractEmailFromToken(token);

        if (!string.IsNullOrEmpty(email))
        {
            var userProfile = await _userServices.GetUserProfileAsync(email);
            if (userProfile != null)
            {
                ViewBag.UserName = userProfile.UserName;
                ViewBag.UserImage = userProfile.PathOfProfilePicture ?? "/images/Default_pfp.svg.png";
            }
        }
    }


    // private async Task SetUserProfileInViewBag()
    // {
    //     var token = Request.Cookies["AuthToken"];
    //     var email = ExtractEmailFromToken(token);

    //     if (!string.IsNullOrEmpty(email))
    //     {
    //         var userProfile = await _userServices.GetUserProfileAsync(email);
    //         if (userProfile != null)
    //         {
    //             ViewBag.UserName = userProfile.UserName;
    //             ViewBag.UserImage = userProfile.ProfileImageUrl;
    //         }
    //     }
    // }


    public async Task<IActionResult> Dashboard()
    {
        //  var token = Request.Cookies["AuthToken"];
        //  var email=ExtractEmailFromToken(token);

        // var user = await _userServices.GetCurrentUserAsync(email);
        // ViewBag.UserName = user?.Username ?? "Guest";

        await SetUserProfileInViewBag();

        return View();
    }


    // private string ExtractEmailFromToken(string token)
    // {
    //     var handler = new JwtSecurityTokenHandler();
    //     var jwtToken = handler.ReadJwtToken(token);
    //     var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

    //     return roleClaim?.Value;
    // }


    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}