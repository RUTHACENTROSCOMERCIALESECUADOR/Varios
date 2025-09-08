using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using WebApplication3.Context;
using WebApplication3.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApplication3.Datos
{
    public interface IMapeoDatosDestinatrio
    {
        Task<List<Destinatario>> Consulta(ConsultarDestinatarioQuery query);
        Task<List<Destinatario>> listar(ListarDestinatariosQuery query);
        Task<int> crear(Destinatario req);
        Task<int> Actualizar(Destinatario req);
        Task<string> eliminar(string idDestinatario);
        //v
    }
    public class MapeoDatosDestinatrio : IMapeoDatosDestinatrio
    {
        private readonly AppDbContext _appDBContext;
        public MapeoDatosDestinatrio(AppDbContext context)
        {
            _appDBContext = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Destinatario>> Consulta(ConsultarDestinatarioQuery req)
        {
            List<Destinatario> response = new List<Destinatario>();

           // List<string> ListDestinatario = req.Destinatarios.Select(x => x.CodigoDestinatario).OfType<string>().ToList();

            //string XmlLista = ConvertListToSimpleXML(ListDestinatario);

            var sqlConnection = (SqlConnection)_appDBContext.Database.GetDbConnection();
            
            
         
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@i_accion", SqlDbType.NChar, 1) { Value = "C" },
                    new SqlParameter("@i_Destinatario", SqlDbType.VarChar) { Value = req.idDestinatario },
                    //new SqlParameter("@i_empresa", SqlDbType.Int) { Value = req.IdEmpresa }


                };
                
                var dal = new DataAccessLayer(sqlConnection);
                var dataSet = await dal.ExecuteStoredProcedureDataSetAsync("pr_consultardestinatario_2", parameters);
                if (dataSet.Tables.Count == 0) return null;
                if (dataSet.Tables[0].Rows.Count == 0) return null;
            var table = dataSet.Tables[0];
            response = ConvertToList<Destinatario>(table);
            //response = lista.FirstOrDefault(); // o [0] si est

            return response;
            
           
        }

        public async Task<List<Destinatario>> listar(ListarDestinatariosQuery req)
        {
            List<Destinatario> destinatarios = new List<Destinatario>();
            var connection = (SqlConnection)_appDBContext.Database.GetDbConnection();
            var dataAccesLay = new DataAccessLayer(connection);
           
            var parametros = new SqlParameter[]
                {
                    new SqlParameter("@i_accion",SqlDbType.NChar){Value="G"},
                    new SqlParameter("@i_empresa",SqlDbType.Int){Value=req.idEmpresa  },
                    new SqlParameter("@i_nombre",SqlDbType.VarChar){Value = req.nombreCodigo}

                };

            var dataSet = await dataAccesLay.ExecuteStoredProcedureDataSetAsync("pr_consultardestinatario_2", parametros);
            //var json = JsonConvert.SerializeObject(dataSet.Tables[0]);
            //destinatarios = JsonConvert.DeserializeObject<List<Destinatario>>(json);
            var table = dataSet.Tables[0];
             destinatarios = ConvertToList<Destinatario>(table);
            
            return destinatarios;
        }
        public async Task<List<Destinatario>> GetDepartment(ListarDestinatariosQuery req)
        {
            List<Destinatario> destinatarios = new List<Destinatario>();

            var connection = _appDBContext.Database.GetDbConnection();

            bool cerrarConnection = false;

            try
            {
                

                if (connection.State != System.Data.ConnectionState.Open)
                {
                    await connection.OpenAsync();
                    cerrarConnection = true;
                }
                    

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pr_consultardestinatario_2";

                    command.Parameters.Add(new SqlParameter("@i_accion", SqlDbType.NChar, 1) { Value = "G" });
                    command.Parameters.Add(new SqlParameter("@i_empresa", SqlDbType.Int) { Value = req.idEmpresa });
                    command.Parameters.Add(new SqlParameter("@i_nombre", SqlDbType.VarChar) { Value = req.nombreCodigo });


                    var dataSet = new DataSet();

                    using (var adapter = new SqlDataAdapter((SqlCommand)command))
                    {
                        adapter.Fill(dataSet);
                    }

                    var json = JsonConvert.SerializeObject(dataSet.Tables[0]);
                    destinatarios = JsonConvert.DeserializeObject<List<Destinatario>>(json);
                   
                }
               
                return destinatarios;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consultar destinatarios: {ex.Message}");
                throw;
            }
            finally
            {
                if (cerrarConnection && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public async Task<int> crear(Destinatario req)
        {

            int procesado = 0;
            try
            {
                var connection = (SqlConnection)_appDBContext.Database.GetDbConnection();
                var DataAccesLay = new DataAccessLayer(connection);

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@i_accion", SqlDbType.NChar){Value = "L"},
                  
                    new SqlParameter("@nombreDestinatario", SqlDbType.NVarChar) { Value = req.nombreDestinatario },
                    new SqlParameter("@codigoDestinatario", SqlDbType.NVarChar) { Value = req.codigoDestinatario},
                    
                    new SqlParameter("@i_correo", SqlDbType.VarChar) { Value = req.correoDestinatario },
                  
            };

                var dataset = await DataAccesLay.ExecuteStoredProcedureDataSetAsync("pr_consultardestinatario_2", parametros);

                var resp = dataset.Tables[0].Rows[0]["DiaSemana"].ToString();

                procesado = Convert.ToInt32(resp);  

                return procesado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consultar destinatarios: {ex.Message}");
                throw;
            }
        }
        public async Task<int> Actualizar(Destinatario req)
        {

            int procesado = 0; 
            try
            {
                var connection = (SqlConnection)_appDBContext.Database.GetDbConnection();
                var DataAccesLay = new DataAccessLayer(connection);

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@i_accion", SqlDbType.NChar){Value = "B"},
                 
                    new SqlParameter("@i_usuarioActualiza", SqlDbType.NVarChar) { Value = "prueba" },
                    new SqlParameter("@i_ipActualiza", SqlDbType.NVarChar) { Value = "pruebas" },
                   
                    new SqlParameter("@i_Destinatario", SqlDbType.VarChar) { Value = req.idDestinatario },
                   
                    new SqlParameter("@i_correo", SqlDbType.VarChar) { Value = req.correoDestinatario },
                   
            };

                var dataset = await DataAccesLay.ExecuteStoredProcedureDataSetAsync("pr_consultardestinatario_2", parametros);
                    
                procesado = 1;
                 
                   

                return procesado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consultar destinatarios: {ex.Message}");
                throw;
            }
        }
        public async Task<string> eliminar(string idDestinatario)
        {
            string response;
            var conexion = (SqlConnection)_appDBContext.Database.GetDbConnection();
            var dataAccLayer = new DataAccessLayer(conexion);

            var parametros = new SqlParameter[]
            {
                new SqlParameter("@i_accion",SqlDbType.NChar){Value = "F"},
                new SqlParameter("@i_Destinatario", SqlDbType.Int){Value = idDestinatario}
            };

            var dataSet = await dataAccLayer.ExecuteStoredProcedureDataSetAsync("pr_consultardestinatario_2", parametros);
            response = dataSet.Tables[0].Rows[0]["DiaSemana"].ToString();
            return response;
        }
        public List<T> ConvertToList<T>(DataTable table) where T : new()
        {
            var properties = typeof(T).GetProperties();
            var list = new List<T>();

            foreach (DataRow row in table.Rows)
            {
                T obj = new T();

                foreach (var prop in properties)
                {
                    if (table.Columns.Contains(prop.Name) && row[prop.Name] != DBNull.Value)
                    {
                        prop.SetValue(obj, Convert.ChangeType(row[prop.Name], prop.PropertyType));
                    }
                }

                list.Add(obj);
            }

            return list;
        }

        internal static string ConvertListToSimpleXML(List<string> list)
        {
            var res = string.Empty; 
            
            if (list != null)
            {
                if (list.Count > 0)
                {
                    var sb = new StringBuilder();
                    sb.Append("<datos>");
                    list.ForEach(t => sb.Append("<detalle>" + "<id>" + t + "</id>" + "</detalle>"));

                    sb.Append("</datos>");
                    res = sb.ToString();
                }
            }
            return res;
        }


    }
}
