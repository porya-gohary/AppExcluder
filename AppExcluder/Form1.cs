using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppExcluder
{
    public partial class Form1 : Form
    {
        private String Selected_Adapter;
        public System.Windows.Forms.ComboBox.ObjectCollection Items { get; }
        
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < net_adapters().Count; i++)
            {
                comboBox1.Items.Add(net_adapters()[i]);
            }
            
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Selected_Adapter = comboBox1.SelectedText;
            Console. WriteLine(Selected_Adapter);
            Console. WriteLine("Hello");
            //throw new System.NotImplementedException();
        }
        
        public List<String> net_adapters() 
        { 
            List<String> values = new List<String>();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces()) 
            { 
                values.Add(nic.Name);
            } 
            return values; 
        }
    }
    
    
}