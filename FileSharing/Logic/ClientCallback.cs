using System;
using System.Collections.Generic;

using FileSharing.FSService;
using System.Net.Sockets;
using System.ServiceModel;
using System.Net;

namespace FileSharing.Logic
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    class ClientCallback : IFSServiceCallback
    {
        public event Action<ClientList> UpdateClientsEven;
        public event Action<ClientList> UpdateChatEven;
        public event Action<ClientContract> TcpListenerAcceptEven;
        public event Action<IPEndPoint, ClientContract> CreateTcpClientEven;

        /// <summary>
        /// Обновляет у всех клиентов ListBox с подключенными клиентами 
        /// </summary>
        /// <param name="clients"></param>
        public void UpdateClientList(ClientList clients)
        {
            if (UpdateClientsEven != null) UpdateClientsEven(clients);
            //ClientsUpdated?.Invoke(clients);
        }

        /// <summary>
        /// Обновляет у всех клиентов Chat ListBox
        /// </summary>
        /// <param name="clients"></param>
        public void UpdateChat(ClientList clients)
        {
            if (UpdateChatEven != null) UpdateChatEven(clients);
            //ChatUpdated?.Invoke(clients);
        }

        /// <summary>
        /// Запрос на скачивание файла
        /// </summary>
        /// <param name="cl"></param>
        /// <returns></returns>
        public string TcpListenerAccept(ClientContract cl)
        {
            // проверить на запуск 1го экзкмпляра
            // TcpListener tl = new TcpListener();
            if (TcpListenerAcceptEven != null) TcpListenerAcceptEven(cl);

            return "10.6.0.34:49453";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public void CreateTcpClient(IPEndPoint ep, ClientContract cl)
        {
            if (CreateTcpClientEven != null) CreateTcpClientEven(ep, cl);
            //StartDownLoadIF?.Invoke(address);
        }

    }
}

