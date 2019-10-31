using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace ProjetoAcessos
{
    class Conexao
    {
        //public static string StringConexao = @"Data Source=LOCALHOST\SQLEXPRESS; Initial Catalog=ControleDeAcessos; Integrated Security=True";
        public static string StringConexao = @"Data Source=PCSIMAO\SQLEXPRESS; Initial Catalog=ControleDeAcessos; Integrated Security=True";

        public static void setUsuario(Usuario pUsuario)
        {
            SqlConnection pConn = new SqlConnection(StringConexao);
            try
            {

                pConn.Open();
                string pSQL = "INSERT INTO Usuarios(usuarioId, usuarioNome) values(@param1,@param2)";
                SqlCommand pCMD = new SqlCommand(pSQL);
                pCMD.Connection = pConn;

                pCMD.Parameters.Add(new SqlParameter("@param1", pUsuario.Id));
                pCMD.Parameters.Add(new SqlParameter("@param2", pUsuario.Nome));
                pCMD.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
            }
            finally
            {
                pConn.Close();
            }
        }

        public static void setAmbiente(Ambiente pAmbiente)
        {
            SqlConnection pConn = new SqlConnection(StringConexao);
            try
            {
                pConn.Open();
                string pSQL = "INSERT INTO Ambientes(AmbienteId, AmbienteNome) values(@param1,@param2)";
                SqlCommand pCMD = new SqlCommand(pSQL);
                pCMD.Connection = pConn;
                pCMD.Parameters.Add(new SqlParameter("@param1", pAmbiente.Id));
                pCMD.Parameters.Add(new SqlParameter("@param2", pAmbiente.Nome));
                pCMD.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
            }
            finally
            {
                pConn.Close();
            }
        }

        public static void setLog(Log pLog, Ambiente pambiente)
        {
            SqlConnection pConn = new SqlConnection(StringConexao);
            try
            {
                pConn.Open();
                string pSQL = "INSERT INTO LogsDeAmbientes(AmbienteId, LogDtAcesso, UsuarioId, LogTipoAcesso) values(@param1,@param2,@param3, @param4)";
                SqlCommand pCMD = new SqlCommand(pSQL);
                pCMD.Connection = pConn;
                pCMD.Parameters.Add(new SqlParameter("@param1", pambiente.Id));
                pCMD.Parameters.Add(new SqlParameter("@param2", pLog.DtAcesso));
                pCMD.Parameters.Add(new SqlParameter("@param3", pLog.Usuario.Id));
                pCMD.Parameters.Add(new SqlParameter("@param4", pLog.Tipo_acesso));

                pCMD.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
            }
            finally
            {
                pConn.Close();
            }
        }

        public static void setPermissoes(Usuario pusuario, Ambiente pambiente)
        {
            SqlConnection pConn = new SqlConnection(StringConexao);
            try
            {
                pConn.Open();
                string pSQL = "INSERT INTO Permissoes(UsuarioId, AmbienteId) values(@param1,@param2)";
                SqlCommand pCMD = new SqlCommand(pSQL);
                pCMD.Connection = pConn;
                pCMD.Parameters.Add(new SqlParameter("@param1", pusuario.Id));
                pCMD.Parameters.Add(new SqlParameter("@param2", pambiente.Id));

                pCMD.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
            }
            finally
            {
                pConn.Close();
            }
        }

        public static void getUsuario(Cadastro c)
        {
            SqlConnection pConn = new SqlConnection(StringConexao);
            try

            {
                SqlCommand command = new SqlCommand("SELECT UsuarioId, UsuarioNome FROM Usuarios", pConn);
                pConn.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    c.AdicionarUsuario(new Usuario(reader.GetInt32(0), reader.GetString(1)));
                }
            }
            catch (Exception exc)
            {
            }
            finally
            {
                pConn.Close();
            }
        }
        public static void getAmbiente(Cadastro c)
        {
            SqlConnection pConn = new SqlConnection(StringConexao);
            try
            {
                SqlCommand command = new SqlCommand("SELECT AmbienteId, AmbienteNome FROM Ambientes", pConn);
                pConn.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    c.AdicionarAmbiente(new Ambiente(reader.GetInt32(0), reader.GetString(1)));
                }
            }
            catch (Exception exc)
            {
            }
            finally
            {
                pConn.Close();
            }
        }


        public static void getLogs(Cadastro c)
        {
            SqlConnection pConn = new SqlConnection(StringConexao);
            try
            {
                SqlCommand command = new SqlCommand("SELECT AmbienteId, LogDtAcesso, UsuarioId, LogTipoAcesso FROM LogsDeAmbientes", pConn);
                pConn.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Ambiente ambi = new Ambiente(reader.GetInt32(0), "");
                    Usuario usu = c.PesquisarUsuario(new Usuario(reader.GetInt32(2), ""));
                    foreach (Ambiente amb in usu.Ambientes)
                    {
                        if (amb.Id.Equals(ambi.Id))
                        {
                            amb.RegistrarLog(new Log(reader.GetDateTime(1), usu, reader.GetBoolean(3)));
                        }
                    }
                }
            }
            catch (Exception exc)
            {
            }
            finally
            {
                pConn.Close();
            }
        }

        public static void getPermissoes(Cadastro c)
        {
            SqlConnection pConn = new SqlConnection(StringConexao);
            try
            {
                SqlCommand command = new SqlCommand("SELECT UsuarioId, AmbienteId FROM Permissoes", pConn);
                pConn.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Usuario usu = c.PesquisarUsuario(new Usuario(reader.GetInt32(0), ""));
                    Ambiente amb = c.PesquisarAmbiente(new Ambiente(reader.GetInt32(1), ""));
                    usu.ConcederPermissao(amb); 
                }
                
            }
            catch (Exception exc)
            {
            }
            finally
            {
                pConn.Close();
            }
        }

        public static void deleteUsuario(Usuario pUsuario)
        {
            SqlConnection pConn = new SqlConnection(StringConexao);
            try
            {
                pConn.Open();
                string pSQL = "DELETE FROM Usuarios WHERE usuarioId = @param1";
                string pSQL2 = "DELETE FROM LogsDeAmbientes WHERE usuarioId = @param2";
                SqlCommand pCMD = new SqlCommand(pSQL);
                SqlCommand pCMD2 = new SqlCommand(pSQL2);
                pCMD.Connection = pConn;
                pCMD2.Parameters.Remove(new SqlParameter("@param2", pUsuario.Id));
                pCMD.Parameters.Remove(new SqlParameter("@param", pUsuario.Id));
                pCMD.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
            }
            finally
            {
                pConn.Close();
            }
        }
        public static void deletePermissoes(Usuario pusuario, Ambiente pambiente)
        {
            SqlConnection pConn = new SqlConnection(StringConexao);
            try

            {
                pConn.Open();
                string pSQL = "DELETE FROM Permissoes WHERE UsuarioId = @param1 and AmbienteId = @param2";
                SqlCommand pCMD = new SqlCommand(pSQL);
                pCMD.Connection = pConn;
                pCMD.Parameters.Add(new SqlParameter("@param1", pusuario.Id));
                pCMD.Parameters.Add(new SqlParameter("@param2", pambiente.Id));

                pCMD.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
            }
            finally
            {
                pConn.Close();
            }
        }

    }
}
