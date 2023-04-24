using Microsoft.AspNetCore.Mvc;

namespace PersonalAccountant.Web.Components
{
	[ViewComponent]
	public sealed class MenuViewComponent : ViewComponent
	{
		public Task<IViewComponentResult> InvokeAsync()
		{
			return Task.FromResult(View() as IViewComponentResult);
		}
	}
}
