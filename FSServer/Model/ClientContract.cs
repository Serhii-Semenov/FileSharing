using System.Runtime.Serialization;

namespace FSServer.Model
{
    [DataContract]
    public class ClientContract 
    {
        [DataMember]
        public Client sender { get; set; } // was user

        [DataMember]
        public Client recipient { get; set; }

        [DataMember]
        public string Path { get; set; }

    }
}
