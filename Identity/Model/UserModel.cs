using Microsoft.AspNet.Identity;
using System;

namespace Identity.Model
{
    public class UserModel : IUser<Guid>
    {
        public UserModel()
        {
            this.SessionId = Guid.NewGuid().ToString();
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string SessionId { get; private set; }
        public Guid Id { get; protected set; }
    }
}
