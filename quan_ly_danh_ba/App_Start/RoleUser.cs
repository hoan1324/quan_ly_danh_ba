using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace quan_ly_danh_ba
{
    public class RoleUser:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = SessionConfig.GetUser();
            if (user != null) {
                return;
            }
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(new {
                    controller="Login",
                    action="SignIn",
                    area=""
                }));
        }
    }
}