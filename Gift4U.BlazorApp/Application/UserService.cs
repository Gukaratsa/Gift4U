using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gift4U.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gift4U.Application.Services
{
    public class UserService
    {
        private readonly GiftDBContext _giftDBContext;
        public User? CurrentUser { get; private set; }
        public bool LoggedIn = false;

        public UserService(GiftDBContext giftDBContext)
        {
            this._giftDBContext = giftDBContext;
            giftDBContext?.Database.Migrate();
        }

        public void AddUser(string FirstName, string LastName, string EmailAddress, string Password)
        {
            if (_giftDBContext.Users.Count(u => u.Email == EmailAddress) > 0)
                throw new ArgumentException($"User with email '{EmailAddress}' already registered");

            _giftDBContext.Users.Add(new User()
            {
                Id = Guid.NewGuid(),
                FirstName = FirstName,
                LastName = LastName,
                Email = EmailAddress,
                Password = Password,
                CreatedDate = DateTime.Now,
                IsEmailConfirmed = false

            });
            _giftDBContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var userToModify = _giftDBContext.Users
                .FirstOrDefault(u => u.Id == user.Id);
            userToModify.FirstName = user.FirstName;
            userToModify.LastName = user.LastName;
            userToModify.Email = user.Email;
            _giftDBContext.SaveChanges();
        }

        public IEnumerable<User> GetUsers()
        {
            return _giftDBContext.Users;
        }

        public void DeleteUser(User user)
        {
            _giftDBContext.Users.Remove(user);
            _giftDBContext.SaveChanges();
        }

        public bool Login(string email, string password)
        {
            if (LoggedIn)
                return LoggedIn;

            CurrentUser = _giftDBContext.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            LoggedIn = CurrentUser != null;
            if (LoggedIn)
                LoginChanged?.Invoke(this, new EventArgs());

            return LoggedIn;
        }

        public void Logout()
        {
            if (!LoggedIn)
                return;

            CurrentUser = null;
            LoggedIn = false;
            LoginChanged?.Invoke(this, new EventArgs());
            
        }

        public event EventHandler LoginChanged;
    }
}
