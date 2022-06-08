using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace CopyStart.Filters
{
    public class UrlScriptActionFilter : ActionFilterAttribute
    {
        private readonly string _namePage;

        /// <summary>
        /// Constructor
        /// </summary>
        public UrlScriptActionFilter()
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="namePage"></param>
        public UrlScriptActionFilter(string nameScript)
        {
            _namePage = nameScript;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Path.HasValue && context.HttpContext.Request.Path.Value.Contains("Api"))
            {
                // This is an API request.  
                base.OnActionExecuted(context);
                return;
            }

            var controller = context.Controller as Controller;
            this.GetScriptUrl(context, controller);

            base.OnActionExecuted(context);
        }
        private void GetScriptUrl(ActionExecutedContext context, Controller controller)
        {
            if (context.HttpContext.Request.Path.HasValue)
            {
                var pathSplit = context.HttpContext.Request.Path.Value.Split("/");
                var page = _namePage ?? (pathSplit.Length == 4 ? pathSplit.Last() : "Index");
                var scriptUrl = $"/js/Areas/{pathSplit[1]}/{pathSplit[2]}/{page}.js";

                controller.ViewBag.ScriptUrl = scriptUrl;
                controller.ViewBag.Page = page;

            }

        }
    }
}
