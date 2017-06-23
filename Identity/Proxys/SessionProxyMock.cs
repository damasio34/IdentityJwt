using BUS.Authentication.Session;
using Identity.Model;

namespace Identity.Proxys
{
    public class SessionProxyMock
    {
        public LoginResult Login(string username, string password)
        {
            if (username.Equals("darlan.silva") && password.Equals("1234")) return new LoginResult();
            else return null;
        }
    }
}
