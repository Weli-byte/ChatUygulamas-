using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Lütfen bağlanmadan önce bir kullanıcı adı girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                client = new TcpClient("127.0.0.1", 8888);
                stream = client.GetStream();

               
                byte[] usernameBuffer = Encoding.UTF8.GetBytes(txtUsername.Text);
                stream.Write(usernameBuffer, 0, usernameBuffer.Length);

                receiveThread = new Thread(ReceiveMessages);
                receiveThread.IsBackground = true;
                receiveThread.Start();

                btnConnect.Enabled = false;
                txtUsername.ReadOnly = true;

                
                UpdateChatBox("Sunucuya başarıyla bağlandı.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sunucuya bağlanılamadı: " + ex.Message, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReceiveMessages()
        {
            byte[] buffer = new byte[2048];
            int byteCount;
            try
            {
                while ((byteCount = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, byteCount);

                    
                    if (message.StartsWith("USERLIST:"))
                    {
                        
                        string userListStr = message.Substring(9);
                        string[] users = userListStr.Split(',');
                        UpdateUserList(users);
                    }
                    else
                    {
                        
                        UpdateChatBox(message);
                    }
                }
            }
            catch { }
        }

        
        private void UpdateUserList(string[] users)
        {
            if (lstUsers.InvokeRequired)
            {
                lstUsers.Invoke(new Action<string[]>(UpdateUserList), new object[] { users });
            }
            else
            {
                lstUsers.Items.Clear(); 
                foreach (string user in users)
                {
                    if (!string.IsNullOrEmpty(user))
                    {
                        lstUsers.Items.Add(user); 
                    }
                }
            }
        }

        private void UpdateChatBox(string message)
        {
            if (txtChatBox.InvokeRequired)
            {
                txtChatBox.Invoke(new Action<string>(UpdateChatBox), message);
            }
            else
            {
                txtChatBox.AppendText(message + Environment.NewLine);
            }
        }

        
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (client != null && client.Connected && !string.IsNullOrEmpty(txtMessageBox.Text))
            {
                string formattedMessage = $"{txtUsername.Text}: {txtMessageBox.Text}";
                SendMessage(formattedMessage);

                txtMessageBox.Clear();
            }
        }

        private void SendMessage(string message)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                UpdateChatBox("Mesaj gönderilirken hata oluştu: " + ex.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (client != null)
            {
                client.Close();
            }
        }
    }
}