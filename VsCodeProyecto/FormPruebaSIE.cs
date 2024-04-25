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
        bool mostrarTotCoches = true;
        bool mostrarTotPersonas = true;

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
            if (CountSelectedRows(dataGridView1, dataGridView2) == 0)
            {
                // Obtener el nombre y apellido de la persona seleccionada
                string nombre = dataGridView1.SelectedRows[0].Cells["cNombre"].Value.ToString();
                string apellido = dataGridView1.SelectedRows[0].Cells["cApellido"].Value.ToString();

                // Obtener el VIN del coche seleccionado
                string marca = dataGridView2.SelectedRows[0].Cells["cMarca"].Value.ToString();
                string modelo = dataGridView2.SelectedRows[0].Cells["cModelo"].Value.ToString();
                string vin = dataGridView2.SelectedRows[0].Cells["cVIN"].Value.ToString();

                // Buscar el ID de la persona por nombre y apellido
                Persona pe = new Persona(nombre, apellido);
                int idPersona = DbFunciones.GetPersonaID(pe, DbFunciones.GetConnection());

                if (idPersona != -1)
                {
                    foreach (DataGridViewRow cocheRow in dataGridView2.SelectedRows)
                    {
                        // Obtener el ID del coche seleccionado
                        int idCoche = (int)cocheRow.Cells["IDCoche"].Value;

                        // Verificar si el coche ya está relacionado con otra persona
                        if (DbFunciones.IsCocheLinked(idCoche))
                        {
                            MessageBox.Show("El coche seleccionado ya está relacionado con otra persona.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Crear la relación entre la persona y el coche
                        DbFunciones.addRelationShip(idCoche, idPersona);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo encontrar la persona con el nombre y apellido especificados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si se hizo clic en una celda con un botón
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridView2.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                // Obtener el nombre de la columna para identificar la acción del botón
                string columnName = dataGridView2.Columns[e.ColumnIndex].Name;

                // Si se hace clic en el botón "Ver"
                if (columnName == "VerColumn1")
                {
                    int idCoche = (int)dataGridView2.Rows[e.RowIndex].Cells["IDCoche"].Value;
                    DataTable personaDataTable = DbFunciones.GetPersonasByCocheId(idCoche);
                    dataGridView1.DataSource = personaDataTable;
                }
                // Si se hace clic en el botón "Editar"
                else if (columnName == "EditarColumn1")
                {
                    formCoche.Clear();
                    formCoche.marca = dataGridView2.Rows[e.RowIndex].Cells["cMarca"].Value.ToString();
                    formCoche.modelo = dataGridView2.Rows[e.RowIndex].Cells["cModelo"].Value.ToString();
                    formCoche.vin = dataGridView2.Rows[e.RowIndex].Cells["cVin"].Value.ToString();
                    formPersona.UpdateInfo((int)dataGridView2.Rows[e.RowIndex].Cells["IDCoche"].Value);
                    formPersona.ShowDialog();
                }
                // Si se hace clic en el botón "Eliminar"
                else if (columnName == "EliminarColumn1")
                {
                    int idCoche = (int)dataGridView2.Rows[e.RowIndex].Cells["IDCoche"].Value;
                    if (DbFunciones.IsCocheLinked(idCoche))
                    {
                        if (MessageBox.Show("El registro de coche está enlazado en otra tabla. ¿Desea eliminarlo también de la otra tabla?", "Confirmación Adicional", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            // Eliminar el registro de la otra tabla
                            DbFunciones.DelCocheLinked(idCoche);
                            DbFunciones.deleteCar(idCoche);
                            Display();
                        }
                    }
                    else
                    {
                        DbFunciones.deleteCar(idCoche);
                        Display();
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DbFunciones.DisplayTable("SELECT ID, NOMBRE, APELLIDO FROM PERSONA", dataGridView1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DbFunciones.DisplayTable("SELECT ID, MARCA, MODELO, VIN FROM COCHES", dataGridView2);
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si se hizo clic en una celda con un botón
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                // Obtener el nombre de la columna para identificar la acción del botón
                string columnName = dataGridView1.Columns[e.ColumnIndex].Name;

                // Si se hace clic en el botón "Ver"
                if (columnName == "VerColumn")
                {
                    int idPersona = (int)dataGridView1.Rows[e.RowIndex].Cells["IDPersona"].Value;
                    DataTable cochesDataTable = DbFunciones.GetCochesByPersonaID(idPersona);
                    dataGridView2.DataSource = cochesDataTable;
                }
                // Si se hace clic en el botón "Editar"
                else if (columnName == "EditarColumn")
                {
                    formPersona.Clear();
                    formPersona.nombre = dataGridView1.Rows[e.RowIndex].Cells["cNombre"].Value.ToString();
                    formPersona.apellido = dataGridView1.Rows[e.RowIndex].Cells["cApellido"].Value.ToString();
                    formPersona.UpdateInfo((int)dataGridView1.Rows[e.RowIndex].Cells["IDPersona"].Value);
                    formPersona.ShowDialog();
                }
                // Si se hace clic en el botón "Eliminar"
                else if (columnName == "EliminarColumn")
                {
                    int idPersona = (int)dataGridView1.Rows[e.RowIndex].Cells["IDPersona"].Value;
                    if (DbFunciones.IsPersonaLinked(idPersona))
                    {
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
            }
        }
        private int CountSelectedRows(DataGridView dataGridView1, DataGridView dataGridView2)
        {
            int count = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell_1 = row.Cells["cCheck"] as DataGridViewCheckBoxCell;
                if (checkBoxCell_1 != null && Convert.ToBoolean(checkBoxCell_1.Value))
                {
                    count++;
                }
            }
            if (count > 0)
            {
                count = 0;
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell_2 = row.Cells["cCheck1"] as DataGridViewCheckBoxCell;
                    if (checkBoxCell_2 != null && Convert.ToBoolean(checkBoxCell_2.Value))
                    {
                        count++;
                    }
                }
                if (count <= 0)
                {
                    MessageBox.Show("No ha seleccionado coches", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 1;
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado una persona", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
            return 0;
        }

        private void dataGridView2_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView2.IsCurrentCellDirty)
            {
                dataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void PanelInfoEmpresa_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
