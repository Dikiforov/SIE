using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Prueba_SIE
{
    class DbFunciones
    {
        public static SqlConnection GetConnection()
        {
            string connection = "Data source=DESKTOP-B0LV3SI;Initial Catalog=SIE_Prueba;Integrated Security=True;Encrypt=false";
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                
                conn.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Excepción en SQL + \n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return conn;
        }
        public static void addPerson(Persona pe)
        {
            SqlConnection conn = GetConnection();
            string sqlQueryCheck = "SELECT COUNT(*) FROM PERSONA WHERE NOMBRE = @NOMBRE AND APELLIDO = @APELLIDO";
            SqlCommand cmd_1 = new SqlCommand(sqlQueryCheck, conn);
            cmd_1.CommandType = System.Data.CommandType.Text;

            cmd_1.Parameters.Add("@NOMBRE", System.Data.SqlDbType.VarChar).Value = pe.Nombre;
            cmd_1.Parameters.Add("@APELLIDO", System.Data.SqlDbType.VarChar).Value = pe.Apellido;
            if ((int)cmd_1.ExecuteScalar() <= 0)
            {
                string sqlQueryInsert = "INSERT INTO PERSONA VALUES (@NOMBRE, @APELLIDO)";
                SqlCommand cmd = new SqlCommand(sqlQueryInsert, conn);
                cmd.Parameters.Add("@NOMBRE", System.Data.SqlDbType.VarChar).Value = pe.Nombre;
                cmd.Parameters.Add("@APELLIDO", System.Data.SqlDbType.VarChar).Value = pe.Apellido;
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Persona añadida correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Persona no añadida + \n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ya existe una persona con el mismo nombre y apellido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conn.Close();
        }
        public static void deletePerson(int idPersona)
        {
            string sqlQuery = "DELETE FROM PERSONA WHERE ID = @ID";
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = idPersona;
            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Persona eliminada correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontró ninguna persona con el ID especificado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al eliminar la persona:\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }
        public static void updatePerson(Persona pe, int idPersona)
        {
            SqlConnection conn = GetConnection();
            string sqlQueryCheck = "SELECT COUNT(*) FROM PERSONA WHERE NOMBRE = @NOMBRE AND APELLIDO = @APELLIDO";
            SqlCommand cmd_1 = new SqlCommand(sqlQueryCheck, conn);
            cmd_1.CommandType = System.Data.CommandType.Text;
            cmd_1.Parameters.Add("@NOMBRE", System.Data.SqlDbType.VarChar).Value = pe.Nombre;
            cmd_1.Parameters.Add("@APELLIDO", System.Data.SqlDbType.VarChar).Value = pe.Apellido;
            if ((int)cmd_1.ExecuteScalar() <= 0)
            {
                string sqlQuery = "UPDATE PERSONA SET NOMBRE = @NOMBRE, APELLIDO = @APELLIDO WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add("@NOMBRE", System.Data.SqlDbType.VarChar).Value = pe.Nombre;
                cmd.Parameters.Add("@APELLIDO", System.Data.SqlDbType.VarChar).Value = pe.Apellido;
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.VarChar).Value = idPersona;
                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Persona actualizado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ninguna persona con el ID especificado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al actualizar la persona:\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ya existe una persona con el mismo nombre y apellido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }

        public static void addCar(Coche co)
        {
            SqlConnection conn = GetConnection();
            string sqlQueryCheck = "SELECT COUNT(*) FROM COCHES WHERE VIN = @VIN";

            SqlCommand cmd_1 = new SqlCommand(sqlQueryCheck, conn);
            cmd_1.CommandType = System.Data.CommandType.Text;
            cmd_1.Parameters.Add("@VIN", System.Data.SqlDbType.VarChar).Value = co.VIN;
            if ((int)cmd_1.ExecuteScalar() <= 0)
            {
                string sqlQueryInsert = "INSERT INTO COCHES VALUES (@MARCA, @MODELO, @VIN)";
                SqlCommand cmd = new SqlCommand(sqlQueryInsert, conn);
                cmd.Parameters.Add("@MARCA", System.Data.SqlDbType.VarChar).Value = co.marca;
                cmd.Parameters.Add("@MODELO", System.Data.SqlDbType.VarChar).Value = co.modelo;
                cmd.Parameters.Add("@VIN", System.Data.SqlDbType.VarChar).Value = co.VIN;
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Coche añadido correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Coche no añadido + \n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ya existe un coche con el mismo VIN.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }
        public static void deleteCar(int idCoche)
        {
            string sqlQuery = "DELETE FROM COCHES WHERE ID = @ID";
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = idCoche;
            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Coche eliminado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontró ningún coche con el ID especificado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al eliminar el coche:\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }
        public static void updateCar(Coche co, int idCoche)
        {
            SqlConnection conn = GetConnection();
            string sqlQueryCheck = "SELECT COUNT(*) FROM COCHES WHERE VIN = @VIN";
            SqlCommand cmd_1 = new SqlCommand(sqlQueryCheck, conn);
            cmd_1.CommandType = System.Data.CommandType.Text;
            cmd_1.Parameters.Add("@VIN", System.Data.SqlDbType.VarChar).Value = co.VIN;
            if ((int)cmd_1.ExecuteScalar() <= 0)
            {
                string sqlQuery = "UPDATE COCHES SET MARCA = @MARCA, MODELO = @MODELO, VIN = @VIN WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add("@MARCA", System.Data.SqlDbType.VarChar).Value = co.marca;
                cmd.Parameters.Add("@MODELO", System.Data.SqlDbType.VarChar).Value = co.modelo;
                cmd.Parameters.Add("@VIN", System.Data.SqlDbType.VarChar).Value = co.VIN;
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.VarChar).Value = idCoche;
                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Coche actualizado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún coche con el VIN especificado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al actualizar el coche:\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ya existe un coche con el mismo VIN.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conn.Close();
        }


        public static bool IsPersonaLinked(int idPersona)
        {
            SqlConnection sqlConnection = GetConnection();
            string query = "SELECT COUNT(*) FROM PERSONA_COCHE WHERE ID_PERSONA = @ID_PERSONA";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@ID_PERSONA", idPersona);
            int count = (int)cmd.ExecuteScalar();
            sqlConnection.Close();
            return count > 0;
        }

        public static void DelPersonaLinked(int idPersona)
        {
            SqlConnection sqlConnection = GetConnection();
            string query = "DELETE FROM PERSONA_COCHE WHERE ID_PERSONA = @ID_PERSONA";
            SqlCommand deleteCmd = new SqlCommand(query, sqlConnection);
            deleteCmd.Parameters.AddWithValue("@ID_PERSONA", idPersona);
            deleteCmd.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public static bool IsCocheLinked(int idCoche)
        {
            SqlConnection sqlConnection = GetConnection();
            string query = "SELECT COUNT(*) FROM PERSONA_COCHE WHERE ID_COCHE = @ID_COCHE";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@ID_COCHE", idCoche);
            int count = (int)cmd.ExecuteScalar();
            sqlConnection.Close();
            return count > 0;
        }

        public static void DelCocheLinked(int idCoche)
        {
            SqlConnection sqlConnection = GetConnection();
            string query = "DELETE FROM PERSONA_COCHE WHERE ID_COCHE = @ID_COCHE";
            SqlCommand deleteCmd = new SqlCommand(query, sqlConnection);
            deleteCmd.Parameters.AddWithValue("@ID_COCHE", idCoche);
            deleteCmd.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public static void addRelationShip(int id_coche, int id_persona)
        {
            // Verificar si ya existe una relación con los mismos valores de id_persona e id_coche
            if (RelationShipExists(id_coche, id_persona))
            {
                MessageBox.Show("Ya existe una relación previa con los mismos valores de ID de coche y persona.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Insertar la relación en la tabla PERSONA_COCHE
            string sqlQuery = "INSERT INTO PERSONA_COCHE VALUES (@ID_PERSONA, @ID_COCHE)";
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.Parameters.Add("@ID_PERSONA", System.Data.SqlDbType.Int).Value = id_persona;
            cmd.Parameters.Add("@ID_COCHE", System.Data.SqlDbType.Int).Value = id_coche;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Relación añadida correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al añadir la relación:\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        // Método para verificar si ya existe una relación con los mismos valores de id_persona e id_coche
        private static bool RelationShipExists(int id_coche, int id_persona)
        {
            string sqlQuery = "SELECT COUNT(*) FROM PERSONA_COCHE WHERE ID_PERSONA = @ID_PERSONA AND ID_COCHE = @ID_COCHE";
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.Parameters.Add("@ID_PERSONA", System.Data.SqlDbType.Int).Value = id_persona;
            cmd.Parameters.Add("@ID_COCHE", System.Data.SqlDbType.Int).Value = id_coche;

            try
            {
                int count = (int)cmd.ExecuteScalar();
                return count > 0; // Devuelve true si existe al menos una relación, false si no existe ninguna
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al verificar la relación existente:\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // En caso de error, se asume que la relación no existe
            }
            finally
            {
                conn.Close();
            }
        }



        public static int GetPersonaID(Persona pe, SqlConnection conn)
        {
            string query = "SELECT ID FROM PERSONA WHERE NOMBRE = @Nombre AND APELLIDO = @Apellido";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nombre", pe.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", pe.Apellido);

            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                return Convert.ToInt32(result);
            }
            else
            {
                return -1;
                throw new InvalidOperationException("No se encontró la persona especificada en la base de datos.");
            }
        }

        public static int GetCocheID(Coche co, SqlConnection conn)
        {
            string query = "SELECT ID FROM COCHES WHERE VIN = @VIN";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@VIN", co.VIN); // Corregido el parámetro

            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                return Convert.ToInt32(result);
            }
            else
            {
                return -1;
                throw new InvalidOperationException("No se encontró el coche especificado en la base de datos.");
            }
        }


        public static void DisplayTable(String query, DataGridView dataGridView ,int? param = null)
        {
            string sql = query;
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (param.HasValue) 
            {
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = param.Value;
            }
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adp.Fill(dataTable);
            dataGridView.DataSource = dataTable;
            conn.Close();
        }

        public static DataTable GetCochesByPersonaID(int idPersona)
        {
            string query = "SELECT COCHES.* FROM COCHES " +
                           "INNER JOIN PERSONA_COCHE ON COCHES.ID = PERSONA_COCHE.ID_COCHE " +
                           "WHERE PERSONA_COCHE.ID_PERSONA = @ID_PERSONA";

            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_PERSONA", idPersona);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
        }

        public static DataTable GetPersonasByCocheId(int idCoche)
        {
            string query = "SELECT PERSONA.* FROM PERSONA " +
                           "INNER JOIN PERSONA_COCHE ON PERSONA.ID = PERSONA_COCHE.ID_PERSONA " +
                           "WHERE PERSONA_COCHE.ID_COCHE = @ID_COCHE";

            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_COCHE", idCoche);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
        }
    }
}
