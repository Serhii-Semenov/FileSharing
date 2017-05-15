using System.ServiceModel;
using FSServer.Model;
using System.Collections.Generic;

namespace FSServer.Service
{
    [ServiceContract(CallbackContract = typeof(ICallback), SessionMode = SessionMode.Required)]

    interface IFSService
    {
        [OperationContract]
        int CreateAccount(string _login, string _password, string _mail);

        /// <summary>
        /// 
        /// OK -> UPDATE online=true
        /// </summary>
        /// <param name="_login"></param>
        /// <param name="_password"></param>
        /// <returns>id account</returns>
        [OperationContract]
        int Login(string _login, string _password);

        [OperationContract]
        void Logout(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns> ClientList </returns>
        [OperationContract]
        ClientList GetClientOnline();

        [OperationContract]
        bool AddPath(string _path, string _name);

        [OperationContract]
        List<string> GetPaths(string _name);

        [OperationContract]
        List<string> GetChat();

        [OperationContract]
        void SendMessageToChat(int _id, string _message);

        [OperationContract]
        void RequestFoDownload(ClientContract cl);

        [OperationContract]
        void AnswerForRequest(string ip, ClientContract cl);

        [OperationContract]
        List<ClientContract> GetListForDownload(string _name);

        [OperationContract]
        int AddFileToDownloadTable(ClientContract cl);

        [OperationContract]
        void UpdateFileForDownload(ClientContract cl);
    }
}
