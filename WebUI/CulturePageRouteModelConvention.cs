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
                    if (template.Contains("Single"))
                    {
                        // Handle the "Single" page
                        selector.AttributeRouteModel.Template = "{lang}/{title}";
                    }
                    else
                    {
                        // Handle other pages
                        selector.AttributeRouteModel.Template = "{lang}/" + template;
                    }
                }
            }
		}
	}
}
