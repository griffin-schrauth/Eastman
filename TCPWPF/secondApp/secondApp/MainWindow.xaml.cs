using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.ComponentModel;
using System.Threading;



namespace secondApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker worker = new BackgroundWorker();
        public IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        public string data = null;
        public Int32 Serverport = 8000;
        TcpListener listener = null;
       
        public MainWindow()
        {
            InitializeComponent();
            //start server
            start_UpServer(localAddr, Serverport);
            worker.DoWork += worker_Dowork;
  
        }

        private void start_UpServer(IPAddress Addy, int pNum)
        {
            //background worker will constantly run in background
            worker.RunWorkerAsync();

        }

        private void worker_Dowork(object sender, DoWorkEventArgs e)
        {
            try
            {// create listener on port
                listener = new TcpListener(localAddr, Serverport);
                //start listener
                listener.Start();

                while (true)
                {
                    //server will listen for a client until one is accepted
                    TcpClient client = listener.AcceptTcpClient();
                    //write to text boxes from this thread
                    this.Dispatcher.Invoke(() =>
                    {
                        MessagetextBox.Clear();
                        MessagetextBox.AppendText("Connected\n");

                    });
                    
                    // Buffer for reading data
                    Byte[] bytes = new Byte[256];
                    //String data = null;
                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    try
                    {
                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            // Translate data bytes to a ASCII string.
                            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                            this.Dispatcher.Invoke(() =>
                            {
                                ChatScreentextBox.AppendText(data + "\n");
                            });

                        }

                        client.Close();

                        this.Dispatcher.Invoke(() =>
                        {
                            MessagetextBox.Clear();
                            MessagetextBox.AppendText("Disconnected");
                        });
                    }
                    catch(IOException ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                // Stop listening for new clients.              
                listener.Stop();
            }
        }      
    }
}