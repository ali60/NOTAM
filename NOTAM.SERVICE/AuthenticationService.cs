using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NOTAM.SERVICE
{
    public interface IAuthenticationService
    {
        User AuthenticateUser(string username, string password);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private class InternalUserData
        {
            public InternalUserData(string username, string email, string hashedPassword, string[] roles)
            {
                Username = username;
                Email = email;
                HashedPassword = hashedPassword;
                Roles = roles;
            }
            public string Username
            {
                get;
                private set;
            }

            public string Email
            {
                get;
                private set;
            }

            public string HashedPassword
            {
                get;
                private set;
            }

            public string[] Roles
            {
                get;
                private set;
            }
        }


        #region Fields

        readonly List<User> _users;

        private NotamDataContext _notamDataContext;

        private UserDataContext _dataContext;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Creates a new repository of origins.
        /// </summary>
        /// <param name="originDataFile">The relative path to an XML resource file that contains origin data.</param>
        public AuthenticationService(UserDataContext dataContext)
        {
            _dataContext = dataContext;
            _users = LoadUsers(dataContext);
        }


        public AuthenticationService(NotamDataContext dataContext)
        {
            _notamDataContext = dataContext;
            _users = LoadUsers(dataContext);
        }

        #endregion // Constructor


        #region Public Interface

        /// <summary>
        /// Raised when a user is placed into the repository.
        /// </summary>
        public event EventHandler<EntityAddedEventArgs<User>> UserAdded;
        public event EventHandler<EntityAddedEventArgs<User>> UserDeleted;
        /// <summary>
        /// Places the specified origin into the repository.
        /// If the origin is already in the repository, an
        /// exception is not thrown.
        /// </summary>
        public void Insert(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (!_users.Contains(user))
            {
                user.HashPassword = CalculateHash(user.Password, user.Username);
                _users.Add(user);
                _notamDataContext.Users.InsertOnSubmit(user);
                _notamDataContext.SubmitChanges();
                if (this.UserAdded != null)
                    this.UserAdded(this, new EntityAddedEventArgs<User>(user));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void Update(User user)
        {
            //_dataContext.Origins.OnSubmit(origin);
            //_dataContext.SubmitChanges();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public void Delete(User user)
        {
            _notamDataContext.Users.DeleteOnSubmit(user);
            _notamDataContext.SubmitChanges();
            if (this.UserDeleted != null)
                this.UserDeleted(this, new EntityAddedEventArgs<User>(user));

        }

        /// <summary>
        /// Returns true if the specified origin exists in the
        /// repository, or false if it is not.
        /// </summary>
        public bool ContainsUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return _users.Contains(user);
        }

        /// <summary>
        /// Returns a shallow-copied list of all origins in the repository.
        /// </summary>
        public List<User> GetUsers()
        {
            return new List<User>(_users);
        }



        #endregion // Public Interface

        #region Private Helpers

        static List<User> LoadUsers(UserDataContext dataContext)
        {
            var result = new List<User>();
            if (dataContext != null)
            {
                var users = dataContext.Users;
                result.AddRange(users);
            }
            return result;

        }

        static List<User> LoadUsers(NotamDataContext dataContext)
        {
            var result = new List<User>();
            if (dataContext != null)
            {
                var users = dataContext.Users;
                result.AddRange(users);
            }
            return result;

        }
        #endregion


        //private static readonly List<InternalUserData> _users = new List<InternalUserData>() 
        //{ 
        //    new InternalUserData("Mark", "mark@company.com", 
        //    "MB5PYIsbI2YzCUe34Q5ZU2VferIoI4Ttd+ydolWV0OE=", new string[] { "Administrators" }), 
        //    new InternalUserData("John", "john@company.com", 
        //    "hMaLizwzOQ5LeOnMuj+C6W75Zl5CXXYbwDSHWW9ZOXc=", new string[] { })
        //};

        public User AuthenticateUser(string username, string clearTextPassword)
        {
            var userData = _users.FirstOrDefault(u => u.Username.Equals(username)
                && u.HashPassword.Equals(CalculateHash(clearTextPassword, u.Username)));
            if (userData == null)
                throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");

            return new User(userData.Username, userData.Role);
        }

        private string CalculateHash(string clearTextPassword, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            // Use the hash algorithm to calculate the hash
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // Return the hash as a base64 encoded string to be compared to the stored password
            return Convert.ToBase64String(hash);
        }
    }
 
}
