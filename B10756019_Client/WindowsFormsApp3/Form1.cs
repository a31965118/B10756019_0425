using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;         //匯入網路通訊協定相關函數
using System.Net.Sockets; //匯入網路插座功能函數

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Socket T;    
        string User; 
       
        private void button1_Click(object sender, EventArgs e)
        {
            string IP = textBox1.Text;                                 
            int Port = int.Parse(textBox2.Text);                       
            IPEndPoint EP = new IPEndPoint(IPAddress.Parse(IP), Port); 

            T = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            User = textBox3.Text;                                   
            try
            {
                T.Connect(EP);                       
                Send("0" + User);                     
            }
            catch (Exception)
            {
                MessageBox.Show("無法連上伺服器！");  
                return;
            }
            button1.Enabled = false;                  
        }
        
        private void Send(string Str)
        {
            byte[] B = Encoding.Default.GetBytes(Str);
            T.Send(B, 0, B.Length, SocketFlags.None); 
        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (button1.Enabled == false)
            {
                Send("9" + User); 
                T.Close();       
            }
        }
    }
}
