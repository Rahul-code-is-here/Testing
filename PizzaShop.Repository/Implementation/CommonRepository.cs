// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// // using BussinessLogicLayer.Interface;

// // using DataAccessLayer.Models;
// using PizzaShop.Domain.DataModels;
// // using DataAccessLayer.ViewModels;?
// using PizzaShop.Domain.ViewModels;
// using Microsoft.EntityFrameworkCore;
// using PizzaShop.Repository.Interfaces;
// using PizzaShop.Domain.DataContext;

// namespace PizzaShop.Repository.Implementations;

// public class CommonRepository : ICommonRepository
// {
//     private readonly PizzaShemaContext _db;
//     public CommonRepository(PizzaShemaContext db)
//     {
//         _db = db;
//     }
//     // Retrieves a list of all countries.
//     public async Task<List<DropDownViewModel>> GetCountriesAsync()
//     {
//         var countries = await _db.Countries.Select(c => new DropDownViewModel()
//         {
//             Text = c.Countryname,
//             Value = c.Countryid.ToString()
//         }).ToListAsync();
//         return countries;
//     }
    
//     //Retrieves a list of states based on the provided country ID.
//     public async Task<List<DropDownViewModel>> GetStatesAsync(int countryId)
//     {
//         var states = await _db.States.Where(s => s.Countryid == countryId)
//                                     .Select(s => new DropDownViewModel()
//                                     {
//                                         Text = s.Statename,
//                                         Value = s.Stateid.ToString()
//                                     }).ToListAsync();
//         return states;
//     }
    
//     //Retrieves a list of cities based on the provided state ID.
//     public async Task<List<DropDownViewModel>> GetCitiesAsync(int stateId)
//     {
//         var cities = await _db.Cities.Where(u => u.Stateid == stateId)
//                                     .Select(a => new DropDownViewModel()
//                                     {
//                                         Text = a.Cityname,
//                                         Value = a.Cityid.ToString()
//                                     }).ToListAsync();
//         return cities;
//     }
    
//     //Retrieves a list of all roles available in the system
//     public async Task<List<DropDownViewModel>> GetRolesAsync()
//     {
//         var roles = await _db.Roles.Select(r => new DropDownViewModel()
//         {
//             Text = r.Rolename,
//             Value = r.Roleid.ToString()
//         }).ToListAsync();
//         return roles;
//     }
    
//     // Validates a given JWT token and extracts user details from it.
//     public async Task<TokenViewModel> ValidateToken(string jwtToken)
//     {
//         TokenViewModel token = new TokenViewModel();
//         try
//         {

//             var handler = new JwtSecurityTokenHandler();
//             var JwtToken = handler.ReadJwtToken(jwtToken);

//             Console.WriteLine(JwtToken.ValidTo);
//             if (JwtToken.ValidTo < DateTime.UtcNow)
//             {
//                 token.Valid = false;
//                 return token;
//             }

//             token.Role = JwtToken.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Role)?.Value ?? "Guest";
//             token.UserEmail = JwtToken.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value ?? "unknown";
//             token.UserName = JwtToken.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value ?? "Guest";
//             token.UserId = JwtToken.Claims.FirstOrDefault(p=>p.Type==ClaimTypes.Sid)?.Value??"";
//             token.Profilepic = JwtToken.Claims.FirstOrDefault(p=>p.Type==ClaimTypes.UserData)?.Value??"";
//             token.Valid = true;
//             return token;
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine("error: while validating try again later");
//             return token;
//         }
//     }

//     public string getCurrentUserImage(string email)
//     {
//         var img = _db.Users.FirstOrDefault(u=>u.Email==email);
//         return img.Profilepic;
//     }
 
// }
