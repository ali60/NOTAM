using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using NOTAM.SERVICE;

namespace NOTAM.Service
{
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(string name, UserRole role)
        {
            Name = name;
            Role = role;
            if(role.Equals(UserRole.Administrator ))
                Roles = new string[] { "Administrator" };
            else
                Roles = new string[] { "User" };
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public UserRole Role { get; private set; }
        public string[] Roles { get; private set; }


        #region IIdentity Members
        public string AuthenticationType { get { return "Custom authentication"; } }

        public bool IsAuthenticated { get { return !string.IsNullOrEmpty(Name); } }
        #endregion
    }
}
