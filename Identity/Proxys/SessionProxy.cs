using BUS.Application;
using BUS.Authentication;
using BUS.Authentication.Session;
using BUS.Base;
using BUS.Core.Session;
using System;
using System.Collections.Generic;

namespace Identity.Proxys
{
    public class SessionProxy : IBUSProxy
    {
        #region [ Attributes ]

        /// <summary>
        /// Attribute for 
        /// </summary>
        BUSProxy proxy;

        public SessionProxy()
        {
            proxy = new BUSProxy(this);
        }

        public SessionProxy(BUSSession busSession)
        {
            proxy = new BUSProxy(this, busSession);
        }

        #endregion

        // Creating session     
        public LoginResult Login(ApplicationModel app)
        {
            proxy.SetArguments(app);
            return proxy.Execute<LoginResult>();
        }
        public void Logout(SessionModel session)
        {
            proxy.SetArguments(session);
            proxy.Execute();
        }
        //public AuthenticationLevelType[] GetStepByUserID(int applicationID, int? id)
        //{
        //    proxy.SetArguments(applicationID, id);
        //    return proxy.Execute<AuthenticationLevelType[]>();
        //}
        public LoginResult Login(ApplicationModel app, SessionModel session, AuthenticationParameter param, int step = 1)
        {
            proxy.SetArguments(app, session, param, step);
            return proxy.Execute<LoginResult>();
        }

        //                                       NOT USING
        //*******************************************************************************************
        //*******************************************************************************************

        public LoginResult Login(ApplicationModel app, AuthenticationParameter param)
        {
            proxy.SetArguments(app, param);
            return proxy.Execute<LoginResult>();
        }
        public SessionModel CheckSession(SessionModel session)
        {
            proxy.SetArguments(session);
            return proxy.Execute<SessionModel>();
        }
        public List<SessionViewModel> GetActiveUserSessions(int userID)
        {
            proxy.SetArguments(userID);
            return proxy.Execute<List<SessionViewModel>>();
        }
        public void CloseSession(Guid sessionID)
        {
            proxy.SetArguments(sessionID);
            proxy.Execute();
        }

        #region [ IBUSProxy Interface Implementation ]

        /// <summary>
        /// Name of the source BUS.
        /// </summary>
        public string SourceBUSName
        {
            get { return Constants.SOURCE_BUS_NAME; }
        }

        /// <summary>
        /// Name of the target BUS.
        /// </summary>
        public string TargetBUSName
        {
            get { return Constants.TARGET_BUS_SESSION; }
        }
        #endregion
    }
}
