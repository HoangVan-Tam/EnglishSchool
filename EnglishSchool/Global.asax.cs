using System.Web.Http;

namespace EnglishSchool
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AutoMapperConfiguration.Configure();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
