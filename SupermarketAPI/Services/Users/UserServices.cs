using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SupermarketAPI.DTOs;
using SupermarketAPI.Models;

namespace SupermarketAPI.Services.Users
{
    public class UserServices : IUserServices
    {
        private readonly SupermarketDbContext _db;
        private readonly IMapper _mapper;

        public UserServices(SupermarketDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> DeleteUser(int userId)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user == null)
                return -1;

            _db.Users.Remove(user);

            return await _db.SaveChangesAsync();
        }

        public async Task<UserResponse> GetUser(int userId)
        {
            var user = await _db.Users.FindAsync(userId);
            var userResponse = _mapper.Map<User, UserResponse>(user);

            return userResponse;
        }

        public async Task<List<UserResponse>> GetUsers()
        {
            var users = await _db.Users.ToListAsync();
            var usersList = _mapper.Map<List<User>, List<UserResponse>>(users);

            return usersList;
        }

        public async Task<UserResponse> Login(UserRequest user)
        {
            var userEntity = await _db.Users.FirstOrDefaultAsync(
                    o=> o.Username == user.Username 
                    && o.UserPassword == user.UserPassword
                );

            var userResponse = _mapper.Map<User, UserResponse>(userEntity); 

            return userResponse;
        }

        public async Task<int> PostUser(UserRequest user)
        {
            var entity = _mapper.Map<UserRequest, User>(user);

            await _db.Users.AddAsync(entity);

            return await _db.SaveChangesAsync();
        }

        public async Task<int> PutUser(int userId, UserRequest user)
        {
            var entity = await _db.Users.FindAsync(userId);
            if (entity == null)
                return -1;

            entity.Username = user.Username;
            entity.UserPassword = user.UserPassword;
            entity.UserRole = user.UserRole;

            _db.Users.Update(entity);

            return await _db.SaveChangesAsync();
        }
    }
}
