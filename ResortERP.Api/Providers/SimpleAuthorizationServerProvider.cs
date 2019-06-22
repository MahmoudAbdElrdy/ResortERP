using Microsoft.Owin.Security.OAuth;
using ResortERP.Api.Controllers;
using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;



namespace ResortERP.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            UserVM user = getUser(context.UserName, context.Password, context);

            if (user == null || user.UPWD != context.Password)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            await Task.Run(() => RedirectConnection(context, context.UserName, user.CS_DataSource, user.CS_InitialCatalog, user.CS_UserId, user.CS_Password, user.USER_TYPE, Convert.ToString(user.ID)));

        }
        public async Task RedirectConnection(OAuthGrantResourceOwnerCredentialsContext context, string userName, string CS_DataSource, string CS_InitialCatalog, string CS_UserId, string CS_Password, int USER_TYPE, string userIdPK)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            identity.AddClaim(new Claim("sub", userName));
            identity.AddClaim(new Claim("role", "user"));
            identity.AddClaim(new Claim("datasource", CS_DataSource));
            identity.AddClaim(new Claim("Initialcatalog", CS_InitialCatalog));
            identity.AddClaim(new Claim("userid", CS_UserId));
            identity.AddClaim(new Claim("userIdPK", userIdPK));
            identity.AddClaim(new Claim("password", CS_Password));
            identity.AddClaim(new Claim("USER_TYPE", USER_TYPE.ToString()));
            identity.AddClaim(new Claim("connectionstring", string.Format("Data Source={0};Initial Catalog={1};user id={2};password={3};integrated security=false;", CS_DataSource, CS_InitialCatalog, CS_UserId, CS_Password)));
            await Task.Run(() => context.Validated(identity));

        }

        UserVM getUser(string userName, string password, OAuthGrantResourceOwnerCredentialsContext context)
        {
            string url = ConfigurationManager.AppSettings["baseUrl"].ToString() + "getuser?username=" + userName + "&password=" + password + "";

            WebRequest request = WebRequest.CreateHttp(url);
            request.Method = "GET";
            request.ContentType = "application/json";


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string result = readStream.ReadToEnd();

            UserVM user = Newtonsoft.Json.JsonConvert.DeserializeObject<UserVM>(result);
            return user;
        }

    }
}