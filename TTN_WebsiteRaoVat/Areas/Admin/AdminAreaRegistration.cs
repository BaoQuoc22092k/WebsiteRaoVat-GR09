using System.Web.Mvc;

namespace TTN_WebsiteRaoVat.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "LoginAdmin",controller="AdminHome", id = UrlParameter.Optional }
            );
        }
    }
}