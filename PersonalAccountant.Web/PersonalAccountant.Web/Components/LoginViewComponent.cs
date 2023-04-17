using Microsoft.AspNetCore.Mvc;

namespace PersonalAccountant.Web.Components
{
	[ViewComponent]
	public sealed class LoginViewComponent : ViewComponent
	{
		private readonly IHttpContextAccessor _contextAccessor;

		public LoginViewComponent(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var user = _contextAccessor.HttpContext?.User;

			return View<string>(user?.Identity?.Name);
		}
	}
}
