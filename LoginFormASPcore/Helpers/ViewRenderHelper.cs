namespace LoginFormASPcore.Helpers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using System.IO;
    using System.Threading.Tasks;

    namespace YourProjectNamespace.Helpers
    {
        public static class ViewRenderHelper
        {
            public static async Task<string> RenderViewAsync<TModel>(
                this Controller controller,
                string viewName,
                TModel model,
                bool partial = false)
            {
                controller.ViewData.Model = model;

                using var writer = new StringWriter();
                var serviceProvider = controller.HttpContext.RequestServices;
                var viewEngine = (IRazorViewEngine)serviceProvider.GetService(typeof(IRazorViewEngine));
                var tempDataProvider = (ITempDataProvider)serviceProvider.GetService(typeof(ITempDataProvider));
                var viewResult = viewEngine.FindView(controller.ControllerContext, viewName, !partial);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"View '{viewName}' not found.");
                }

                var viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return writer.GetStringBuilder().ToString();
            }
        }
    }

}
