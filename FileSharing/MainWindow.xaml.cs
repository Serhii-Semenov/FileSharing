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
using System.ComponentModel;

namespace FileSharing
{
    public partial class MainWindow : Window
    {
        List<ClientContract> forDownLoad;
        BackgroundWorker worker;
        TcpListener recipientListener;

        private int id { get; set; }
        private string name { get; set; }
        public int Port { get; set; }

        FSServiceClient service;
        ILogger Log;

        private bool LogInfo { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            ToLoginView();

            // Initialize Logger
            LogInfo = true;
            Log = WPFLogger.Instance;
            WPFLogger.Instance.Initialize((ListBox)lbxLOG);

            // Initialize worker
            worker = new BackgroundWorker(); // variable declared in the class
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            // PORT
            Port = 42009;
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Title += " DONE";
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int j = 0; j <= 100; j++)
            {
                worker.ReportProgress(j);
                Title += j.ToString();
                Thread.Sleep(50);
            }
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // searchProgressBar.Value = e.ProgressPercentage;
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

                    Label lblClient = new Label() { Content = v.recipient.ClientName };
                    lblClient.Width = 40;
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
                        Width = 100,
                        Minimum = 0,
                        Maximum = 100,
                        Value = v.complite
                    };
                    wp.Children.Add(pbComplite);

                    Button btnResume = new Button()
                    {
                        Width = 70,
                        Content = "Resume file",
                        Margin = new Thickness(5),
                        Tag = i
                    };


                    btnResume.Click += BtnResume_Click;
                    wp.Children.Add(btnResume);

                    i++;
                    lbxDownloading.Items.Add(wp);
                }
                if (forDownLoad.Count > 0) exDownloading.IsExpanded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void BtnResume_Click(object sender, RoutedEventArgs e)
        {
            var i = (int)((Button)sender).Tag;
            ClientContract cl = new ClientContract();
            cl = forDownLoad[i];

            service.RequestFoDownload(cl);
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
                InitForDownload();
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

        private void Callback_CreateTcpClient(string addr, ClientContract cl)
        {
            Log.Debug(string.Format("Callback_CreateTcpClient({0}, {1}) ", addr, ClientContractToString(cl)));

            // parse to EndPoint
            string[] ipport = addr.Split(':');

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

            try
            {
                new Task(() =>
                {
                    TcpClientInTask(ipEndPoint, cl);
                }).Start();
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }



        /// <summary>
        /// Sender TCP Client
        /// </summary>
        /// <param name="_ep">End point</param>
        /// <param name="cl">Client Contract</param>
        private void TcpClientInTask(IPEndPoint _ep, ClientContract cl)
        {
            DispachLog("TcpClientInTask(inside)" + _ep.ToString() + " " + cl.ToString());

            TcpClient senderTcpClient = null;
            FileStream fs = null;
            BinaryReader br = null;
            byte[] buf = new byte[1024];
            long k = 0;

            try
            {
                senderTcpClient = new TcpClient();
                senderTcpClient.Connect(_ep);
                DispachLog("TcpClient.Connect " + senderTcpClient.Client.RemoteEndPoint);
                NetworkStream writerStream = senderTcpClient.GetStream();
                
                int count;
                DispachLog("Open file");
                fs = new FileStream(cl.Path, FileMode.Open);

                DispachLog("br = new BinaryReader(fs)");
                br = new BinaryReader(fs);

                k = fs.Length; // Размер файла
                DispachLog(" File Length = " + k.ToString());

                long position = fs.Seek(cl.complite, SeekOrigin.Begin);
                DispachLog("File Seek -> " + position.ToString());

                DispachLog("Start read from file");
                while((count = br.Read(buf, 0, 1024)) > 0)
                {
                    DispachLog("Readed from file: " + count);
                    //br.Read(buf, 0, buf.Length);
                    DispachLog("Try write to net: ");
                    writerStream.Write(buf, 0, count);
                    DispachLog("Write to net done");
                   
                }
                DispachLog("TcpClientInTask(END)");
            }
            catch (Exception err)
            {
                DispachLog(err.Message);
                // ошибка при передачи файла в поток
                // MessageBox.Show(err.Message, seekBite.ToString());
                // throw;
            }
            finally
            {
                //staff.sizecomplite = seekBite; // записанно байт
                //DispachLog(" seekBite -> " + seekBite.ToString());
                //staff.size = k;
                //staff.complite = (int)(seekBite / (k / (100)));

                //service.UpdateFileForDownload(cl);

                br.Close();
                fs.Close();
            }
        }

        private void Callback_TcpListenerAccept(ClientContract cl)
        {
            // 
            Log.Info("This is sender - " + cl.sender.ClientName);
            Log.Info("I will try to send file for -> " + cl.recipient.ClientName);
            Log.Info(" path: " + cl.Path);

            string ip = Dns.Resolve(Dns.GetHostName()).AddressList.Last() + ":" + Port++.ToString();
            Log.Info("Мой IP - " + ip);

            service.AnswerForRequest(ip, cl);
            Log.Info("TCP Listener Accept Start! " + ip + " - " + cl.recipient.ClientName);

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
                        ListenerAcceptInTask(IPAddr, port, cl);
                    }).Start();
                Log.Debug("Callback_TcpListenerAccept " + IPAddr.ToString() + " " + port.ToString()
                    + " " + ClientContractToString(cl));
            }
            catch (Exception err) { MessageBox.Show(err.Message); }

        }



        /// <summary>
        /// Recipient TcpListener
        /// </summary>
        /// <param name="_IPAddr"></param>
        /// <param name="_port"></param>
        /// <param name="cl"></param>
        private void ListenerAcceptInTask(IPAddress _IPAddr, int _port, ClientContract cl)
        {
            DispachLog("ListenerAcceptInTask(inside) " + _IPAddr.ToString() + " " + _port.ToString() + " " + ClientContractToString(cl));

            FileStream fs = null;
            BinaryWriter bw = null;
            byte[] buf = new byte[1024];

            try
            {
                string filename = System.IO.Path.GetFileName(cl.Path);

                recipientListener = new TcpListener(_IPAddr, _port);

                recipientListener.Start();
                TcpClient recipient = recipientListener.AcceptTcpClient();
                DispachLog("Connect done from: " + recipient.Client.RemoteEndPoint);

                NetworkStream readerStream = recipient.GetStream();
                fs = cl.sizecomplite == 0 ?
                    new FileStream(filename, FileMode.OpenOrCreate) :
                    new FileStream(filename, FileMode.Append);

                bw = new BinaryWriter(fs);

                DispachLog("Try to read from net");

                while (readerStream.CanRead)
                {
                    DispachLog("Begin read from net");
                    int readedBytes = readerStream.Read(buf, 0, buf.Length);
                    DispachLog("Readed from net: " + readedBytes);
                    bw.Write(buf, 0, readedBytes);
                    DispachLog("Writed in file done"); 
                }
                DispachLog("ListenerAcceptInTask(END)");

            }
            catch (Exception ex)
            {
                DispachLog(ex.Message);
               // MessageBox.Show(ex.Message, i.ToString());
                //throw;
            }
            finally
            {

                DispachLog("FINNALY ListenerAcceptInTask");
                // здесь записывать сколько записалось
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
            string path, senderName;  // sender params
            try
            {
                senderName = lbxClients.SelectedItem.ToString();
                path = lbxPaths.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            Log.Info(string.Format("Скачать у пользователя - {0}", senderName));
            Log.Info(string.Format("Файл - {0}", path));

            var client = ClientRoomCore.Clients.Clients.FirstOrDefault(c => c.Value.ClientName == senderName);
            ClientContract cl = new ClientContract()
            {
                sender = client.Value,
                recipient = new Client()
                {
                    Id = id,
                    ClientName = name
                },
                Path = path,
                sizecomplite = 0,
                size = 0,
                complite = 0
            };

            // Add staff to DB
            //staff.id = service.AddFileToDownloadTable(staff);
            Log.Debug("btnDownload_Click()");
            //Log.Debug(string.Format("service.AddFileToDownloadTable({0})", ClToString(staff)));

            // RequestForDownload
            service.RequestFoDownload(cl);
            Log.Debug(string.Format("service.RequestFoDownload({0})", ClientContractToString(cl)));
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

        private string ClientContractToString(ClientContract cl)
        {
            return string.Format("[{0}]S:({1}){2}; R:({3}){4}; SIZE:{5}; PATH:{6}",
                cl.id, cl.sender.Id, cl.sender.ClientName, cl.recipient.Id, cl.recipient.ClientName, cl.size, cl.Path);
        }

        private void DispachLog(string s)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                Log.Debug(s);
            }));
        }

    }
}
