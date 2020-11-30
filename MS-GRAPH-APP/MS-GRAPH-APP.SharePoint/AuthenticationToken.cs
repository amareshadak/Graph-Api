using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS_GRAPH_APP.SharePoint
{
    public static class AuthenticationToken
    {
        #region Get Bearer Access Token
        private static async Task<string> GetAccessToken()
        {
            try
            {
#if DEBUG

#else
            string clientId = Environment.GetEnvironmentVariable("GRAPHAPI_CLIENTID");
            string tenantId = Environment.GetEnvironmentVariable("GRAPHAPI_TENANTID");
            string clientSecret = Environment.GetEnvironmentVariable("GRAPHAPI_CLIENTSECRET");
            string authority = Environment.GetEnvironmentVariable("GRAPHAPI_AUTHORITY");
#endif


                IConfidentialClientApplication app;
                app = ConfidentialClientApplicationBuilder.Create(clientId)
                                                          .WithClientSecret(clientSecret)
                                                          .WithAuthority(new Uri(authority))
                                                          .Build();
                var scope = GraphScope();

                var result = await app.AcquireTokenForClient(scope)
                                      .ExecuteAsync();

                return result.AccessToken;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Get Graph Scope
        private static List<string> GraphScope()
        {
            List<string> scope = new List<string>();
            scope.Add("https://graph.microsoft.com/.default");
            return scope;
        }
        #endregion

        #region Get Request Client
        public static async Task<GraphServiceClient> GetGraphServiceClient()
        {
            var token = await GetAccessToken();
            GraphServiceClient graphClient = new GraphServiceClient(new DelegateAuthenticationProvider(async request =>
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            }));

            return graphClient;
        }
        #endregion




    }
}
