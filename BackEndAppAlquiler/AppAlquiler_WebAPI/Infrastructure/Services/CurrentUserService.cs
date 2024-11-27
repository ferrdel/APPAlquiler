using AppAlquiler_BusinessLayer.Interfaces;
using System.Security.Claims;

namespace AppAlquiler_WebAPI.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public int? UserId { get; }

        public string UserName { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = Convert.ToInt32(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier));
            UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
        }
    }
}
