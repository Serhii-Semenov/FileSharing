using System.Runtime.Serialization;

namespace FSServer.Model
{
    [DataContract]
    public class ClientContract 
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public Client sender { get; set; } 

        [DataMember]
        public Client recipient { get; set; }

        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public long size { get; set; }

        [DataMember]
        public long sizecomplite { get; set; } // количество записанных буферов по 1024

        [DataMember]
        public int complite { get; set; }

    }
}
