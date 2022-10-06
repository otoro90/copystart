using System.Collections.Generic;

namespace CopyStart.Models
{
    public class Breadcrumb
    {
        public string Text { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }
        public Dictionary<string,string> Params { get; set; }
        public bool Active { get; set; }

        public Breadcrumb() { }

        public Breadcrumb(string text, string action, string controller, string area, bool active, Dictionary<string, string> parameters)
        {
            this.Text = text;
            this.Action = action;
            this.Controller = controller;
            this.Area = area;
            this.Active = active;
            this.Params = parameters;
        }
    }
}
