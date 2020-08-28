using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Negocio;

namespace Proyecto_a_Pensar
{
    public partial class Form2 : Form
    {

        int contadordeclientes = 0;
        string cambiarnombre;
        string mensajeServidor;
        int puntostot;
        Servidor cn = new Servidor();
        int TamanioColumnasFilas = 4;
        int Movimientos = 0;
        int CantidadDeCartasVolteadas = 0;
        List<string> CartasEnumeradas;
        List<string> CartasRevueltas;
        ArrayList CartasSeleccionadas;
        PictureBox CartaTemporal1;
        PictureBox CartaTemporal2;
        int CartaActual = 0;

        public Form2()
        {
            InitializeComponent();
            iniciarJuego();
        }
        public void iniciarJuego() {
            timer1.Enabled = false;
            timer1.Stop();
            lblRecord.Text = "0";
            CantidadDeCartasVolteadas = 0;
            Movimientos = 0;
            PanelJuego.Controls.Clear();
            CartasEnumeradas= new List<string>();
            CartasRevueltas = new List<string>();
            CartasSeleccionadas = new ArrayList();
            for (int i = 0; i < 8; i++) {
                CartasEnumeradas.Add(i.ToString());
                CartasEnumeradas.Add(i.ToString());
            }
            var NumeroAleatorio = new Random();
            var Resultado = CartasEnumeradas.OrderBy(item=> NumeroAleatorio.Next());
            foreach(string ValorCarta in Resultado){
                CartasRevueltas.Add(ValorCarta);
            }
            var tablaPanel = new TableLayoutPanel();
            tablaPanel.RowCount = TamanioColumnasFilas;
            tablaPanel.ColumnCount = TamanioColumnasFilas;
            for (int i = 0; i < TamanioColumnasFilas;i++ )
            {
                var Porcentaje = 150f / (float)TamanioColumnasFilas - 10;
                tablaPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,Porcentaje));
                tablaPanel.RowStyles.Add(new RowStyle(SizeType.Percent,Porcentaje));
             }
            int contadorFichas = 1;

            for (var i = 0; i < TamanioColumnasFilas; i++)
            {
                for (var j = 0; j < TamanioColumnasFilas; j++)
                {
                    var CartasJuego = new PictureBox();
                    CartasJuego.Name = string.Format("{0}", contadorFichas);
                    CartasJuego.Dock = DockStyle.Fill;
                    CartasJuego.SizeMode = PictureBoxSizeMode.StretchImage;
                    CartasJuego.Image = Properties.Resources.reverso;
                    CartasJuego.Cursor = Cursors.Hand;
                    CartasJuego.Click += btnCarta_Click;
                    tablaPanel.Controls.Add(CartasJuego, j, i);
                    contadorFichas++;
                }
            }
            tablaPanel.Dock = DockStyle.Fill;
            PanelJuego.Controls.Add(tablaPanel);

        
        }
        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            iniciarJuego();
        }
        private void btnCarta_Click(object sender, EventArgs e)
        {
            if (CartasSeleccionadas.Count < 2) { 
            Movimientos++;
            //lblRecord.Text = Convert.ToString(Movimientos);
            var CartasSeleccionadasUsuario = (PictureBox)sender;

            CartaActual= Convert.ToInt32(CartasRevueltas[Convert.ToInt32(CartasSeleccionadasUsuario.Name)-1]);
            CartasSeleccionadasUsuario.Image = RecuperarImagen(CartaActual);
            CartasSeleccionadas.Add(CartasSeleccionadasUsuario);
                //  2 Veces se realizo el evento del click
                if(CartasSeleccionadas.Count==2){
                    CartaTemporal1 = (PictureBox)CartasSeleccionadas[0];
                    CartaTemporal2 = (PictureBox)CartasSeleccionadas[1];
                    int Carta1= Convert.ToInt32(CartasRevueltas[Convert.ToInt32(CartaTemporal1.Name)-1]);
                    int Carta2 = Convert.ToInt32(CartasRevueltas[Convert.ToInt32(CartaTemporal2.Name) - 1]);

                    if (Carta1 != Carta2)
                    {
                        timer1.Enabled = true;
                        timer1.Start();
                       
                    }
                    else {
                        CantidadDeCartasVolteadas++;
                        lblRecord.Text = Convert.ToString(CantidadDeCartasVolteadas);
                        if (CantidadDeCartasVolteadas>7){
                            //MessageBox.Show("El juego termino");
                            cn.EnviarSeleccion("Gano");
                        }
                        else
                        {
                            cn.EnviarSeleccion("NoTermino");
                        }
                        CartaTemporal1.Enabled = false; CartaTemporal2.Enabled = false;
                        if (CantidadDeCartasVolteadas == 0)
                        {
                            cn.EnviarSeleccion("0");
                        }
                        else if (CantidadDeCartasVolteadas == 1)
                        {
                            cn.EnviarSeleccion("1");
                        }
                        else if (CantidadDeCartasVolteadas == 2)
                        {
                            cn.EnviarSeleccion("2");
                        }
                        else if (CantidadDeCartasVolteadas == 3)
                        {
                            cn.EnviarSeleccion("3");
                        }
                        else if (CantidadDeCartasVolteadas == 4)
                        {
                            cn.EnviarSeleccion("4");
                        }
                        else if (CantidadDeCartasVolteadas == 5)
                        {
                            cn.EnviarSeleccion("5");
                        }
                        else if (CantidadDeCartasVolteadas == 6)
                        {
                            cn.EnviarSeleccion("6");
                        }
                        else if (CantidadDeCartasVolteadas == 7)
                        {
                            cn.EnviarSeleccion("7");

                        }

                        CartasSeleccionadas.Clear();
                        
                    }


                }
            }

        }
        public Bitmap RecuperarImagen(int NumeroImagen){
        Bitmap TmpImg= new Bitmap(200,100);
        switch (NumeroImagen) {
            case 0: TmpImg = Properties.Resources.Img11;
                break;
            default: TmpImg = (Bitmap)Properties.Resources.ResourceManager.GetObject("Img"+NumeroImagen);
                break;
        }
        return TmpImg;
        
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int TiempoVirarCarta = 1;
            if (TiempoVirarCarta == 1) { 
            CartaTemporal1.Image=Properties.Resources.reverso;
            CartaTemporal2.Image = Properties.Resources.reverso;
            CartasSeleccionadas.Clear();
            TiempoVirarCarta = 0;
            timer1.Stop();
            
            }
        }
        private void btnEnviarMensaje_Click(object sender, EventArgs e)
        {
            try
            {
                cn.EnviarMensaje(txtMensaje.Text);
            }
            catch
            {

                MessageBox.Show("EnviarMensaje");

            }
        }


        public void RecibirMensajes()
        {

            while (true)
            {
                NetworkStream serverStream = cn.SocketCliente.GetStream();

                byte[] inStream = new byte[10025];

                serverStream.Read(inStream, 0, 10024);
                mensajeServidor = System.Text.Encoding.ASCII.GetString(inStream);


                String[] buscarnombre = System.Text.RegularExpressions.Regex.Split(mensajeServidor, ",");
                string carta = buscarnombre[0];
                if (mensajeServidor.Contains("Unido"))
                {
                    String[] nombrejugador = System.Text.RegularExpressions.Regex.Split(mensajeServidor, " ");
                    string nombre = nombrejugador[0];
                    if (contadordeclientes == 0)
                    {
                        cambiarnombre = nombre;
                        nombre = cambiarnombre;
                        contadordeclientes++;

                    }
                }
                if (mensajeServidor.Contains(cambiarnombre))
                {
                    string nombre = cambiarnombre;
                if (mensajeServidor.Contains("Gano"))
                {
                    String[] nombrejugador = System.Text.RegularExpressions.Regex.Split(mensajeServidor,"");
                    if (nombre== cambiarnombre) 
                    {
                        
                        MessageBox.Show("Felicitaciones Ganaste!!! Jugador:"+ nombre);
                        puntostot += 1;
                     }
                    else
                    {
                            MessageBox.Show("Perdiste  Jugador" + cambiarnombre);
                    }
                }
                    if (lblRecord.InvokeRequired)
                        {
                            lblRecord.Invoke(new Action(() => lblRecord.Text = puntostot.ToString()));
                        }
                        else
                        {
                            lblRecord.Text = puntostot.ToString();

                        }


                    
                }
                if (mensajeServidor.Contains("Empate"))
                {
                    MessageBox.Show("EMPATE");
                }
                msg();
            }
        }
        private void msg()
        {
            if (InvokeRequired)
                this.Invoke(new MethodInvoker(msg));
            else
                txtChat.Text = txtChat.Text + Environment.NewLine + " >> " + mensajeServidor;
        }
       

        private void Form2_Load(object sender, EventArgs e)
        {
            Thread ctThread = new Thread(RecibirMensajes);
            ctThread.Start();
            msg();
        }

        private void btnEnviarMensaje_Click_2(object sender, EventArgs e)
        {
            try
            {
                cn.EnviarMensaje(txtMensaje.Text);
            }
            catch
            {

                MessageBox.Show("EnviarMensaje");

            }
        }
    }
}
