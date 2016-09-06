using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileWatchDemo
{
    public partial class Form1 : Form
    {
        FileWatch m_filewatch = new FileWatch();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_filewatch.StartWatch();
            m_filewatch.SetCallBack(OnCallBack);
        }

        // Define the event handlers.
        void OnCallBack(object oInfo)
        {
            AddInfo(oInfo.ToString());
        }

        void AddInfo(string strInfo)
        {
            richTextBox1.Text += strInfo + "\r\n";
        }

  
    }
}
