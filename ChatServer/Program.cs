using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatServer
{
    class Program
    {
        static Dictionary<TcpClient, string> clients = new Dictionary<TcpClient, string>();
        static object lockObj = new object();

        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 8888);
            server.Start();
            Console.WriteLine("Sunucu başlatıldı. Port 8888 dinleniyor...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(client);
            }
        }

        static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            string username = null;

            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[2048];
                int byteCount = stream.Read(buffer, 0, buffer.Length);
                if (byteCount > 0)
                {
                    username = Encoding.UTF8.GetString(buffer, 0, byteCount);
                    lock (lockObj)
                    {
                        clients.Add(client, username);
                    }
                    Console.WriteLine($"{username} bağlandı.");
                    BroadcastMessage($"--- {username} sohbete katıldı. ---", client);
                    BroadcastUserList();
                }

                while ((byteCount = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string fullMessage = Encoding.UTF8.GetString(buffer, 0, byteCount);
                    Console.WriteLine($"Gelen Mesaj ({username}): {fullMessage}");

                   
                    int separatorIndex = fullMessage.IndexOf(": ");
                    string actualMessage = (separatorIndex > -1) ? fullMessage.Substring(separatorIndex + 2) : fullMessage;

                    if (actualMessage.Trim().StartsWith("/w "))
                    {
                        HandlePrivateMessage(actualMessage.Trim(), client, username);
                    }
                    else
                    {
                        BroadcastMessage(fullMessage, client);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"{username ?? "Bir istemci"} bağlantısı koptu.");
            }
            finally
            {
                if (username != null)
                {
                    BroadcastMessage($"--- {username} sohbetten ayrıldı. ---", client);
                }
                lock (lockObj)
                {
                    if (client != null) clients.Remove(client);
                }
                client.Close();
                BroadcastUserList();
            }
        }

        static void HandlePrivateMessage(string message, TcpClient sender, string senderUsername)
        {
            string[] parts = message.Split(new char[] { ' ' }, 3);
            if (parts.Length < 3)
            {
                SendMessageToClient(sender, "Sistem: Geçersiz özel mesaj formatı. Kullanım: /w <kullanıcı_adı> <mesaj>");
                return;
            }

            string targetUsername = parts[1];
            string privateMessage = parts[2];
            TcpClient targetClient = null;

            lock (lockObj)
            {
                targetClient = clients.FirstOrDefault(c => c.Value.Equals(targetUsername, StringComparison.OrdinalIgnoreCase)).Key;
            }

            if (targetClient != null)
            {
                string formattedMessage = $"(Özel Mesaj) {senderUsername}: {privateMessage}";
                SendMessageToClient(targetClient, formattedMessage);
                string confirmationMessage = $"(-> {targetUsername}) Özel Mesaj Gönderildi: {privateMessage}";
                SendMessageToClient(sender, confirmationMessage);
            }
            else
            {
                SendMessageToClient(sender, $"Sistem: '{targetUsername}' adlı kullanıcı bulunamadı veya çevrimdışı.");
            }
        }

        static void SendMessageToClient(TcpClient client, string message)
        {
            try
            {
                string fullMessage = $"{DateTime.Now:HH:mm} - {message}";
                byte[] buffer = Encoding.UTF8.GetBytes(fullMessage);
                client.GetStream().Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Tekil mesaj gönderilirken hata: {ex.Message}");
            }
        }

        static void BroadcastMessage(string message, TcpClient sender)
        {
            string fullMessage = $"{DateTime.Now:HH:mm} - {message}";
            byte[] buffer = Encoding.UTF8.GetBytes(fullMessage);
            lock (lockObj)
            {
                foreach (var c in clients.Keys)
                {
                    if (c != sender)
                    {
                        c.GetStream().Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }

        static void BroadcastUserList()
        {
            string userListMessage = "USERLIST:" + string.Join(",", clients.Values.ToArray());
            byte[] buffer = Encoding.UTF8.GetBytes(userListMessage);
            lock (lockObj)
            {
                foreach (var c in clients.Keys)
                {
                    c.GetStream().Write(buffer, 0, buffer.Length);
                }
            }
            Console.WriteLine("Kullanıcı listesi güncellendi ve yayınlandı.");
        }
    }
}