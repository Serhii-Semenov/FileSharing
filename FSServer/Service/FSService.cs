using System;
using System.Collections.Generic;

using FSServer.Model;
using System.ServiceModel;
using FSServer.Privider;
using System.Linq;
using System.Threading.Tasks;



namespace FSServer.Service
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]

    class FSService : IFSService
    {
        private static List<UserCallback> userList = new List<UserCallback>();

        //public void AnswerForRequest(string ip, int _id)
        //{
        //    var client = userList.FirstOrDefault(c => c.Id == _id);
        //    if (client != null)
        //    {
        //        new Task(() =>
        //        {
        //            client.Callback.CreateTcpClient(ip);
        //        }).Start();
        //    }
        //}

        public void AnswerForRequest(string ip, ClientContract cl)
        {
            var client = userList.FirstOrDefault(c => c.Id == cl.recipient.Id);
            if (client != null)
            {
                new Task(() =>
                {
                    client.Callback.CreateTcpClient(ip, cl);
                }).Start();
            }
        }

        /// <summary>
        /// Отправить запрос  на скачивание файла 
        /// Инициировать срабатывание Callback метода  
        /// </summary>
        /// <param name="cl"></param>
        public void RequestFoDownload(ClientContract cl)
        {
            // Find Client
            var client = userList.FirstOrDefault(c => c.Id == cl.sender.Id);
            //cl.user.ClientName = name; // подменяем логин на того кто скачивает файл ?????????????????????
            if (client != null)
            {
                new Task(() =>
                {
                    client.Callback.TcpListenerAccept(cl);
                }).Start();
            }
        }

        public int CreateAccount(string _login, string _password, string _mail)
        {
            var prov = new Provider();
            int idClient = prov.CreateAccount(_login, _password, _mail);
            if (idClient > 0)
            {
                var callback = OperationContext.Current.GetCallbackChannel<ICallback>();

                if (!userList.Any(u => u.Id == idClient))
                {
                    var client = new Client() { Id = idClient, ClientName = _login };
                    userList.Add(new UserCallback(idClient, callback));
                    SendAll(callback);
                }
            }
            return idClient;
        }

        public ClientList GetClientOnline()
        {
            var cl = new ClientList();
            // ? хранить ли на Имена или только ID на службе в статике
            Provider pr = new Privider.Provider();
            foreach (var v in userList)
            {
                cl.Clients.Add( v.Id, (new Client() { Id = v.Id, ClientName = pr.GetNameById(v.Id) }));
            }

            return cl;
        }

        public int Login(string _login, string _password)
        {
            var prov = new Provider();
            int idClient = prov.Login(_login, _password);

            var callback = OperationContext.Current.GetCallbackChannel<ICallback>();

            if (!userList.Any(u => u.Id == idClient) && idClient > 0)
            {
                userList.Add(new UserCallback(idClient, callback));
                SendAll(callback);
            }
            return idClient;
        }

        /// <summary>
        /// Отправить вызов callback UpdateClientList метода на подключенных клиентах
        /// кроме currentClient
        /// </summary>
        /// <param name="currentClient">клиент который инициирует вызов метода</param>
        private void SendAll(ICallback currentClient)
        {
            foreach (var v in userList)
            {
                if (v.Callback != currentClient)
                {
                    v.Callback.UpdateClientList(GetClientOnline());
                }
            }
        }

        public void Logout(int id)
        {
            var callback = OperationContext.Current.GetCallbackChannel<ICallback>();
            var user = userList.FirstOrDefault(u=> u.Callback == callback);
            if (user != null)
            {
                userList.Remove(user);
            }
            SendAll(callback);
        }

        public bool AddPath(string _path, string _name)
        {
            var prov = new Provider();
            return prov.AddPath(_path, _name);
        }

        public List<string> GetPaths(string _name)
        {
            var prov = new Provider();
            return prov.GetPaths(_name);
        }

        public List<string> GetChat()
        {
            var prov = new Provider();
            return prov.GetChat();
        }

        public List<ClientContract> GetListForDownload(string _name)
        {
            var prov = new Provider();
            return prov.GetListForDownload(_name);
        }

        public int AddFileToDownloadTable(ClientContract cl)
        {
            var prov = new Provider();
            return prov.AddFileToDownloadTable(cl);
        }

        public void UpdateFileForDownload(ClientContract cl)
        {
            var prov = new Provider();
            prov.UpdateFileForDownload(cl);
        }

        public void SendMessageToChat(int _id, string _message)
        {
            var prov = new Provider();
            prov.SendMessageToChat(_message, _id);

            var callback = OperationContext.Current.GetCallbackChannel<ICallback>();

            if (!userList.Any(u => u.Callback == callback))
            {
                userList.Add(new UserCallback(_id, callback));
            }

            foreach (var v in userList)
            {
                if (v.Callback != callback)
                {
                    v.Callback.UpdateChat(GetClientOnline());
                }
            }
        }
    }
}
