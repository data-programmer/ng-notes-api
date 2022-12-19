using System.Threading.Tasks;
using NGNotesAPI.Models;

namespace NGNotesAPI.Services
{
    public interface IUserService
    {
        public Task<UserModel> SignIn(string username);

        public Task<UserModel> RegisterUser(UserModel user);

        public Task<UserModel> GetUser(int UserId);

        public UserModel UpdateUser(UserModel user);

        public UserModel DeleteUser(int UserId);
    }
}
