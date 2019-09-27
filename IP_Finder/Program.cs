using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime;
using System.Threading;
using System.Xml;

namespace IP_Finder
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            String Selected_adapter;
            String Program_Address;
            String ForceIP_Address;
            String IP;
            Selected_adapter = args[0];
            //Console.WriteLine(Selected_adapter);
            ForceIP_Address = args[1];
            Program_Address = args[2];

            IP = getIP(Selected_adapter);
            String command = "\"" + ForceIP_Address + "\"" + " -i " + IP + " " + "\"" +
                             Program_Address + "\"";
            Console.WriteLine(command);


            //Asynchronously start the Thread to process the Execute command request.
            Thread objThread = new Thread(new ParameterizedThreadStart(ExecuteCommandSync));
            //Make the thread as background thread.
            objThread.IsBackground = true;
            //Set the Priority of the thread.
            objThread.Priority = ThreadPriority.Highest;
            //Start the thread.
            objThread.Start(command);
            System.Threading.Thread.Sleep(5000);
            
          
        }

        public static String getIP(String name)
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


        public static void ExecuteCommandSync(object command)
        {
            //Console.WriteLine("HELLO!");

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine(command + "");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }
    }
}