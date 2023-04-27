using _5NET.ViewModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace _5NET.Model
{
    class MetodLogic
    {
        private static Socket socket;
        private static List<Socket> Clients;
        private static string username = "noname";
        private static HashSet<string> set;
        public static void NewChat(EnterModel enterModel)
        {
            Defolt();
            username = enterModel.Name;
            MainViewModel1.MessageStory.Add(MessageFormat(username + " Create Chat"));
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(enterModel.Ip.Split(':')[0]), Convert.ToInt32(enterModel.Ip.Split(':')[1]));
            socket.Bind(ipPoint);
            socket.Listen(10);
            _ = ListenToClients();
        }
        public static void ConectChat(EnterModel enterModel)
        {
            Defolt();
            username = enterModel.Name;
            socket.ConnectAsync(enterModel.Ip.Split(':')[0], Convert.ToInt32(enterModel.Ip.Split(':')[1]));
            _ = RecieveMessage();
            SendMesage(username + " Include");
        }
        private static void Defolt()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Clients = new List<Socket>();
            set = new HashSet<string>();
        }
        private static async Task RecieveMessage(Socket? sockete=null)//общий
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                if (sockete is null)
                    await socket.ReceiveAsync(bytes, SocketFlags.None);
                else
                    await sockete.ReceiveAsync(bytes, SocketFlags.None);
                
                string message = Encoding.UTF8.GetString(bytes);
                    MainViewModel1.MessageStory.Add(MessageFormat(message));
                if (!(sockete is null))
                    foreach (var item in Clients)
                        _ = SendMessage(message, item);               
            }
        }
        private static async Task ListenToClients()//сервер
        {
            while (true)
            {
                var client=await socket.AcceptAsync();
                Clients.Add(client);
                _ = RecieveMessage(client);
            }
        }
        private static async Task SendMessage(string message, Socket? sockete=null)//общий
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            if (sockete is null)
                await socket.SendAsync(bytes, SocketFlags.None);
            else
                await sockete.SendAsync(bytes, SocketFlags.None);
        }
        public static void SendMesage(string message)
        {
            foreach (var item in Clients)
                _ = SendMessage(message, item);
            if (Clients.Count == 0)
                _ = SendMessage(message);
            else
                MainViewModel1.MessageStory.Add(MessageFormat(message));
        }
        private static string MessageFormat(string message)
        {

            set.Add(username);
            MainViewModel1.Clients.Clear();
            foreach (var item in set)
                MainViewModel1.Clients.Add(item);
            return "("+DateTime.Now+"): " + username + ": " + message;
        }
    }
    class EnterModel
    {
        public EnterModel(string Ip,string Name) {
            this.Name = Name;
            this.Ip = Ip;
        }
        public string Ip { get; set; }
        public string Name { get; set; }
    }
}
