using FileSharing.FSService;

namespace FileSharing.Logic
{
    static class ClientRoomCore
    {
        public static ClientStatus Status { get; set; }

        public static Client Client { get; set; }

        public static ClientList Clients { get; set; }
    }
}
