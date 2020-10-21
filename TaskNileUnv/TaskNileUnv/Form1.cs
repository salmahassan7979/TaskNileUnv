using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace TaskNileUnv
{
    public partial class Form1 : Form
    {
        SerialPort port = null;
        String data_rx = " ";
        int data_tx = 60;
        int num;
        bool flag_rx = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Send_Click(object sender, EventArgs e)
        {
            
            send();
        }
        private void send()
        {
            try
            {
                port.Write("@" + textBox1.Text + ";");
                data_tx = Int32.Parse(textBox1.Text);

            }
            catch (Exception e)
            {

            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                send();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            connect();
        }
        private void connect()
        {
            port = new SerialPort(comboBox1.SelectedItem.ToString());
            port.DataReceived += new SerialDataReceivedEventHandler(data_rx_handler);
            port.BaudRate = 9600;
            port.DataBits = 8;
            port.StopBits = StopBits.One;
            try
            {
                if (!port.IsOpen)
                {
                    port.Open();
                    label1.Text = "Connected";
                    label1.ForeColor = Color.Green;

                }
            }
            catch (Exception e)
            {
            }
        }
        private void data_rx_handler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port1 = (SerialPort)sender;
            String text = port1.ReadExisting();
            int idx_end = text.IndexOf(';');
            if ((idx_end >= 0) && flag_rx)
            {
                flag_rx = false;
                try
                {
                    data_rx += text.Substring(0, idx_end);
                   
                }
                catch (Exception m)
                {
                    data_rx = m.Message;
                }
            }
            if (flag_rx)
            {
                data_rx += text;
            }
            int idx_start = text.IndexOf('@');
            if (idx_start >= 0)
            {

                flag_rx = true;
                data_rx = "";
                if (text.Length > (idx_start + 1))
                {
                    data_rx += text.Substring((idx_start + 1), (text.Length - 1));
                    int idx = data_rx.IndexOf(';');
                    if (idx >= 0)
                    {
                        data_rx = data_rx.Substring(0, idx);
                    }
                }
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
