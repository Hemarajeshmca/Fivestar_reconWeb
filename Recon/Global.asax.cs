using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Recon.Controllers;

namespace Recon
{
    public class MvcApplication : System.Web.HttpApplication
    {
        LoginController login = new LoginController();
        

       
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        //protected void Session_End(object sender, EventArgs e)
        //{
          

        //    login.Login();
           
        //    //new RedirectToRouteResult(new RouteValueDictionary { { "action", "Login" }, { "controller", "Login" } });
        //   // Response.Redirect("Login/Login");
        //   // RedirectResult("~/User/Login");
           
        //}

        //protected void Session_Start()
        //{

        //    if (Session["Username"] != null)
        //    {
        //        //Redirect to Welcome Page if Session is not null  
        //       // HttpContext.Current.Response.Redirect("~/WelcomeScreen", false);

        //    }
        //    else
        //    {
        //        //Redirect to Login Page if Session is null & Expires                   
        //        new RedirectToRouteResult(new RouteValueDictionary { { "action", "Index" }, { "controller", "Login" } });
        //    }
        //}


        //protected void Session_End()
        //{

        //    if (Session["Username"] != null)
        //    {
        //        //Redirect to Welcome Page if Session is not null  
        //        // HttpContext.Current.Response.Redirect("~/WelcomeScreen", false);

        //    }
        //    else
        //    {
        //        //Redirect to Login Page if Session is null & Expires                   
        //        new RedirectToRouteResult(new RouteValueDictionary { { "action", "Index" }, { "controller", "Login" } });
        //    }
        //}
    }
}
