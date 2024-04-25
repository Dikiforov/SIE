using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Prueba_SIE
{
    public partial class FormPersonas : Form
    {
        private readonly FormPruebaSIE _parent;
        public string nombre, apellido;
        public int id;
        public FormPersonas(FormPruebaSIE parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Añadir")
            {
                Persona pe = new Persona(textBox1.Text.Trim(), textBox2.Text.Trim());
                DbFunciones.addPerson(pe);
                Clear();
            }
            if (button1.Text == "Actualizar")
            {
                Persona pe = new Persona(textBox1.Text.Trim(), textBox2.Text.Trim());
                DbFunciones.updatePerson(pe, id);
            }
            _parent.Display();
        }

        public void Clear()
        {
            textBox1.Text = textBox2.Text = string.Empty;
        }
        public void UpdateInfo(int id)
        {
            button1.Text = "Actualizar";
            textBox1.Text = nombre;
            textBox2.Text = apellido;
            this.id = id;
        }
    }
}
