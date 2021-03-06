﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace AppExcluder
{
    public partial class Form1 : Form
    {
        private String Selected_Adapter;
        List<String> Adapters = new List<String>();
        string path;

        //Start CMD Window Hidden
        Process cmd = new Process();


        public Form1()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            Adapters = net_adapters();
            for (int i = 0; i < Adapters.Count; i++)
            {
                comboBox1.Items.Add(Adapters[i]);
            }
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void label1_Click(object sender, EventArgs e)
        {
          //  throw new System.NotImplementedException();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Selected_Adapter = comboBox1.SelectedItem.ToString();
            //Console. WriteLine(Selected_Adapter);
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

        private void button1_Click(object sender, EventArgs e)
        {
            //String command = "\"C:\\Program Files\\Mozilla Firefox\\firefox.exe\"";
            if (Selected_Adapter == null)
            {
                MessageBox.Show("Please Select A Network Adapter.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (label2.Text == "Not Selected!")
            {
                MessageBox.Show("Please Select A Application.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            String command = "\""+AppDomain.CurrentDomain.BaseDirectory+"ForceBindIP\\ForceBindIP64.exe"+"\""+" -i "+getIP(Selected_Adapter)+" "+"\""+
                label2.Text+"\"";
            Console.WriteLine(command);

            try
            {
                //Asynchronously start the Thread to process the Execute command request.
                Thread objThread = new Thread(new ParameterizedThreadStart(ExecuteCommandSync));
                //Make the thread as background thread.
                objThread.IsBackground = true;
                //Set the Priority of the thread.
                objThread.Priority = ThreadPriority.AboveNormal;
                //Start the thread.
                objThread.Start(command);
            }
            catch (ThreadStartException objException)
            {
                // Log the exception
            }

            String IP = getIP(Selected_Adapter);
        }

        public String getIP(String name)
        {
            String IP = null;
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                //Console.Write(nic.Name+"\n");
                if (nic.Name.ToString().Equals(name))
                {
                    foreach (UnicastIPAddressInformation ip in nic.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            Console.WriteLine(ip.Address.ToString());
                            IP = ip.Address.ToString();
                            return IP;
                        }
                    }
                    
                    
                }
            }
            
            return null;
        }

        public void ExecuteCommandSync(object command)
        {
          
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine(command+"");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
//            System.Diagnostics.ProcessStartInfo procStartInfo =
//                new System.Diagnostics.ProcessStartInfo("cmd",  command+"");
//            procStartInfo.RedirectStandardOutput = true;
//            procStartInfo.UseShellExecute = false;
//            
//            procStartInfo.CreateNoWindow = true;
//           
//            System.Diagnostics.Process proc = new System.Diagnostics.Process();
//            proc.StartInfo = procStartInfo;
//            proc.Start();
//           
//            string result = proc.StandardOutput.ReadToEnd();
//            
//            Console.WriteLine(result);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Applications (*.exe)|*.exe";
            dialog.Title = "Please select an Application.";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.FileName;
            }

            label2.Text = path;
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            //throw new System.NotImplementedException();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
           // throw new System.NotImplementedException();
        }
        
        public void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation)
        {
            string shortcutLocation = System.IO.Path.Combine(shortcutPath, shortcutName + ".lnk");
//            string run_shortcut = "\""+AppDomain.CurrentDomain.BaseDirectory + "IP_Finder.exe\" \"" + Selected_Adapter +"\" \""
//                                  +AppDomain.CurrentDomain.BaseDirectory+"ForceBindIP\\ForceBindIP64.exe"+"\" "+"\""+
//                                  label2.Text+"\"" ;
            string run_shortcut = AppDomain.CurrentDomain.BaseDirectory + "IP_Finder.exe";
            Console.WriteLine(run_shortcut);
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);
 
            shortcut.Description = "My shortcut description";   // The description of the shortcut
            shortcut.IconLocation = targetFileLocation;           // The icon of the shortcut
            shortcut.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            shortcut.Arguments = "\"" + Selected_Adapter +"\" \""
                                  +AppDomain.CurrentDomain.BaseDirectory+"ForceBindIP\\ForceBindIP64.exe"+"\" "+"\""+
                                  label2.Text+"\"" ;
            shortcut.TargetPath = run_shortcut;                 // The path of the file that will launch when the shortcut is run
            shortcut.Save();                                    // Save the shortcut
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Applications (*.exe)|*.exe";
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(label2.Text);
            saveFileDialog.Title = "Choose a place for Shortcut.";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (saveFileDialog.ShowDialog()==DialogResult.OK)
            {
                
                

                CreateShortcut(Path.GetFileNameWithoutExtension(saveFileDialog.FileName), Path.GetDirectoryName(saveFileDialog.FileName),label2.Text);
            }
            //throw new System.NotImplementedException();
        }
    }
}