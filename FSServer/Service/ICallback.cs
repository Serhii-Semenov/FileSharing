using System.ServiceModel;
using FSServer.Model;

namespace FSServer.Service
{

    public interface ICallback
    {
        /// <summary>
        /// Обновление списка подключенных к сервесу клиентов
        /// </summary>
        /// <param name="players"></param>
        [OperationContract(IsOneWay = true)]
        void UpdateClientList(ClientList clients);

        [OperationContract(IsOneWay = true)]
        void UpdateChat(ClientList clients);

        /// <summary>
        /// запрос на скакчивание файла
        /// </summary>
        /// <param name="cl">Идентификатор клиента и путь к троебуемому файлу</param>
        /// <returns></returns>
        [OperationContract]
        string TcpListenerAccept(ClientContract cl);

        /// <summary>
        /// Инициация скачивания файла
        /// </summary>
        /// <param name="address">IP:PORT Клиента с которого будет скачиваться файл</param>
        [OperationContract(IsOneWay = true)]
        void CreateTcpClient(string address, ClientContract cl);
    }
}
