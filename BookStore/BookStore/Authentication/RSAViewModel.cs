using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Authentication
{
    public class RSAViewModel
    {
        public List<string> PublicKeysList { get; set; }
        public List<string> PrivateKeysList { get; set; }
        public List<Tuple<string, string>> AuthMemberString { get; set; }
        public string nowEncryptAccount { get; set; }
        public string nowEncryptPassword { get; set; }
        public string nowPublicKey { get; set; }
        public string nowPrivateKey { get;set; }
    }
}