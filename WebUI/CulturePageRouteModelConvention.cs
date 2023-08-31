using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace WebUI
{
	public class CulturePageRouteModelConvention : IPageRouteModelConvention
	{
		public void Apply(PageRouteModel model)
		{
			foreach (var selector in model.Selectors)
			{
				var template = selector.AttributeRouteModel.Template;

                // Check if the template starts with "admin/authentication/Logout"
                if (!template.StartsWith("admin", StringComparison.OrdinalIgnoreCase))
                {
                    // Add a required route parameter for the language code
                    selector.AttributeRouteModel.Template = "{lang}/" + template;
                }
            }
		}
	}
}
