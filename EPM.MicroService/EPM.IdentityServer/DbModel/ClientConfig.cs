using System.Collections.Generic;

namespace EPM.IdentityServer.DbModel
{
    public class ClientConfig
    {
        public string ClientId { get; set; }
        public List<string> ClientSecrets { get; set; }
        public string AllowedGrantTypes { get; set; }
        public List<string> AllowedScopes { get; set; }
    }
}
