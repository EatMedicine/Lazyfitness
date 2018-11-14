using System.Web.Mvc;

namespace Lazyfitness.Areas.informationOfUser
{
    public class informationOfUserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "informationOfUser";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "informationOfUser_default",
                "informationOfUser/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}