using Microsoft.AspNetCore.Authorization;
using System;

namespace DoubleV.Helpers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeRolesAttribute : Attribute
    {
        public string[] Roles { get; }

        public AuthorizeRolesAttribute(params string[] roles)
        {
            Roles = roles;
        }
    }
}
