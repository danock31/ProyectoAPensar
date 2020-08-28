using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Proyecto_a_Pensar
{
    public partial class FrmPrincipal : System.Windows.Forms.Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void BtnJugar_Click(object sender, EventArgs e)
        {
            try
            {
                Form1 abrir = new Form1();
                abrir.Show();

            }
            catch
            {
                MessageBox.Show("ERROR DESCONOCIDO");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NombreJugadores abrir = new NombreJugadores();
            abrir.Show();
        }
    }
}
