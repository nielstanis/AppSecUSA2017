using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Linq;
using System;

namespace WebApplication
{
    public class InfoController
    {
        readonly HtmlEncoder _htmlEncoder;
        readonly ApplicationPartManager _manager;

        public InfoController(HtmlEncoder htmlEncoder, ApplicationPartManager manager)
        {
            _htmlEncoder = htmlEncoder;
            _manager = manager;
        }

        [Produces("text/html")]
		public string Index_(string name)
		{
			return $"Hello {name}!";
		}

        public ContentResult Index(string name)
    	{
            return new ContentResult 
            { 
                Content = $"Hello {_htmlEncoder.Encode(name)}!", 
                ContentType = "text/html"
            };
    	}

        public ContentResult AvailableControllers()
        {
            var controllerFeature = new ControllerFeature();
            _manager.PopulateFeature(controllerFeature);
            var controllers = controllerFeature.Controllers.Select(x => x.ToString()).ToList();

            return new ContentResult
            {
                Content = String.Join(Environment.NewLine, controllers)
            };
        }
    }
}
