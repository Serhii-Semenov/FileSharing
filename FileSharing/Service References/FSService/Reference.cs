﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FileSharing.FSService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ClientList", Namespace="http://schemas.datacontract.org/2004/07/FSServer.Model")]
    [System.SerializableAttribute()]
    public partial class ClientList : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<int, FileSharing.FSService.Client> ClientsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<int, FileSharing.FSService.Client> Clients {
            get {
                return this.ClientsField;
            }
            set {
                if ((object.ReferenceEquals(this.ClientsField, value) != true)) {
                    this.ClientsField = value;
                    this.RaisePropertyChanged("Clients");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Client", Namespace="http://schemas.datacontract.org/2004/07/FSServer.Model")]
    [System.SerializableAttribute()]
    public partial class Client : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClientNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ClientName {
            get {
                return this.ClientNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ClientNameField, value) != true)) {
                    this.ClientNameField = value;
                    this.RaisePropertyChanged("ClientName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ClientContract", Namespace="http://schemas.datacontract.org/2004/07/FSServer.Model")]
    [System.SerializableAttribute()]
    public partial class ClientContract : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PathField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int compliteField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int idField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private FileSharing.FSService.Client recipientField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private FileSharing.FSService.Client senderField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long sizeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long sizecompliteField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Path {
            get {
                return this.PathField;
            }
            set {
                if ((object.ReferenceEquals(this.PathField, value) != true)) {
                    this.PathField = value;
                    this.RaisePropertyChanged("Path");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int complite {
            get {
                return this.compliteField;
            }
            set {
                if ((this.compliteField.Equals(value) != true)) {
                    this.compliteField = value;
                    this.RaisePropertyChanged("complite");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int id {
            get {
                return this.idField;
            }
            set {
                if ((this.idField.Equals(value) != true)) {
                    this.idField = value;
                    this.RaisePropertyChanged("id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public FileSharing.FSService.Client recipient {
            get {
                return this.recipientField;
            }
            set {
                if ((object.ReferenceEquals(this.recipientField, value) != true)) {
                    this.recipientField = value;
                    this.RaisePropertyChanged("recipient");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public FileSharing.FSService.Client sender {
            get {
                return this.senderField;
            }
            set {
                if ((object.ReferenceEquals(this.senderField, value) != true)) {
                    this.senderField = value;
                    this.RaisePropertyChanged("sender");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long size {
            get {
                return this.sizeField;
            }
            set {
                if ((this.sizeField.Equals(value) != true)) {
                    this.sizeField = value;
                    this.RaisePropertyChanged("size");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long sizecomplite {
            get {
                return this.sizecompliteField;
            }
            set {
                if ((this.sizecompliteField.Equals(value) != true)) {
                    this.sizecompliteField = value;
                    this.RaisePropertyChanged("sizecomplite");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="FSService.IFSService", CallbackContract=typeof(FileSharing.FSService.IFSServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IFSService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/CreateAccount", ReplyAction="http://tempuri.org/IFSService/CreateAccountResponse")]
        int CreateAccount(string _login, string _password, string _mail);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/CreateAccount", ReplyAction="http://tempuri.org/IFSService/CreateAccountResponse")]
        System.Threading.Tasks.Task<int> CreateAccountAsync(string _login, string _password, string _mail);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/Login", ReplyAction="http://tempuri.org/IFSService/LoginResponse")]
        int Login(string _login, string _password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/Login", ReplyAction="http://tempuri.org/IFSService/LoginResponse")]
        System.Threading.Tasks.Task<int> LoginAsync(string _login, string _password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/Logout", ReplyAction="http://tempuri.org/IFSService/LogoutResponse")]
        void Logout(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/Logout", ReplyAction="http://tempuri.org/IFSService/LogoutResponse")]
        System.Threading.Tasks.Task LogoutAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/GetClientOnline", ReplyAction="http://tempuri.org/IFSService/GetClientOnlineResponse")]
        FileSharing.FSService.ClientList GetClientOnline();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/GetClientOnline", ReplyAction="http://tempuri.org/IFSService/GetClientOnlineResponse")]
        System.Threading.Tasks.Task<FileSharing.FSService.ClientList> GetClientOnlineAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/AddPath", ReplyAction="http://tempuri.org/IFSService/AddPathResponse")]
        bool AddPath(string _path, string _name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/AddPath", ReplyAction="http://tempuri.org/IFSService/AddPathResponse")]
        System.Threading.Tasks.Task<bool> AddPathAsync(string _path, string _name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/GetPaths", ReplyAction="http://tempuri.org/IFSService/GetPathsResponse")]
        string[] GetPaths(string _name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/GetPaths", ReplyAction="http://tempuri.org/IFSService/GetPathsResponse")]
        System.Threading.Tasks.Task<string[]> GetPathsAsync(string _name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/GetChat", ReplyAction="http://tempuri.org/IFSService/GetChatResponse")]
        string[] GetChat();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/GetChat", ReplyAction="http://tempuri.org/IFSService/GetChatResponse")]
        System.Threading.Tasks.Task<string[]> GetChatAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/SendMessageToChat", ReplyAction="http://tempuri.org/IFSService/SendMessageToChatResponse")]
        void SendMessageToChat(int _id, string _message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/SendMessageToChat", ReplyAction="http://tempuri.org/IFSService/SendMessageToChatResponse")]
        System.Threading.Tasks.Task SendMessageToChatAsync(int _id, string _message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/RequestFoDownload", ReplyAction="http://tempuri.org/IFSService/RequestFoDownloadResponse")]
        void RequestFoDownload(FileSharing.FSService.ClientContract cl);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/RequestFoDownload", ReplyAction="http://tempuri.org/IFSService/RequestFoDownloadResponse")]
        System.Threading.Tasks.Task RequestFoDownloadAsync(FileSharing.FSService.ClientContract cl);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/AnswerForRequest", ReplyAction="http://tempuri.org/IFSService/AnswerForRequestResponse")]
        void AnswerForRequest(string ip, int _id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/AnswerForRequest", ReplyAction="http://tempuri.org/IFSService/AnswerForRequestResponse")]
        System.Threading.Tasks.Task AnswerForRequestAsync(string ip, int _id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/GetListForDownload", ReplyAction="http://tempuri.org/IFSService/GetListForDownloadResponse")]
        FileSharing.FSService.ClientContract[] GetListForDownload(string _name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/GetListForDownload", ReplyAction="http://tempuri.org/IFSService/GetListForDownloadResponse")]
        System.Threading.Tasks.Task<FileSharing.FSService.ClientContract[]> GetListForDownloadAsync(string _name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/AddFileToDownloadTable", ReplyAction="http://tempuri.org/IFSService/AddFileToDownloadTableResponse")]
        int AddFileToDownloadTable(FileSharing.FSService.ClientContract cl);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/AddFileToDownloadTable", ReplyAction="http://tempuri.org/IFSService/AddFileToDownloadTableResponse")]
        System.Threading.Tasks.Task<int> AddFileToDownloadTableAsync(FileSharing.FSService.ClientContract cl);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/UpdateFileForDownload", ReplyAction="http://tempuri.org/IFSService/UpdateFileForDownloadResponse")]
        void UpdateFileForDownload(FileSharing.FSService.ClientContract cl);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/UpdateFileForDownload", ReplyAction="http://tempuri.org/IFSService/UpdateFileForDownloadResponse")]
        System.Threading.Tasks.Task UpdateFileForDownloadAsync(FileSharing.FSService.ClientContract cl);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFSServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFSService/UpdateClientList")]
        void UpdateClientList(FileSharing.FSService.ClientList clients);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFSService/UpdateChat")]
        void UpdateChat(FileSharing.FSService.ClientList clients);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFSService/TcpListenerAccept", ReplyAction="http://tempuri.org/IFSService/TcpListenerAcceptResponse")]
        string TcpListenerAccept(FileSharing.FSService.ClientContract cl);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFSService/CreateTcpClient")]
        void CreateTcpClient(string address);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFSServiceChannel : FileSharing.FSService.IFSService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FSServiceClient : System.ServiceModel.DuplexClientBase<FileSharing.FSService.IFSService>, FileSharing.FSService.IFSService {
        
        public FSServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public FSServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public FSServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public FSServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public FSServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public int CreateAccount(string _login, string _password, string _mail) {
            return base.Channel.CreateAccount(_login, _password, _mail);
        }
        
        public System.Threading.Tasks.Task<int> CreateAccountAsync(string _login, string _password, string _mail) {
            return base.Channel.CreateAccountAsync(_login, _password, _mail);
        }
        
        public int Login(string _login, string _password) {
            return base.Channel.Login(_login, _password);
        }
        
        public System.Threading.Tasks.Task<int> LoginAsync(string _login, string _password) {
            return base.Channel.LoginAsync(_login, _password);
        }
        
        public void Logout(int id) {
            base.Channel.Logout(id);
        }
        
        public System.Threading.Tasks.Task LogoutAsync(int id) {
            return base.Channel.LogoutAsync(id);
        }
        
        public FileSharing.FSService.ClientList GetClientOnline() {
            return base.Channel.GetClientOnline();
        }
        
        public System.Threading.Tasks.Task<FileSharing.FSService.ClientList> GetClientOnlineAsync() {
            return base.Channel.GetClientOnlineAsync();
        }
        
        public bool AddPath(string _path, string _name) {
            return base.Channel.AddPath(_path, _name);
        }
        
        public System.Threading.Tasks.Task<bool> AddPathAsync(string _path, string _name) {
            return base.Channel.AddPathAsync(_path, _name);
        }
        
        public string[] GetPaths(string _name) {
            return base.Channel.GetPaths(_name);
        }
        
        public System.Threading.Tasks.Task<string[]> GetPathsAsync(string _name) {
            return base.Channel.GetPathsAsync(_name);
        }
        
        public string[] GetChat() {
            return base.Channel.GetChat();
        }
        
        public System.Threading.Tasks.Task<string[]> GetChatAsync() {
            return base.Channel.GetChatAsync();
        }
        
        public void SendMessageToChat(int _id, string _message) {
            base.Channel.SendMessageToChat(_id, _message);
        }
        
        public System.Threading.Tasks.Task SendMessageToChatAsync(int _id, string _message) {
            return base.Channel.SendMessageToChatAsync(_id, _message);
        }
        
        public void RequestFoDownload(FileSharing.FSService.ClientContract cl) {
            base.Channel.RequestFoDownload(cl);
        }
        
        public System.Threading.Tasks.Task RequestFoDownloadAsync(FileSharing.FSService.ClientContract cl) {
            return base.Channel.RequestFoDownloadAsync(cl);
        }
        
        public void AnswerForRequest(string ip, int _id) {
            base.Channel.AnswerForRequest(ip, _id);
        }
        
        public System.Threading.Tasks.Task AnswerForRequestAsync(string ip, int _id) {
            return base.Channel.AnswerForRequestAsync(ip, _id);
        }
        
        public FileSharing.FSService.ClientContract[] GetListForDownload(string _name) {
            return base.Channel.GetListForDownload(_name);
        }
        
        public System.Threading.Tasks.Task<FileSharing.FSService.ClientContract[]> GetListForDownloadAsync(string _name) {
            return base.Channel.GetListForDownloadAsync(_name);
        }
        
        public int AddFileToDownloadTable(FileSharing.FSService.ClientContract cl) {
            return base.Channel.AddFileToDownloadTable(cl);
        }
        
        public System.Threading.Tasks.Task<int> AddFileToDownloadTableAsync(FileSharing.FSService.ClientContract cl) {
            return base.Channel.AddFileToDownloadTableAsync(cl);
        }
        
        public void UpdateFileForDownload(FileSharing.FSService.ClientContract cl) {
            base.Channel.UpdateFileForDownload(cl);
        }
        
        public System.Threading.Tasks.Task UpdateFileForDownloadAsync(FileSharing.FSService.ClientContract cl) {
            return base.Channel.UpdateFileForDownloadAsync(cl);
        }
    }
}
