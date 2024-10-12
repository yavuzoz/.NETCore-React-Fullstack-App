using Microsoft.EntityFrameworkCore;
using Personal_info_API.Data;
using Personal_info_API.Data.Model;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Personal_info_API.Dao
{
    public class UserDaoImp : IUserDaoImp
    {
        private readonly AppDbContext _context;

        public UserDaoImp(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddUser(User u, string host)
        {
            _context.Users.Add(u);
            await _context.SaveChangesAsync();
            u.Image = host + u.Image;
            return u;
        }

        public async Task<List<User>> GetUsers(string host)
        {
            var users = await _context.Users.ToListAsync();
            foreach (var user in users)
            {
                user.Image = host + user.Image;
            }
            return users;
        }

        public async Task<int> DeleteUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                DeleteImageFromFileSystem(user.Image);
                return 1;
            }
            return 0;
        }

        public async Task<int> UpdateUserPhoneNumberById(int id, string phoneNumber)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.PhoneNumber = phoneNumber;
                await _context.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<string> SaveImage(IFormFile imageFile)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = $"{Path.GetFileNameWithoutExtension(imageFile.FileName)}_{DateTime.Now:yyyyMMddHHmmss}{Path.GetExtension(imageFile.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"Images/{fileName}";
        }

        private void DeleteImageFromFileSystem(string imagePath)
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), imagePath.Replace("Images/", ""));
                if (File.Exists(fullPath))
                {
                    File.SetAttributes(fullPath, FileAttributes.Normal);
                    File.Delete(fullPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while deleting file: {ex.Message}");
            }
        }
    }
}
