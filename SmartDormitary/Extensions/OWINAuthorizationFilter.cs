using Hangfire.Dashboard;

namespace SmartDormitary.Extensions
{
    public class OWINAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            return httpContext.User.Identity.IsAuthenticated && httpContext.User.IsInRole("Administrator");
        }
    }
}