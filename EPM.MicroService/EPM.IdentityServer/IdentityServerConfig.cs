﻿using EPM.IdentityServer.DbModel;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace EPM.IdentityServer
{
    public class IdentityServerConfig
    {
        /// <summary>
        /// API资源
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public static IEnumerable<ApiScope> GetApiScopes(IConfigurationSection section)
        {
            List<ApiScope> resource = new List<ApiScope>();

            if (section != null)
            {
                List<ApiConfig> configs = new List<ApiConfig>();
                section.Bind("ApiScopes", configs);
                foreach (var config in configs)
                {
                    resource.Add(new ApiScope(config.Name, config.DisplayName));
                }
            }

            return resource.ToArray();
        }
        /// <summary>
        /// 定义受信任的客户端 Client
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients(IConfigurationSection section)
        {
            List<Client> clients = new List<Client>();

            if (section != null)
            {
                List<ClientConfig> configs = new List<ClientConfig>();
                section.Bind("Clients", configs);
                foreach (var config in configs)
                {
                    Client client = new Client();
                    client.ClientId = config.ClientId;
                    List<Secret> clientSecrets = new List<Secret>();
                    foreach (var secret in config.ClientSecrets)
                    {
                        clientSecrets.Add(new Secret(secret.Sha256()));
                    }
                    client.ClientSecrets = clientSecrets.ToArray();

                    GrantTypes grantTypes = new GrantTypes();
                    var allowedGrantTypes = grantTypes.GetType().GetProperty(config.AllowedGrantTypes);
                    client.AllowedGrantTypes = allowedGrantTypes == null ?
                        GrantTypes.ClientCredentials : (ICollection<string>)allowedGrantTypes.GetValue(grantTypes, null);

                    client.AllowedScopes = config.AllowedScopes.ToArray();

                    clients.Add(client);
                }
            }
            return clients.ToArray();
        }
    }
}
