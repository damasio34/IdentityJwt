using System;
using System.ComponentModel.DataAnnotations;

namespace Identity.Model
{
    public class Token
    {
        public Token(string key, DateTime validity)
        {
            this.Key = key;
            this.Validity = validity;
        }

        public string Key { get; protected set; }
        public DateTime Validity { get; protected set; }
    }
}
