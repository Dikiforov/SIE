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
    public partial class FormPruebaSIE : Form
    {
        FormPersonas formPersona;
        FormCoches formCoche;
        public FormPruebaSIE()
        {
            InitializeComponent();
            formPersona = new FormPersonas(this);
            formCoche = new FormCoches(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formPersona.Clear();
            formPersona.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formCoche.Clear();
            formCoche.ShowDialog();
        }

        public void Display()
        {
            DbFunciones.DisplayTable("SELECT ID, NOMBRE, APELLIDO FROM PERSONA", dataGridView1);
            DbFunciones.DisplayTable("SELECT ID, MARCA, MODELO, VIN FROM COCHES", dataGridView2);
        }

        private void FormPruebaSIE_Shown(object sender, EventArgs e)
        {
            Display();
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && dataGridView2.SelectedRows.Count > 0)
            {
                // Obtener los valores seleccionados de la primera tabla
                int idPersona = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["IDPersona"].Value);

                // Obtener los valores seleccionados de la segunda tabla
                int idCoche = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["IDCoche"].Value);

                // Aquí puedes realizar la operación de inserción en la tabla deseada
                // Suponiendo que tengas un método para insertar la relación en la base de datos
                DbFunciones.addRelationShip(idPersona, idCoche);
                Display();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila en ambos DataGridView.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                MessageBox.Show("Ha seleccionado ver", "ver");
                return;
            }

            if (e.ColumnIndex == 1)
            {
                formPersona.Clear();
                formPersona.nombre = dataGridView1.Rows[e.RowIndex].Cells["cNombre"].Value.ToString();
                formPersona.apellido = dataGridView1.Rows[e.RowIndex].Cells["cApellido"].Value.ToString();
                formPersona.UpdateInfo((int)dataGridView1.Rows[e.RowIndex].Cells["IDPersona"].Value);
                formPersona.ShowDialog();
                return;
            }
            if (e.ColumnIndex == 2)
            {
                if (MessageBox.Show("¿Está seguro de eliminar el registro?", "Información", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    int idPersona = (int)dataGridView1.Rows[e.RowIndex].Cells["IDPersona"].Value;
                    if (DbFunciones.IsPersonaLinked(idPersona)){
                        if (MessageBox.Show("El registro de persona está enlazado en otra tabla. ¿Desea eliminarlo también de la otra tabla?", "Confirmación Adicional", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            // Eliminar el registro de la otra tabla
                            DbFunciones.DelPersonaLinked(idPersona);
                            DbFunciones.deletePerson(idPersona);
                            Display();
                        }
                    }
                    else
                    {
                        DbFunciones.deletePerson(idPersona);
                        Display();
                    }
                    
                }
                return;
            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                formCoche.Clear();
                formCoche.marca = dataGridView2.Rows[e.RowIndex].Cells["cMarca"].Value.ToString();
                formCoche.modelo = dataGridView2.Rows[e.RowIndex].Cells["cModelo"].Value.ToString();
                formCoche.vin = dataGridView2.Rows[e.RowIndex].Cells["cVIN"].Value.ToString();
                formCoche.UpdateInfo((int)dataGridView2.Rows[e.RowIndex].Cells["IDCoche"].Value);
                formCoche.ShowDialog();
                return;
            }
            if (e.ColumnIndex == 2)
            {
                if (MessageBox.Show("¿Está seguro de eliminar el registro?", "Información", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    int idCoche = (int)dataGridView2.Rows[e.RowIndex].Cells["IDCoche"].Value;
                    if (DbFunciones.IsPersonaLinked(idCoche))
                    {
                        if (MessageBox.Show("El registro de persona está enlazado en otra tabla. ¿Desea eliminarlo también de la otra tabla?", "Confirmación Adicional", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            // Eliminar el registro de la otra tabla
                            DbFunciones.DelCocheLinked(idCoche);
                            DbFunciones.deleteCar(idCoche);
                            Display();
                        }
                    }
                    else
                    {
                        DbFunciones.deletePerson(idCoche);
                        Display();
                    }

                }
                return;
            }
        }
    }
}
