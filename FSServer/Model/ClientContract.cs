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

        [DataMember]
        public int size { get; set; }

        [DataMember]
        public int sizecomplite { get; set; }

        [DataMember]
        public int complite { get; set; }

    }
}
