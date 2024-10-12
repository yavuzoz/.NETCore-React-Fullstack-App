using Personal_info_API.Model;

namespace Personal_info_API.Dao
{
    public interface IUserDaoImp
    {
        public Task<List<User>> GetUsers(string host);
        public Task<User> AddUser(User u, string host);

        public Task<int> DeleteUserById(int id);

        public Task<int> UpdateUserPhoneNumberById(int id, string phoneNumber);

        public Task<string> SaveImage(IFormFile imageFile);
    }
}
