using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.Filters
{
    public class CustomAuthAttribute : AuthorizeAttribute
    {
        bool IsAllowed;
        public CustomAuthAttribute(bool allowedParam)
        {
            IsAllowed = allowedParam;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.IsLocal)
            {
                return IsAllowed;
            }
            else
            {
                return true;
            }
        }
    }
}