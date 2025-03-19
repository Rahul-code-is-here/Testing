

using Microsoft.EntityFrameworkCore;
using PizzaShop.Domain.DataContext;
using PizzaShop.Domain.DataModels;
using PizzaShop.Domain.ViewModels;
using PizzaShop.Repository.Interface;
using System.Linq;
using System.Linq.Expressions;

namespace PizzaShop.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly PizzaShemaContext _context;

        public UserRepository(PizzaShemaContext context)
        {
            _context = context;
        }



        public User Get(Expression<Func<User, bool>> predicate)
        {
            return _context.Users.FirstOrDefault(predicate);
        }

        public async Task<User> GetEmailAndPassword(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password );
        }

        // public async Task<User> GetEmailAndPassword(string email, string password)
        // {
        //     var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsDeleted==false && u.Status == "Active");
        //     if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
        //     {
        //         return user;
        //     }
        //     return null;
        // }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public IQueryable<User> GetUsers()
        {
            return _context.Users.AsQueryable();
        }

        public IQueryable<Country> GetCountries()
        {
            return _context.Countries.AsQueryable();
        }

        public IQueryable<State> GetStates(int countryId)
        {
            return _context.States.Where(s => s.CountryId == countryId).AsQueryable();
        }

        public IQueryable<City> GetCities(int stateId)
        {
            return _context.Cities.Where(c => c.StateId == stateId).AsQueryable();
        }


        public async Task<List<UserListModel>> GetUsersAsync(Expression<Func<User, bool>> predicate, int pageNumber, int pageSize, string sortBy, string sortOrder)
        {
            var query = _context.Users
                .Where(predicate)
                .Select(u => new UserListModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Phone = u.Phone,
                    RoleId = u.RoleId,
                    ProfileImagePath = u.ProfileImage,
                    IsDeleted = u.IsDeleted,
                    status = u.Status
                });

            // Sorting Logic
            query = sortBy switch
            {
                "Email" => (sortOrder == "asc") ? query.OrderBy(u => u.Email) : query.OrderByDescending(u => u.Email),
                "Phone" => (sortOrder == "asc") ? query.OrderBy(u => u.Phone) : query.OrderByDescending(u => u.Phone),
                "Role" => (sortOrder == "asc") ? query.OrderBy(u => u.RoleId) : query.OrderByDescending(u => u.RoleId),
                _ => (sortOrder == "asc") ? query.OrderBy(u => u.FirstName).ThenBy(u => u.LastName) : query.OrderByDescending(u => u.FirstName).ThenByDescending(u => u.LastName)
            };

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalUsersAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.CountAsync(predicate);
        }

        public async Task<bool> SoftDeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<EditUserModel> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return null;
            // var status1 = user.Status;
            // if (status1 == "1" || status1 == "Active")
            // {
            //     status1 = "Active";
            // }
            // if(status1 == "2" || status1 == "Inactive")
            // {
            //     status1 = "Inactive";
            // }
            return new EditUserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Username,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId,
                CountryID = user.CountryId,
                StateID = user.StateId,
                CityID = user.CityId,
                Address = user.Adress,
                Phone = user.Phone,
                Zipcode = user.Zipcode,
                status = user.Status,
                /////
                PathOfProfilePicture = user.ProfileImage,
                Countries = await _context.Countries.ToListAsync(),
                States = await _context.States.Where(s => s.CountryId == user.CountryId).ToListAsync(),
                Cities = await _context.Cities.Where(c => c.StateId == user.StateId).ToListAsync()
            };
        }

        public async Task<bool> UpdateUserAsync(EditUserModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.Id);
            if (user == null) return false;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Username = model.UserName;
            user.Email = model.Email;

            if (!string.IsNullOrEmpty(model.Password))
            {
                user.Password = model.Password;
            }

            user.RoleId = model.RoleId;
            user.CountryId = model.CountryID;
            user.StateId = model.StateID;
            user.CityId = model.CityID;
            user.Adress = model.Address;
            // user.Password=model.Password;
            user.Phone = model.Phone;
            user.Zipcode = model.Zipcode;
            user.Status=model.status;
            user.ProfileImage = model.PathOfProfilePicture;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<List<State>> GetStatesByCountryIdAsync(int countryId)
        {
            return await _context.States.Where(s => s.CountryId == countryId).ToListAsync();
        }

        public async Task<List<City>> GetCitiesByStateIdAsync(int stateId)
        {
            return await _context.Cities.Where(c => c.StateId == stateId).ToListAsync();
        }



        public async Task<User> GetUserByIdAsyncs(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> isFirstLogin(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Isfirstlogin == true);
            if (user == null) return false;

            // var result= _context.Users.Where(u => u.Email == email && u.IsFirstLogin == true).FirstOrDefault();
            user.Isfirstlogin = false;
            await _context.SaveChangesAsync();

            return true;
        }

        // public async Task<(List<TViewModel> Items, int TotalCount)> GetPaginatedListAsync<TEntity, TViewModel>(
        // Expression<Func<TEntity, bool>> predicate,
        // int pageNumber,
        // int pageSize,
        // string sortBy,
        // string sortOrder,
        // Expression<Func<TEntity, TViewModel>> selector)
        // where TEntity : class
        // {
        //     var query = _context.Set<TEntity>().Where(predicate);

        //     // Sorting Logic
        //     query = sortBy switch
        //     {
        //         "Email" => (sortOrder == "asc") ? query.OrderBy(e => EF.Property<object>(e, sortBy)) : query.OrderByDescending(e => EF.Property<object>(e, sortBy)),
        //         "Phone" => (sortOrder == "asc") ? query.OrderBy(e => EF.Property<object>(e, sortBy)) : query.OrderByDescending(e => EF.Property<object>(e, sortBy)),
        //         "Role" => (sortOrder == "asc") ? query.OrderBy(e => EF.Property<object>(e, sortBy)) : query.OrderByDescending(e => EF.Property<object>(e, sortBy)),
        //         _ => (sortOrder == "asc") ? query.OrderBy(e => EF.Property<object>(e, sortBy)) : query.OrderByDescending(e => EF.Property<object>(e, sortBy))
        //     };

        //     var totalCount = await query.CountAsync();

        //     var items = await query
        //         .Skip((pageNumber - 1) * pageSize)
        //         .Take(pageSize)
        //         .Select(selector)
        //         .ToListAsync();

        //     return (items, totalCount);
        // }

    }
}