using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FSServer.Model
{
    public class ClientList
    {
        public Dictionary<int, Client> Clients { get; set; }

        public ClientList()
        {
            Clients = new Dictionary<int, Client>();
        }
    }
}
