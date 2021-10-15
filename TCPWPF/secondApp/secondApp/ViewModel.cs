using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.ComponentModel;

namespace secondApp
{
    public class ViewModel : INotifyPropertyChanged
    {
        //create static variable so we can use in other view
        public static TcpClient client;
        //create stream to read and write
        public NetworkStream stream;

        //create command
        public Command MyCommand { get; set; } //for execution

        //create string
        string _content = "Connect";
        //get and set value for data bindind
        public string ConnectButton
        {
            get { return _content; }
            set { _content = value; }
        }

        //function to change status to Disconnect
        public void changeStatusToDis()
        {
            ConnectButton = "Disconnect";
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("ConnectButton"));
        }
        //function to change status to Connect
        public void changeStatusToCon()
        {
            ConnectButton = "Connect";
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("ConnectButton"));
        }

        public ViewModel()
        {
            MyCommand = new Command(ExecuteMethod, canExecuteMethod);
        }
        //return true to execute command
        private bool canExecuteMethod(object parameter)
        {
            return true;//dont really need this but for my purpose we do
        }
        //execute method 
        public void ExecuteMethod(object parameter)
        {
            ConnectToServer();
        }


        // will execute differently depending on status of connection
        public void ConnectToServer()
        {

            if (_content == "Connect" && client == null)
            {
                try
                {
                    // Create a TcpClient.                  
                    Int32 port = 88;
                    client = new TcpClient("127.0.0.1", port);

                    //change button content
                    changeStatusToDis();                   
                }
                catch (ArgumentNullException ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                catch (SocketException ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }//if content of button is disconnect
            else if (_content == "Disconnect")// && client != null)
            {//disconnect client and close stream
                if (client.Connected)
                {
                    client.Close();
                    if (stream != null)
                    {
                        stream.Close();
                        stream.Dispose();
                    }
                    client = null;
                    
                }//change content to connected
                changeStatusToCon();
            }
            else
            {
                //do nothing
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
    }


}

