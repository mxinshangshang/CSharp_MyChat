using System;
//using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
//using System.Collections.Specialized;


namespace WindowsFormsApplication1
{
    public partial class PrimaryForm : Form
    {
        string name;
        string ip;
        private Thread Listen;
        private TcpListener tcpListener;
        private static string message = "";

        public struct Message
        {
            private readonly string userName;
            private readonly string content;
            private readonly DateTime postDate;

            public Message(string userName, string content)
            {
                this.userName = userName;
                this.content = content;
                this.postDate = DateTime.Now;
            }

            public Message(string content) : this("System", content) { }

            public string UserName
            {
                get { return userName; }
            }

            public string Content
            {
                get { return content; }
            }

            public DateTime PostDate
            {
                get { return postDate; }
            }

            public override string ToString()
            {
                return String.Format("{0}[{1}]：\r\n{2}\r\n", userName, postDate, content);
            }
        }      

        public PrimaryForm()
        {
            InitializeComponent();
        }

        public PrimaryForm(string name, string ip)
        {
            InitializeComponent();
            this.name = name;
            this.ip = ip;
        }

        private void PrimaryForm_Load(object sender, EventArgs e)
        {
            Listen = new Thread(new ThreadStart(this.StartListen));
            Listen.IsBackground = true;
            Listen.Start();
            timer1.Start();
        }
		// 开始多线程侦听
        private void StartListen()
        {
            byte[] buffer = new byte[8192];
            message = "";
            //IPAddress ipAddress = Dns.GetHostAddresses(Dns.GetHostName())[0];
            //IPEndPoint ipLocalEndPoint = new IPEndPoint(ipAddress, 8500);
            tcpListener = new TcpListener(IPAddress.Any,8500);//tcpListener = new TcpListener(ipLocalEndPoint);//
            tcpListener.Start();
            while (true)
            {
                //tcpListener.Start();
                TcpClient tcpclient = tcpListener.AcceptTcpClient();
                NetworkStream streamToClient = tcpclient.GetStream();
                int bytesRead = streamToClient.Read(buffer, 0, 8192);
                message = Encoding.Unicode.GetString(buffer, 0, bytesRead);
                txtContent.AppendText(message);//txtContent.Text += message;
                txtContent.ScrollToCaret();
                //streamToClient.Close();
                //tcpclient.Close();
                //tcpListener.Stop();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            if (this.tcpListener != null)
            {
                tcpListener.Stop();
            }
            if (Listen != null)
            {
                if (Listen.ThreadState == ThreadState.Running)
                {
                    Listen.Abort();
                }
            }
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtContent.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //TcpClient client1;
            //Stream streamToServer;
            try
            {
                TcpClient client = new TcpClient();
                IPAddress ip1 = IPAddress.Parse(ip);//{ 111, 186, 100, 46 }
                client.Connect(ip1, 8500);
                Stream streamToServer = client.GetStream();	// 获取连接至远程的流
                //lock (streamToServer)
                //{
                    Message msg=new Message(name, textBox1.Text);
                    byte[] buffer = Encoding.Unicode.GetBytes(msg.ToString());
                    streamToServer.Write(buffer, 0, buffer.Length);
                    streamToServer.Flush();
                    streamToServer.Close();
                    client.Close();
                    txtContent.AppendText(msg.ToString());//txtContent.Text += msg.ToString();
                    txtContent.ScrollToCaret();
                    textBox1.Clear();
                //}
                //streamToServer.Close();
                //client.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (message != "")
            {
                txtContent.AppendText(message);
                txtContent.ScrollToCaret();
                message = "";
            }
        }
        private void PrimaryForm_Closed(object sender, EventArgs e)
        {
            if (this.tcpListener != null)
            {
                tcpListener.Stop();
            }
            if (Listen != null)
            {
                if (Listen.ThreadState == ThreadState.Running)
                {
                    Listen.Abort();
                }
            }
            Application.Exit();
        }
    }
}
