using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using System.Net;
//using System.Net.Sockets;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SignIn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(nameText.Text))
            {
                MessageBox.Show("请输入你的姓名！");
                nameText.Focus();
                return;
            }
            if (String.IsNullOrEmpty(ipText.Text))
            {
                MessageBox.Show("请输入对方的IP！");
                ipText.Focus();
                return;
            }

            //TcpClient client;
            //client = new TcpClient();

            //try
            //{
            //    IPAddress ip = IPAddress.Parse(ipText.Text);//{ 111, 186, 100, 46 }
            //    client.Connect(ip, 8500);
            //    client.Close();
            //}
            //catch
            //{
            //    IPAddress[] localIPs;
            //    localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            //    foreach (IPAddress ip in localIPs)
            //    {
            //        if (ip.AddressFamily == AddressFamily.InterNetwork)
            //        ip1 = ip.ToString();
            //        TcpListener listener = new TcpListener(IPAddress.Parse(ip1), 8500);
            //        listener.Start();
            //        TcpClient remoteClient = listener.AcceptTcpClient();
            //        listener.Stop();
            //        remoteClient.Close();
            //    }
                
                
                
                //IPAddress ip = IPAddress.Parse(ipText.Text);//{ 111, 186, 100, 46 }
                //TcpListener listener = new TcpListener(ip, 8500);
                //listener.Start();
                //TcpClient remoteClient = listener.AcceptTcpClient();
                //listener.Stop();
            //}

            Form main = new PrimaryForm(nameText.Text, ipText.Text);
            main.Show();
            this.Hide();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Form1_Closing(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
