using BUS.Application;
using BUS.Authentication;
using BUS.Authentication.Session;
using Identity.Proxys;
using System;

namespace Identity.Service
{
    public class SessionService
    {
        public LoginResult Login(string login, string password)
        {
            throw new NotImplementedException();
            //var key = BUS.Base.Util.Encryption.GenerateKey();

            ////var proxy = new SessionProxy(CacheController.BaseCache.SessionBus);
            //var proxy = new SessionProxy();
            //var authenticationParameterPassword = new BUS.Authentication.AuthenticationParameterPassword
            //{
            //    UserName = login,
            //    Secret = BUS.Base.Util.Encryption.Encrypt(password)
            //};
            //var appliationModel = new ApplicationModel
            //{
            //    ApplicationID = 999,
            //    ApplicationName = "Identity",
            //    ApplicationToken = Guid.NewGuid().ToString(),
            //    CreatedAt = DateTime.Now,
            //    IsActive = true                
            //};
            //var loginResult = proxy.Login(appliationModel, sessionModel
            //                                                              CacheController.AuthenticationCache.LoginResult.Session,
            //                                                              CacheController.AuthenticationCache.ParameterPassword,
            //                                                              2);
            //CacheController.AuthenticationCache.LoginResult.Session = CacheController.AuthenticationCache.LoginResult.Session;
            //CacheController.BaseCache.SessionBus.UserID = (int)CacheController.AuthenticationCache.LoginResult.Session.UserID;

            //return CacheController.AuthenticationCache.LoginResult;
        }
    }
}
