using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prueba_SIE
{
    public partial class FormCoches : Form
    {
        private readonly FormPruebaSIE _parent;
        public string marca, modelo, vin;
        public int id;
        public FormCoches(FormPruebaSIE parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Añadir")
            {
                Coche co = new Coche(textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim());
                DbFunciones.addCar(co);
                Clear();
            }
            if (button1.Text == "Actualizar")
            {
                Coche co = new Coche(textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim());
                DbFunciones.updateCar(co, id);
            }
            _parent.Display();
        }

        public void Clear()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = string.Empty;
        }

        public void UpdateInfo(int id)
        {
            button1.Text = "Actualizar";
            textBox1.Text = marca;
            textBox2.Text = modelo;
            textBox3.Text = vin;
            this.id = id;
        }
    }
}
