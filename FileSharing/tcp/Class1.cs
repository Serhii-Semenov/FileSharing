using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FileSharing.tcp
{
    class Class1 // Client
    {
        public Class1()
        {
            TcpClient eclient = new TcpClient("localhost", 34567);
            NetworkStream writerStream = eclient.GetStream();
            BinaryFormatter format = new BinaryFormatter();
            byte[] buf = new byte[1024];
            int count;
            FileStream fs = new FileStream("111.txt", FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            long k = fs.Length;//Размер файла.
            format.Serialize(writerStream, k.ToString());//Вначале передаём размер
            while ((count = br.Read(buf, 0, 1024)) > 0)
            {
                format.Serialize(writerStream, buf);//А теперь в цикле по 1024 байта передаём файл
            }
        }
    }

    class Class2 // Server
    {
        public Class2()
        {
            TcpListener clientListener = new TcpListener(34567);
            clientListener.Start();
            TcpClient client = clientListener.AcceptTcpClient();
            NetworkStream readerStream = client.GetStream();
            BinaryFormatter outformat = new BinaryFormatter();
            FileStream fs = new FileStream("111.jpg", FileMode.OpenOrCreate);
            BinaryWriter bw = new BinaryWriter(fs);
            int count;
            count = int.Parse(outformat.Deserialize(readerStream).ToString());//Получаем размер файла
            int i = 0;
            for (; i < count; i += 1024)//Цикл пока не дойдём до конца файла
            {

                byte[] buf = (byte[])(outformat.Deserialize(readerStream));//Собственно читаем из потока и записываем в файл
                bw.Write(buf);
            }
            bw.Close();
            fs.Close();
        }
    }
}
