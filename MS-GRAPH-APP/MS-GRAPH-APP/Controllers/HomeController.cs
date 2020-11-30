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
        // private const string token = "eyJ0eXAiOiJKV1QiLCJub25jZSI6IlRibUx0RlJMMFp5RFFSYWZnaVQtYlFiWElvQmVlbDlxLTNVcU5BZi15VW8iLCJhbGciOiJSUzI1NiIsIng1dCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCIsImtpZCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC85NTYxMmQ4Yi1jNWZiLTQwMWItYjM3Ny0yODM5NWNhM2Y3NTEvIiwiaWF0IjoxNjA2NzM5NTU3LCJuYmYiOjE2MDY3Mzk1NTcsImV4cCI6MTYwNjc0MzQ1NywiYWNjdCI6MCwiYWNyIjoiMSIsImFjcnMiOlsidXJuOnVzZXI6cmVnaXN0ZXJzZWN1cml0eWluZm8iLCJ1cm46bWljcm9zb2Z0OnJlcTEiLCJ1cm46bWljcm9zb2Z0OnJlcTIiLCJ1cm46bWljcm9zb2Z0OnJlcTMiLCJjMSIsImMyIiwiYzMiLCJjNCIsImM1IiwiYzYiLCJjNyIsImM4IiwiYzkiLCJjMTAiLCJjMTEiLCJjMTIiLCJjMTMiLCJjMTQiLCJjMTUiLCJjMTYiLCJjMTciLCJjMTgiLCJjMTkiLCJjMjAiLCJjMjEiLCJjMjIiLCJjMjMiLCJjMjQiLCJjMjUiXSwiYWlvIjoiRTJSZ1lBZzJsVHJkR0hDOTlLVkFxa3JuTTh0bi9lcDlFU2RFL3hVMkgvdHdZZFp1NTI0QSIsImFtciI6WyJwd2QiXSwiYXBwX2Rpc3BsYXluYW1lIjoiR3JhcGggZXhwbG9yZXIgKG9mZmljaWFsIHNpdGUpIiwiYXBwaWQiOiJkZThiYzhiNS1kOWY5LTQ4YjEtYThhZC1iNzQ4ZGE3MjUwNjQiLCJhcHBpZGFjciI6IjAiLCJmYW1pbHlfbmFtZSI6IkNoYXVoYW4iLCJnaXZlbl9uYW1lIjoiU3VzaGlsIiwiaWR0eXAiOiJ1c2VyIiwiaXBhZGRyIjoiNDcuMTEuMjM0LjE3OSIsIm5hbWUiOiJTdXNoaWwgQ2hhdWhhbiIsIm9pZCI6ImIyNmNmOTQxLTJkMzctNDQyOS1hMDg4LWUxMjAyNjI5ZmQ1MyIsInBsYXRmIjoiMyIsInB1aWQiOiIxMDAzMjAwMEZCODgyRkEyIiwicmgiOiIwLkFBQUFpeTFobGZ2RkcwQ3pkeWc1WEtQM1ViWElpOTc1MmJGSXFLMjNTTnB5VUdSMUFMdy4iLCJzY3AiOiJBZG1pbmlzdHJhdGl2ZVVuaXQuUmVhZC5BbGwgQWRtaW5pc3RyYXRpdmVVbml0LlJlYWRXcml0ZS5BbGwgQVBJQ29ubmVjdG9ycy5SZWFkLkFsbCBBUElDb25uZWN0b3JzLlJlYWRXcml0ZS5BbGwgQXBwbGljYXRpb24uUmVhZC5BbGwgQXBwbGljYXRpb24uUmVhZFdyaXRlLkFsbCBEZXZpY2UuQ29tbWFuZCBEZXZpY2UuUmVhZCBEZXZpY2UuUmVhZC5BbGwgRGlyZWN0b3J5LkFjY2Vzc0FzVXNlci5BbGwgRGlyZWN0b3J5LlJlYWQuQWxsIERpcmVjdG9yeS5SZWFkV3JpdGUuQWxsIGVtYWlsIEZpbGVzLlJlYWQgRmlsZXMuUmVhZC5BbGwgRmlsZXMuUmVhZC5TZWxlY3RlZCBGaWxlcy5SZWFkV3JpdGUgRmlsZXMuUmVhZFdyaXRlLkFsbCBGaWxlcy5SZWFkV3JpdGUuQXBwRm9sZGVyIEZpbGVzLlJlYWRXcml0ZS5TZWxlY3RlZCBHcm91cC5SZWFkLkFsbCBHcm91cC5SZWFkV3JpdGUuQWxsIElkZW50aXR5UHJvdmlkZXIuUmVhZC5BbGwgSWRlbnRpdHlQcm92aWRlci5SZWFkV3JpdGUuQWxsIG9wZW5pZCBQZW9wbGUuUmVhZCBQZW9wbGUuUmVhZC5BbGwgcHJvZmlsZSBVc2VyLlJlYWQgVXNlci5SZWFkV3JpdGUgVXNlci5SZWFkV3JpdGUuQWxsIiwic2lnbmluX3N0YXRlIjpbImttc2kiXSwic3ViIjoiOU5KRVRTNndza0haTVhLemp1T3poS000YVN0cHhsUHI4X2pnQzlJRExKTSIsInRlbmFudF9yZWdpb25fc2NvcGUiOiJOQSIsInRpZCI6Ijk1NjEyZDhiLWM1ZmItNDAxYi1iMzc3LTI4Mzk1Y2EzZjc1MSIsInVuaXF1ZV9uYW1lIjoic3VzaGlsQEN5bm90ZWNrQkNUcmlhbC5vbm1pY3Jvc29mdC5jb20iLCJ1cG4iOiJzdXNoaWxAQ3lub3RlY2tCQ1RyaWFsLm9ubWljcm9zb2Z0LmNvbSIsInV0aSI6Im1yalNDNU5pOEVxdlhjb2ZUM1lDQUEiLCJ2ZXIiOiIxLjAiLCJ3aWRzIjpbIjYyZTkwMzk0LTY5ZjUtNDIzNy05MTkwLTAxMjE3NzE0NWUxMCIsImYyOGExZjUwLWY2ZTctNDU3MS04MThiLTZhMTJmMmFmNmI2YyIsImI3OWZiZjRkLTNlZjktNDY4OS04MTQzLTc2YjE5NGU4NTUwOSJdLCJ4bXNfc3QiOnsic3ViIjoieV9TQ3N3TzdFeTZTN19WX0hrZmtiaVM3UGFQZ2tvZ1doR0NQZzE2SHhORSJ9LCJ4bXNfdGNkdCI6MTYwNTc3MzE3OH0.Xqv4QFSz1gcS4FjzKxF-xLpjF0GUttuF2hrMStuzX6aye0i9XM345x7pE3jlQvAbthbxbtweGj8XObDAcddJe82GG_FRnMWlcUKquZBquGleP5CbvyetFJBMRqlqCT-txRrAdr2emoH6coqfvYqe23vvPA1zG-yKfAqcNg-Zz2c15HeeRUml5sjdLxrt3OwteP4VNajCB1lSM9ARqCtzZDrV7xZNlYsbhevx2Vrz1VYWjRuJIC0vFq6e4JSR1h49eR1GAOV7L-UzIH_31521Jese5xmRKe6VPj0OeOfzBAbBUGMobJjJP9p7HuvPRUnVOxNK_1fXFnwEsVPN1MDr2w";

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


        public async static Task getUsersAsync()
        {
            var clientId = "44d45105-e1e5-4be7-906b-d32399a97cb2";
            var tenantId = "95612d8b-c5fb-401b-b377-28395ca3f751";
            var clientSecret = "oAxpY087Z31~-7q2Y~5T_c5LjMNhqa_Y6_";
            var authority = $"https://login.microsoftonline.com/{tenantId}";
            //            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
            //                .Create(clientId)
            //                .WithTenantId(tenantId)
            //                .WithClientSecret(clientSecret)
            //                .Build();
            //            ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);
            //            GraphServiceClient graphClientAuth = new GraphServiceClient(authProvider);
            //            var driveItem1 = await graphClientAuth.Me.Drive.Root
            //.Request()
            //.GetAsync();

            IConfidentialClientApplication app;
            app = ConfidentialClientApplicationBuilder.Create(clientId)
                                                      .WithClientSecret(clientSecret)
                                                      .WithAuthority(new Uri(authority))
                                                      .Build();

            var scope = new List<string>();
            scope.Add("https://graph.microsoft.com/.default");
            //scope.Add("https://graph.microsoft.com/User.ReadWrite.All");

            var result = await app.AcquireTokenForClient(scope)
                  .ExecuteAsync();





            GraphServiceClient graphClient = new GraphServiceClient(new DelegateAuthenticationProvider(async request =>
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", "");
            }));

            var drives = await graphClient.Sites["root"]
           .Request()
           .GetAsync();




           var site = await graphClient.Sites["cynoteckbctrial.sharepoint.com,8c6e519c-a58c-4ac0-9ecc-80dc5fa1bed9,ea7c383c-3eeb-4aee-a9d7-a6758f48d188"]
                .Lists
                                        .Request()
                                        .GetAsync();




    //        var driveItem = new ListItem
    //        {
                
    //            Name = "New Folder11",
    //            Folder = new Folder
    //            {
    //            },
    //            AdditionalData = new Dictionary<string, object>()
    //{
    //    {"@microsoft.graph.conflictBehavior", "rename"}
    //}
    //        };

            var children = await graphClient.Me.Drive.Root.Children
              .Request()
              .GetAsync();

   


            //        var drives = await graphClient.Sites["root"].Lists
            //       .Request()
            //       .GetAsync();



        }

        private static async Task<GraphServiceClient> GetGraphApiClient()
        {

            string APP_ID = "44d45105-e1e5-4be7-906b-d32399a97cb2";
            string APP_SECERET = "oAxpY087Z31~-7q2Y~5T_c5LjMNhqa_Y6_";
            string APP_TENANT_ID = "95612d8b-c5fb-401b-b377-28395ca3f751";
            string TOKEN_ENDPOINT = $"https://login.microsoftonline.com/{APP_TENANT_ID}/oauth2/v2.0/token";
            string MS_GRAPH_SCOPE = "https://graph.microsoft.com/.default";




            GraphServiceClient graphClient = new GraphServiceClient(new DelegateAuthenticationProvider(async request =>
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", "");
            }));




            return graphClient;
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