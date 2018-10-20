using System.Web.Mvc;

namespace Lazyfitness.Areas.backStage
{
    public class backStageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "backStage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "backStage_default",
                "backStage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}