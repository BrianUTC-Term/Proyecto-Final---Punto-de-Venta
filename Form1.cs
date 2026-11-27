using WinFormsApp1.Negocio;
using WinFormsApp1.Modelos;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cliente c = new Cliente();
            ClienteNegocio.Agregar(c);
        }
    }
}
