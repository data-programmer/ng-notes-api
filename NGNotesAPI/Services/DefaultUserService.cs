using AutoMapper;
using NGNotesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace NGNotesAPI.Services
{
    public class DefaultUserService : IUserService
    {
        private readonly NGNotesApiDBContext _context;
        private readonly IMapper _mapper;

        public DefaultUserService(
            NGNotesApiDBContext context,
            IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserModel> SignIn(string username)
        {
            var entity = await _context.User
                .Where(x => x.Username == username)
                .SingleOrDefaultAsync();

            if (entity == null)
            {
                return null;
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<UserModel>(entity);
        }

        public async Task<UserModel> RegisterUser(UserModel user)
        {
            // Register the user
            var entity = await _context.User.AddAsync(user);

            if (entity == null)
            {
                return null;
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<UserModel>(entity.Entity);
        }

        public async Task<UserModel> GetUser(int UserId)
        {
            var entity = await _context.User
                .SingleOrDefaultAsync(x => x.Id == UserId);

            if (entity == null)
            {
                return null;
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<UserModel>(entity);
        }

        public UserModel UpdateUser(UserModel user)
        {
            var entity = _context.User
                .Update(user);

            if (entity == null)
            {
                return null;
            }

            _context.SaveChanges();

            return _mapper.Map<UserModel>(entity.Entity);
        }

        public UserModel DeleteUser(int UserId)
        {
            var entity = _context.User
                .Where(x => x.Id == UserId)
                .SingleOrDefault();

            var result = _context.User.Remove(entity);

            if (result == null)
            {
                return null;
            }

            _context.SaveChanges();

            return _mapper.Map<UserModel>(result.Entity);
        }       
    }
}