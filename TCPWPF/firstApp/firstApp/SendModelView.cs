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

namespace firstApp
{

    public class SendViewModel: ViewModel
    {
        //create command
        public SendCommand MySendCommand { get; set; }

        public SendViewModel()
        {
            MySendCommand = new SendCommand(ExecuteSendMethod);//execute command
        }

        public void ExecuteSendMethod(object parameter)
        {
            //execute method will be passed to SendCommand
            if (client !=  null)//client is active
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("hello");
                //stream object to Write
                NetworkStream stream = client.GetStream();

                //Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);
            }
            else
            {   //non-blocking message when user tries to send when not connected
                MessageBox.Show("You are not connected");
            }
        }
    }
}
