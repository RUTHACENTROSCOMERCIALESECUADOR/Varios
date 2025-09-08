using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApplication3.Datos
{
    public class DataAccessLayer
    {
        private readonly SqlConnection _connection;

        public DataAccessLayer(SqlConnection connection)
        {
            _connection = connection ??
                                        throw new ArgumentNullException(nameof(connection));
        }

        public async Task<DataTable> ExecuteStoredProcedureAsync(string storedProcedureName, SqlParameter[] parameters)
        {
            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentException("El nombre del procedimiento almacenado no puede estar vacío.", nameof(storedProcedureName));

            var result = new DataTable();
            bool shouldCloseConnection = false;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    await _connection.OpenAsync();
                    shouldCloseConnection = true;
                }

                using (var cmd = new SqlCommand(storedProcedureName, _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null && parameters.Length > 0)
                        cmd.Parameters.AddRange(parameters);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(result);
                    }
                }

                return result;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Error de SQL al ejecutar el procedimiento almacenado.", sqlEx);
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Operación inválida durante la ejecución del procedimiento.", invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al ejecutar el procedimiento almacenado.", ex);
            }
            finally
            {
                if (shouldCloseConnection && _connection.State == ConnectionState.Open)
                    _connection.Close();
            }

            
        }
        public async Task<DataSet> ExecuteStoredProcedureDataSetAsync(string storedProcedureName, SqlParameter[] parameters)
        {
            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentException("El nombre del procedimiento almacenado no puede estar vacío.", nameof(storedProcedureName));

            var dataSet = new DataSet();
            bool cierraConnection = false;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    await _connection.OpenAsync();
                    cierraConnection = true;
                }

                using (var cmd = new SqlCommand(storedProcedureName, _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null && parameters.Length > 0)
                        cmd.Parameters.AddRange(parameters);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataSet);
                    }
                }

                return dataSet;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Error de SQL al ejecutar el procedimiento almacenado.", sqlEx);
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Operación inválida durante la ejecución del procedimiento.", invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al ejecutar el procedimiento almacenado.", ex);
            }
            finally
            {
                if (cierraConnection && _connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }
    }
}
