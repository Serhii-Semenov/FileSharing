using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;

using FileSharing.FSService;
using System.ServiceModel;
using FileSharing.Logic;
using Microsoft.Win32;
using System.Threading;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using Logger;


namespace FileSharing
{
    public partial class MainWindow : Window
    {
        ClientContract staff;
        List<ClientContract> forDownLoad;

        int id = 0;
        string name = "";
        FSServiceClient service;
        ILogger Log;

        public MainWindow()
        {
            InitializeComponent();

            Init();

        }

        private void Init()
        {
            ToLoginView();

            // Initialize Logger
            Log = WPFLogger.Instance;
            WPFLogger.Instance.Initialize((ListBox)lbxLOG);

            // Initialize List<ClientContract> forDownLoad;
            // InitForDownload();
        }

        private void InitForDownload()
        {
            lbxDownloading.Items.Clear();
            int i = 0;
            try
            {
                forDownLoad = new List<ClientContract>(service.GetListForDownload(name));
                foreach (var v in forDownLoad)
                {
                    WrapPanel wp = new WrapPanel();
                    wp.Tag = i;

                    Label lblClient = new Label() { Content = v.recipient.ClientName};
                    lblClient.Width = 150;
                    lblClient.BorderThickness = new Thickness(1);
                    lblClient.Margin = new Thickness(5);
                    wp.Children.Add(lblClient);

                    Label lblPath = new Label() { Content = v.Path };
                    lblPath.Width = 150;
                    lblPath.BorderThickness = new Thickness(1);
                    lblPath.Margin = new Thickness(5);
                    wp.Children.Add(lblPath);

                    ProgressBar pbComplite = new ProgressBar()
                    {
                        Width = 200,
                        Minimum = 0,
                        Maximum = 100,
                        Value = v.complite
                    };
                    wp.Children.Add(pbComplite);

                    Button btnResume = new Button()
                    {
                        Content = "Resume file",
                        Margin = new Thickness(5),
                        Tag = i
                    };

                    btnResume.Click += BtnResume_Click;
                    wp.Children.Add(btnResume);

                    i++;
                    lbxDownloading.Items.Add(wp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnResume_Click(object sender, RoutedEventArgs e)
        {
            var i = ((Button)sender).Tag;
            MessageBox.Show(i.ToString());
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                service.Logout(ClientRoomCore.Client.Id);
            }
            catch { }
            finally
            {
                base.OnClosing(e);
            }
        }

        private void btnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            btnAddCollapsed(true);
        }

        private void btnAddCollapsed(bool key)
        {
            var yes = Visibility.Visible;
            var no = Visibility.Collapsed;
            if (key)
            {
                wpAddPerson.Visibility = yes;
                btnLogin.Visibility = no;
                btnAddPerson.Visibility = no;
                btnOkAddPerson.Visibility = yes;
                btnCancelAddPerson.Visibility = yes;
            }
            else
            {
                wpAddPerson.Visibility = no;
                btnLogin.Visibility = yes;
                btnAddPerson.Visibility = yes;
                btnOkAddPerson.Visibility = no;
                btnCancelAddPerson.Visibility = no;
            }
        }

        private void btnOkAddPerson_Click(object sender, RoutedEventArgs e)
        {
            // Add person in DB
            if (tbLogin.Text == "")
            {
                MessageBox.Show("Input login...");
                return;
            }
            else if (tbPassword.Text == "")
            {
                MessageBox.Show("Input password...");
                return;
            }
            else if (tbMail.Text == "")
            {
                MessageBox.Show("Input mail...");
                return;
            }

            if (Connect(true) < 0)
            {
                MessageBox.Show("Could not add client to database");
                return;
            }
            WhenLoginSuccess();
        }

        private void WhenLoginSuccess()
        {
            exAuth.Header = tbLogin.Text;
            TbClear();
            exAuth.IsExpanded = false;
            exAuth.IsEnabled = false;
            btnDisconnect.IsEnabled = true;
            btnChat.IsEnabled = true;
            btnDownload.IsEnabled = true;
        }

        private void ToLoginView()
        {
            exAuth.Header = "Authorization";
            TbClear();
            LbxClear();
            exAuth.IsExpanded = true;
            exAuth.IsEnabled = true;
            btnDisconnect.IsEnabled = false;
            btnChat.IsEnabled = false;
            btnDownload.IsEnabled = false;
            btnUpload.Visibility = Visibility.Hidden;
        }

        private int Connect(bool add)
        {
            try
            {
                id = 0;
                name = "";
                var callback = new ClientCallback();

                callback.UpdateClientsEven += cl =>
                {
                    ClientRoomCore.Clients = cl;
                    UpdateClientsList();
                };

                callback.UpdateChatEven += cl =>
                {
                    ClientRoomCore.Clients = cl;
                    UpdateChat();
                };


                callback.TcpListenerAcceptEven += Callback_TcpListenerAccept;
                callback.CreateTcpClientEven += Callback_CreateTcpClient;


                service = new FSServiceClient(new InstanceContext(callback));
                id = add ? (service.CreateAccount(tbLogin.Text, tbPassword.Text, tbMail.Text)) : (service.Login(tbLogin.Text, tbPassword.Text));
                name = tbLogin.Text;
                ClientRoomCore.Client = new Client() { Id = id, ClientName = tbLogin.Text };
                ClientRoomCore.Status = ClientStatus.Online;
                GetPlayers();
                UpdateChat();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            return id;
        }

        private void UpdateChat()
        {
            lbxChat.Items.Clear();
            var temp = service.GetChat().ToList<string>();
            foreach (var v in temp)
                lbxChat.Items.Add(v);
        }

        private void Callback_CreateTcpClient(string obj)
        {
            string str = (string)obj;
            lbxLOG.Items.Add("AnswerForRequest - object -> " + str);

            // parse to EndPoint
            string[] ipport = str.Split(':');

            int port;
            IPAddress IPAddr;
            if (!int.TryParse(ipport[1], out port))
            {
                MessageBox.Show("Неверный потр!");
                return;
            }

            if (!IPAddress.TryParse(ipport[0], out IPAddr))
            {
                MessageBox.Show("Неверный IP!");
                return;
            }

            IPEndPoint ipEndPoint = new IPEndPoint(IPAddr, port);


            //
            // TODO
            // ...
            try
            {
                new Task(() =>
                {
                    TcpClientInTask(ipEndPoint);
                }).Start();
            }
            catch (Exception err) { MessageBox.Show(err.Message); }


        }

        private void TcpClientInTask(IPEndPoint _ep)
        {
            if (staff == null)
            {
                MessageBox.Show("staff is null");
                return;
            }

            int seekBuf = 0;

            FileStream fs = null;
            BinaryReader br = null;
            try
            {
                TcpClient eclient = new TcpClient();
                eclient.Connect(_ep);
                NetworkStream writerStream = eclient.GetStream();
                BinaryFormatter format = new BinaryFormatter();
                byte[] buf = new byte[1024];
                int count;
                fs = new FileStream(staff.Path, FileMode.Open);
                br = new BinaryReader(fs);
                long k = fs.Length;//Размер файла.
                format.Serialize(writerStream, k.ToString());//Вначале передаём размер
                // seek поставить в нужную позицию 
                while ((count = br.Read(buf, 0, 1024)) > 0)
                {
                    format.Serialize(writerStream, buf); // А теперь в цикле по 1024 байта передаём файл
                    seekBuf++;
                }
            }
            catch (Exception err)
            {
                // ошибка при передачи файла в поток
                MessageBox.Show(err.Message, seekBuf.ToString());
                throw;
            }
            finally
            {
                br.Close();
                fs.Close();
            }
        }

        private void Callback_TcpListenerAccept(ClientContract clientt)
        {
            lbxLOG.Items.Add("sender - " + clientt.sender.ClientName + " & rec.-" + clientt.recipient.ClientName + " path: " + clientt.Path);
            lbxLOG.Items.Add("sender - " + clientt.sender.ClientName);

            // **********
            string ip = "127.0.0.1:42009";
            // **********

            service.AnswerForRequest(ip, clientt.recipient.Id);
            //lbxLOG.Items.Add("TCP Listener Accept Start! " + ip + " - " + clientt.recipient.ClientName);
            Log.Info("TCP Listener Accept Start! " + ip + " - " + clientt.recipient.ClientName);


            //
            // TODO
            // ...
            string[] ipport = ip.Split(':');

            int port;
            IPAddress IPAddr;
            if (!int.TryParse(ipport[1], out port))
            {
                MessageBox.Show("Неверный потр!");
                return;
            }

            if (!IPAddress.TryParse(ipport[0], out IPAddr))
            {
                MessageBox.Show("Неверный IP!");
                return;
            }

            try
            {
                new Task(() =>
                    {
                        ListenerAcceptInTask(IPAddr, port, clientt.Path);
                    }).Start();
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
            lbxLOG.Items.Add("TCP Listener Accept Start! --> SUCCESSFULY");
        }

        private void ListenerAcceptInTask(IPAddress _IPAddr, int _port, string _path)
        {
            FileStream fs = null;
            BinaryWriter bw = null;
            int i = 0;
            int count;
            try
            {
                string filename = System.IO.Path.GetFileName(_path);
                TcpListener clientListener = new TcpListener(_port);
                clientListener.Start();
                TcpClient client = clientListener.AcceptTcpClient();
                NetworkStream readerStream = client.GetStream();
                BinaryFormatter outformat = new BinaryFormatter();
                fs = new FileStream(filename, FileMode.OpenOrCreate);
                // при дозаписи -> new FileStream(filename, FileMode.Append);
                bw = new BinaryWriter(fs);
                count = int.Parse(outformat.Deserialize(readerStream).ToString()); // Получаем размер файла
                for (; i < count; i += 1024)//Цикл пока не дойдём до конца файла
                {

                    byte[] buf = (byte[])(outformat.Deserialize(readerStream));//Собственно читаем из потока и записываем в файл
                    bw.Write(buf);
                    // добавить переменную сколько из потока записалось
                    // 
                }
            }
            catch (Exception)
            {
                MessageBox.Show(i.ToString());
                throw;
            }
            finally
            {
                bw.Close();
                fs.Close();
                Thread.Sleep(5000);
            }
            
        }

        private void UpdateClientsList()
        {
            lbxPaths.Items.Clear();
            lbxClients.Items.Clear();
            foreach (var p in ClientRoomCore.Clients.Clients.Values)
            {
                lbxClients.Items.Add(p.ClientName);
            }
        }

        private void btnCancelAddPerson_Click(object sender, RoutedEventArgs e)
        {
            btnAddCollapsed(false);
            TbClear();
        }

        private void TbClear()
        {
            tbLogin.Clear();
            tbPassword.Clear();
            tbMail.Clear();
        }

        private void LbxClear()
        {
            lbxChat.Items.Clear();
            lbxClients.Items.Clear();
            lbxPaths.Items.Clear();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Login person in DB
            if (tbLogin.Text == "")
            {
                MessageBox.Show("Input login...");
                return;
            }
            else if (tbPassword.Text == "")
            {
                MessageBox.Show("Input password...");
                return;
            }

            if (ClientRoomCore.Status == ClientStatus.Offline)
            {
                if (Connect(false) < 0)
                {
                    MessageBox.Show("Login or password is incorrect!");
                    return;
                }
                WhenLoginSuccess();
            }
        }

        private void Disconnect()
        {
            try
            {
                service.Logout(ClientRoomCore.Client.Id);
                ClientRoomCore.Status = ClientStatus.Offline;
                service.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void GetPlayers()
        {
            try
            {
                ClientRoomCore.Clients = service.GetClientOnline();
                UpdateClientsList();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            Disconnect();
            ToLoginView();
        }

        private void lbxClients_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                ListBox lb = (ListBox)sender;
                if (lb.SelectedValue.ToString() == ClientRoomCore.Client.ClientName.ToString())
                {
                    btnUpload.Visibility = Visibility.Visible;
                    btnDownload.Visibility = Visibility.Hidden;
                    ShowPaths(name);
                }
                else
                {
                    btnUpload.Visibility = Visibility.Hidden;
                    btnDownload.Visibility = Visibility.Visible;
                    ShowPaths(lb.SelectedValue.ToString());
                }
            }
            catch { }
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            // TODO
            // callback init question to download
            // ***

            string username = lbxClients.SelectedItem.ToString();
            string path = lbxPaths.SelectedItem.ToString();

            lbxLOG.Items.Add("Скачать у user - " + lbxClients.SelectedItem);
            lbxLOG.Items.Add("Файл path - " + lbxPaths.SelectedItem);

            var client = ClientRoomCore.Clients.Clients.FirstOrDefault(c => c.Value.ClientName == username);
            staff = new ClientContract() { sender = client.Value, recipient = new Client() { Id = id, ClientName = name }, Path = path };

            // RequestForDownload
            service.RequestFoDownload(staff);
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            // bool? result = dlg.ShowDialog();
            if (result == true)
            {
                if (!AddPathToDB(dlg.FileName, name))
                {
                    MessageBox.Show("Somthing went wrong.");
                    return;
                }
                else
                {
                    MessageBox.Show("The Addition complite.");
                    ShowPaths(name);
                }
            }
        }

        private void ShowPaths(string _name)
        {
            try
            {
                List<string> lb = service.GetPaths(_name).ToList<string>();
                lbxPaths.Items.Clear();
                foreach (var v in lb)
                    lbxPaths.Items.Add(v);
            }
            catch (Exception err)
            {
                MessageBox.Show("Somthing went wrong.  " + err.Message);
                throw;
            }
        }

        private bool AddPathToDB(string _filename, string _name)
        {
            bool b = false;
            try
            {
                b = service.AddPath(_filename, _name);
            }
            catch (Exception err)
            {
                MessageBox.Show("Somthing went wrong.  " + err.Message);
                throw;
            }
            return b;
        }

        private void btnChat_Click(object sender, RoutedEventArgs e)
        {
            service.SendMessageToChat(id, tbChat.Text);
            tbChat.Clear();
            UpdateChat();
        }

    }
}
