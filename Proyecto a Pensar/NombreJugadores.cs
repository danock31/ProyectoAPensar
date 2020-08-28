using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Negocio;

namespace Proyecto_a_Pensar
{
    public partial class NombreJugadores : Form
    {
        public NombreJugadores()
        {
            InitializeComponent();
        }
        private void btnIniciarJuego_Click(object sender, EventArgs e)
        {
            Servidor cn = new Servidor();
            cn.ConectarServidor();

            cn.EnviarNick(textBox1.Text);
            Form2 abrir = new Form2();
            abrir.Show();
        }
    }
}
