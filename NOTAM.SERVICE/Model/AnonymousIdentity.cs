using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NOTAM.SERVICE;

namespace NOTAM.Service
{
    public class AnonymousIdentity : CustomIdentity
    {
        public AnonymousIdentity()
            : base(string.Empty, UserRole.Anonymous)
        { }
    }
}
