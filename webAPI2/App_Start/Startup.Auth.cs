using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using webAPI2.Providers;
using webAPI2.Models;

/*
 https://tools.ietf.org/html/rfc6750
 The OAuth 2.0 Authorization Framework: Bearer Token Usage

Abstract

   This specification describes how to use bearer tokens in HTTP
   requests to access OAuth 2.0 protected resources.  Any party in
   possession of a bearer token (a "bearer") can use it to get access to
   the associated resources (without demonstrating possession of a
   cryptographic key).  To prevent misuse, bearer tokens need to be
   protected from disclosure in storage and in transport.

*/


namespace webAPI2
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());  //Active

            //**********************************************************************************************
            /* 
             * http://stackoverflow.com/questions/26166826/usecookieauthentication-vs-useexternalsignincookie
             * why do I need UseCookieAuthentication and UseExternalSignInCookie?
             You need all of them, if you want Google sign in to work. This is how it works. 
             * In the OWIN pipeline, you have three middleware components: 
             * (1) the cookie authentication middleware running in active mode, 
             * (2) another instance of cookie authentication middleware but running in passive mode, and 
             * (3) Google authentication middleware. That will be like so.

                app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    ...
                }); // Active

                app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie); // Passive

                app.UseGoogleAuthentication(...);
                When there is a 401, your user gets redirected to Google. There, your user logs in and Google validates the credential. 
                Google then redirects the user back to your app. At this point, Google authentication middleware gets the login info, 
                applies a grant (read external cookie) and short circuits the OWIN pipeline and redirects to the external callback URL, 
                which corresponds to ExternalLoginCallback action method of AccountController. So, 
                at this point when the request comes to your app as a result of redirect, you get the external cookie 
                with the user name and email claims.
             
                             */
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie); //Passive

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "330193776007-lhimr7k2tuchn7euseqbgessnj64usgt.apps.googleusercontent.com",
                ClientSecret = "NM7MYz-WTxZnObi3HRiQ51bE"
            });
        }
    }
}
