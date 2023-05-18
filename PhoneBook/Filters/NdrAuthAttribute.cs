using System;
using System.Security.Principal;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace PhoneBook.Filters
{
    public class NdrAuthAttribute: FilterAttribute, IAuthenticationFilter
    {
        public void onAuthentication(AuthenticationContext context)
        {
            IIdentity ident = context.Principal.Identity;
            if (!ident.IsAuthenticated)
            {
                context.Result = new HttpUnAuthorizedResult();
            }
        }
        public void onAuthenticationChallenge(AuthenticationChallengeContext context)
        {
            if (context.Result == null)
            {
                var r = new RouteValueDictionary {
                    {"controller","NdrAcc" },
                    { "action","Login"},
                    { "returnUrl",context.HttpContext.Request.RawUrl}};

                context.Result = new RedirectToRouteResult(r);
            }
        }
    }
}