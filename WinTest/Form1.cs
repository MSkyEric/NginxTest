using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "主线程ID：" +Thread.CurrentThread.ManagedThreadId.ToString();
            //while (true)
            //{
            //    label1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss");
            //    Application.DoEvents();
            //    Thread.Sleep(1000);
            //}

            Thread td = new Thread(() =>
            {
                while (true)
                {
                    label1.Invoke(new MethodInvoker(() => label1.Text = "线程ID：" + Thread.CurrentThread.ManagedThreadId.ToString() + ", " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss")));
                    Thread.Sleep(200);
                }

            }) { IsBackground = true };
            td.Start();
            label2.Text += ", 子线程ID：" + td.ManagedThreadId.ToString();

        }
    }
}
