using System.Runtime.Serialization;

namespace FSServer.Model
{
    [DataContract]
    public class Client
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string ClientName { get; set; }
    }
}
