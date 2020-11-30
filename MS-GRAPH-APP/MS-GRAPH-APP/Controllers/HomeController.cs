using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using MS_GRAPH_APP.SharePoint;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MS_GRAPH_APP.Controllers
{
    public class HomeController : Controller
    {

        public async Task<bool> CreateFolder()
        {
            try
            {
                var graphClient = await AuthenticationToken.GetGraphServiceClient();

            }
            catch (Exception ex)
            {

                throw;
            }
            return true;
        }
        public async Task<ActionResult> Index()
        {
            var graphClient = await  AuthenticationToken.GetGraphServiceClient();

            var driveItem = new DriveItem
            {
                Name = "New Folder",
                Description = "",
                Folder = new Folder
                {
                },
                AdditionalData = new Dictionary<string, object>()
    {
        {"@microsoft.graph.conflictBehavior", "fail"}
    }
            };

    //        var drive = await graphClient.Drive.Root.Children
    //.Request()
    //.AddAsync(driveItem);


            var site = await graphClient.Sites["root"].Lists["94ba3add-098b-4395-b361-904160f84476"].Items["1"].Versions
           .Request()
           .GetAsync();



            return View();
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}