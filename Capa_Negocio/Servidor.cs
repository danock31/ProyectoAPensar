using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Capa_Negocio
{
    public class Servidor
    {
        public bool MensajeRecibido = true;
        public String MensajeMessage;
        private String MensajeServidor;
        public static System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        public string nickdejugador;
        public bool Reiniciar = false;
        public bool MensajeRecibidoBool { get { return MensajeRecibido; } }
        public String MensajeDeServeMessage { get { return MensajeMessage; } }
        public System.Net.Sockets.TcpClient SocketCliente { get { return clientSocket; } }

        public void ConectarServidor()
        {
            clientSocket.Connect("LocalHost", 8888);  //Se guarda primero el nombre del servido y despues el numero de host

        }
        public void EnviarMensaje(String MensajeClientetxt)
        {
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] MensajeCliente = System.Text.Encoding.ASCII.GetBytes(MensajeClientetxt + "$");
            serverStream.Write(MensajeCliente, 0, MensajeCliente.Length);
            serverStream.Flush();

        }

        public void EnviarSeleccion(string selec)
        {
            NetworkStream serverStream = clientSocket.GetStream();

            byte[] MensajeCliente = System.Text.Encoding.ASCII.GetBytes("Listo," + selec + "$");
            serverStream.Write(MensajeCliente, 0, MensajeCliente.Length);
            serverStream.Flush();

        }
        public void EnviarNick(string Nick)
        {
            NetworkStream serverStream = clientSocket.GetStream();

            byte[] MensajeCliente = System.Text.Encoding.ASCII.GetBytes(Nick + "$");
            serverStream.Write(MensajeCliente, 0, MensajeCliente.Length);
            serverStream.Flush();
            nickdejugador = Nick;

        }
        public void EnviarPuntuaje(int puntaje)
        {
            NetworkStream serverStream = clientSocket.GetStream();

            byte[] MensajeCliente = System.Text.Encoding.ASCII.GetBytes("Puntuacion" + "," + puntaje + "$");
            serverStream.Write(MensajeCliente, 0, MensajeCliente.Length);
            serverStream.Flush();

        }
        public void EnviarPeticionRecord()
        {
            NetworkStream serverStream = clientSocket.GetStream();

            byte[] MensajeCliente = System.Text.Encoding.ASCII.GetBytes("RECORD" + "$");
            serverStream.Write(MensajeCliente, 0, MensajeCliente.Length);
            serverStream.Flush();

        }
        public void EnviarPeticionGuardarRecord()
        {
            NetworkStream serverStream = clientSocket.GetStream();

            byte[] MensajeCliente = System.Text.Encoding.ASCII.GetBytes("GUARDARRECORD" + "$");
            serverStream.Write(MensajeCliente, 0, MensajeCliente.Length);
            serverStream.Flush();

        }
    }
}

