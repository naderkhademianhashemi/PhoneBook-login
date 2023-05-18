using Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace PhoneBook.Filters
{
    public class NdrAutAttribute : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext context)
        {
            IIdentity ident = context.Principal.Identity;

            if (!ident.IsAuthenticated)
            {
                context.Result = new HttpUnauthorizedResult();
            }
        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext context)
        {
            var res = context.Result;

            if (context.Result == null || context.Result is HttpUnauthorizedResult)
            {
                var r = new RouteValueDictionary {
                    {"controller",EnglishResourse.LoginController },
                    { "action",EnglishResourse.LoginAction},
                    { "returnUrl",context.HttpContext.Request.RawUrl}};

                context.Result = new RedirectToRouteResult(r);
            }
        }
    }
}