namespace ChatClient
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtChatBox = new System.Windows.Forms.TextBox();
            this.txtMessageBox = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lstUsers = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // txtChatBox
            // 
            this.txtChatBox.BackColor = System.Drawing.Color.DimGray;
            this.txtChatBox.Location = new System.Drawing.Point(25, 61);
            this.txtChatBox.Multiline = true;
            this.txtChatBox.Name = "txtChatBox";
            this.txtChatBox.ReadOnly = true;
            this.txtChatBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChatBox.Size = new System.Drawing.Size(633, 195);
            this.txtChatBox.TabIndex = 0;
            // 
            // txtMessageBox
            // 
            this.txtMessageBox.BackColor = System.Drawing.Color.DarkGray;
            this.txtMessageBox.Location = new System.Drawing.Point(25, 397);
            this.txtMessageBox.Name = "txtMessageBox";
            this.txtMessageBox.Size = new System.Drawing.Size(520, 22);
            this.txtMessageBox.TabIndex = 1;
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.Violet;
            this.btnSend.Location = new System.Drawing.Point(700, 396);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Gönder";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.Gainsboro;
            this.txtUsername.Location = new System.Drawing.Point(158, 21);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(405, 22);
            this.txtUsername.TabIndex = 3;
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.Aquamarine;
            this.btnConnect.Location = new System.Drawing.Point(700, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 32);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Bağlan";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(41, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Kullanıcı Adı:";
            // 
            // lstUsers
            // 
            this.lstUsers.FormattingEnabled = true;
            this.lstUsers.ItemHeight = 16;
            this.lstUsers.Location = new System.Drawing.Point(668, 61);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.Size = new System.Drawing.Size(120, 324);
            this.lstUsers.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lstUsers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessageBox);
            this.Controls.Add(this.txtChatBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtChatBox;
        private System.Windows.Forms.TextBox txtMessageBox;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstUsers;
    }
}

