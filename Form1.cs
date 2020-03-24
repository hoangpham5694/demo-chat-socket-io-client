using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Quobject.SocketIoClientDotNet.Client;

namespace TestSocket
{
    public partial class Form1 : Form
    {
        private string room = "default";
        public Socket socket;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
               socket = IO.Socket("http://192.168.1.44:3000/"); 
           // socket = IO.Socket("https://cacc5f9b.ngrok.io/");
            socket.On(Socket.EVENT_CONNECT, () =>
            {
              //  socket.Emit("send_message", "asd", "asddfff");

            });
            socket.On("receiver_data", (data) =>
            {
                //  Console.WriteLine(data);

                  dynamic item = JsonConvert.DeserializeObject(data.ToString());
                   Console.WriteLine(Convert.ToString(item.from) );
                //item.from + ": " + item.msg + Environment.NewLine
                txtLogMessage.Invoke((MethodInvoker)(() => txtLogMessage.AppendText(Convert.ToString(item.from) + ": " + Convert.ToString(item.msg) + Environment.NewLine)));
             //   txtMessage.AppendText("sdfdf");
            });

        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            
            socket.Emit("send_message", txtName.Text, txtMessage.Text, room);
        }

        private void btnJoinRoom_Click(object sender, EventArgs e)
        {
            socket.Emit("leave-room", room);
            this.room = txtRoom.Text;
            socket.Emit("room", room);
        }

        private void btnMyRoom_Click(object sender, EventArgs e)
        {
            socket.Emit("my-room");
        }
    }
}
