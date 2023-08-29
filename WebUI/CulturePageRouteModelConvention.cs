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

				// Add a required route parameter for the language code
				selector.AttributeRouteModel.Template = "{lang}/" + template;
			}
		}
	}
}
