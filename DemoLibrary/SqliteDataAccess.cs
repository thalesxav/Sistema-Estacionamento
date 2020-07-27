using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public static class SqliteDataAccess
    {
        public static List<RegistrosModel> CarregaUltimasPlacas()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<RegistrosModel>("select * from Registros LIMIT 150", new DynamicParameters());
                    return output.ToList();
                }
                /*RegistrosModel output = new RegistrosModel();
                using (var conn = new System.Data.SQLite.SQLiteConnection(LoadConnectionString()))
                {
                    conn.Open();
                    
                    using (var comm = new System.Data.SQLite.SQLiteCommand(conn))
                    {
                        comm.CommandText = "select * from Registros LIMIT 50";
                        //var clienteId = comm.ExecuteScalar();
                        using (var reader = comm.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                output.id = (int)reader["id"];
                                output.placa = reader["placa"].ToString();
                                output.tipo = reader["tipo"] != DBNull.Value ? (int)reader["tipo"] : 1;
                                output.data_entrada = (DateTime)reader["data_entrada"];
                                output.data_saida = (DateTime)reader["data_saida"];
                                output.total_pagar = (int)reader["total_pagar"];
                                output.impresso = (int)reader["impresso"];
                            }
                        }
                    }
                }*/
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static List<RegistrosModel> LocalizarPlaca(string text)
        {
            try
            {
                var query = "Select * from Registros where placa = :placa";
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("placa", text);

                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<RegistrosModel>(query, dynamicParameters);
                    //if(output.ToList().Count() > 0)

                    return output.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable LeDados2<S, T>(string query) where S : IDbConnection, new()
                                           where T : IDbDataAdapter, IDisposable, new()
        {
            using (var conn = new S())
            {
                using (var da = new T())
                {
                    using (da.SelectCommand = conn.CreateCommand())
                    {
                        da.SelectCommand.CommandText = query;
                        da.SelectCommand.Connection.ConnectionString = LoadConnectionString();
                        DataSet ds = new DataSet(); 
                        da.Fill(ds);
                        return ds.Tables[0];
                    }
                }
            }
        }

        public static List<string> GetImportedFileList()
        {
            List<string> ImportedFiles = new List<string>();

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                using (IDbCommand fmd = cnn.CreateCommand())
                {
                    fmd.CommandText = @"SELECT * FROM Registros";
                    fmd.CommandType = CommandType.Text;
                    IDataReader r = fmd.ExecuteReader();
                    while (r.Read())
                    {
                        
                    }
                }
            }
            return ImportedFiles;
        }

        public static DataTable Consulta(string text)
        {
            try
            {
                var query = text;
                var dynamicParameters = new DynamicParameters();

                using (var conn = new System.Data.SQLite.SQLiteConnection(LoadConnectionString()))
                {
                    conn.Open();
                    
                    using (var comm = new System.Data.SQLite.SQLiteCommand(conn))
                    {
                        comm.CommandText = query;

                        var adapter = new System.Data.SQLite.SQLiteDataAdapter(comm);
                        var dataTable = new System.Data.DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;

                        /*foreach (System.Data.DataRow row in dataTable.Rows)
                        {
                            Console.WriteLine("Nome do Cliente: {0}", row["Nome"]);
                        }

                        //var clienteId = comm.ExecuteScalar();
                        using (var reader = comm.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                
                            }
                        }*/
                        
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private static DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach(var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

             foreach (var item in items)
            {
               var values = new object[props.Length];
                for (var i=0; i<props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        public static bool InsereCupom(RegistrosModel registro)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Execute("insert into Registros (placa,  tipo, data_entrada, impresso) " +
                        "values (@placa, @tipo, @data_entrada, 1)", registro);
                
                    return true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            //return Path.Combine(ApplicationData.Current.LocalFolder.Path,@"Databases\DublinCityCouncilTable.db"), true)
            //return System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + ConfigurationManager.ConnectionStrings[id].ConnectionString;
            //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),ConfigurationManager.ConnectionStrings[id].ConnectionString);
            //return path;
            //return string.Format("Data Source={0};Version=3;Foreign Keys=true; FailIfMissing=True;", path);
            //return  "Data Source=C:\\Users\\thales.nogueira\\Documents\\Agência Thax\\Sistema Estacionamento\\WinFormUI\\DemoDB.db;Version=3;";
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
            //return "Data Source="+System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)+"DemoDB.db;Version=3;";
        }

        public static List<TiposPagamentoModel> CarregaTiposPagamentos()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<TiposPagamentoModel>("select * from TipoPagamento ORDER BY tipo ASC", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static bool Login(String usuario, String senha)
        {
            try
            {
                var query = "Select * from Person where usuario = @usr and senha = @senha";
                //var query = "Select * from Person";
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@usr", usuario.Trim());
                dynamicParameters.Add("@senha", senha.Trim());

                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<PersonModel>(query, dynamicParameters);
                    if(output.ToList().Count() > 0)
                        return true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return false;
        }

        public static List<TiposPagamentoModel> RetornaValor(int tipo)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("tipo", tipo);
                    var output = cnn.Query<TiposPagamentoModel>("select * from TipoPagamento where tipo = :tipo", dynamicParameters);
                    return output.ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static List<RegistrosModel> CarregaUltimoCupom()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<RegistrosModel>("select * from Registros order by id desc limit 1", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static List<EstacionamentoModel> CarregaDadosEstacionamento()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<EstacionamentoModel>("select * from Estacionamento", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static bool ExistePlacaEntrada(string placa)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                     var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("placa", placa);
                    var output = cnn.Query<TiposPagamentoModel>("select * from Registros where placa = :placa and data_saida is null ORDER BY data_saida DESC LIMIT 1", dynamicParameters);
                    return output.ToList().Count > 0;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static List<RegistrosModel> RelatorioPorData(string text, string text2)
        {
            try
            {
                var query = "Select * from Registros where data_entrada >= :text and data_entrada < :text2";
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("text", text);
                dynamicParameters.Add("text2", text2);

                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<RegistrosModel>(query, dynamicParameters);
                    //if(output.ToList().Count() > 0)

                    return output.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<RegistrosModel> CarregaPagamento(string id)
        {
             try
             {
                var query = "Select * from Registros where id = :id";
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("id", id);

                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<RegistrosModel>(query, dynamicParameters);
                    //if(output.ToList().Count() > 0)
                        
                    return output.ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static bool RegistraSaida(RegistrosModel registro)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Execute("Update Registros Set data_saida = @data_saida, total_pagar = @total_pagar, " +
                        "impresso = 1" +
                        " Where id = @id", registro);
                
                    return true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static List<RegistrosModel> CarregaPagamentoByPlaca(string placa)
        {
            try
            {
                var query = "Select * from Registros where placa = :placa and data_saida is null";
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("placa", placa);

                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<RegistrosModel>(query, dynamicParameters);
                    //if(output.ToList().Count() > 0)

                    return output.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<RegistrosModel> CarregaPagamentoPorPlacaJaSaiu(string id)
        {
            try
            {
                var query = "Select * from Registros where id = :id and data_saida is not null";
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("id", id);

                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<RegistrosModel>(query, dynamicParameters);
                    //if(output.ToList().Count() > 0)

                    return output.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<RegistrosModel> CarregaPagamentoPorId(string id)
        {
            try
            {
                var query = "Select * from Registros where id = :id";
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("id", id);

                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<RegistrosModel>(query, dynamicParameters);
                    //if(output.ToList().Count() > 0)

                    return output.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<RegistrosModel> JaEntrouNoPatio(string text)
        {
            try
            {
                var query = "Select * from Registros where placa = :placa order by data_entrada desc limit 1";
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("placa", text);

                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<RegistrosModel>(query, dynamicParameters);
                    //if(output.ToList().Count() > 0)

                    return output.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<RegistrosModel> CarregaUltimaEntradaByPlaca(string text)
        {
            try
            {
                var query = "Select * from Registros where placa = :placa order by data_entrada desc limit 1";
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("placa", text);

                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<RegistrosModel>(query, dynamicParameters);
                    //if(output.ToList().Count() > 0)

                    return output.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
