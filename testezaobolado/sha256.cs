using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace testezaobolado
{
    internal static class sha256
    {
        public static string ToSHA256(string value)
        {
            SHA256 sha256 = SHA256.Create();

            byte[] hashData = sha256.ComputeHash(Encoding.Default.GetBytes(value));
            StringBuilder returnValue = new StringBuilder();

            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            return returnValue.ToString();
        }
        public static bool VerificarCredenciais(string usuario, string senha) // verificar Login
        {
            Conexao.Conectar();
            string sql = "select usuario, senha from teste where usuario=@usuario AND senha=@senha";
            SqlCommand cmd = new SqlCommand(sql, Conexao.conn);
            cmd.Parameters.AddWithValue("usuario", usuario);
            cmd.Parameters.AddWithValue("senha", ToSHA256(senha));

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                Conexao.Fechar();
                return true;
            }
            Conexao.Fechar();
            return false;
        }

        public static bool CriarUsuario(string usuario, string senha)
        {
            try
            {
                string senhaHasheada = ToSHA256(senha);
                Conexao.Conectar();
                string insertQuery = "insert into teste (usuario,senha) values (@usuario, @senha)";
                SqlCommand com = new SqlCommand(insertQuery, Conexao.conn);
                com.Parameters.AddWithValue("@usuario", usuario);
                com.Parameters.AddWithValue("@senha", senhaHasheada);

                com.ExecuteNonQuery();
                Conexao.Fechar();
                return true;
            }
            catch (Exception ex)
            {
                Conexao.Fechar();
                System.Windows.Forms.MessageBox.Show("erro"+ ex);
                return false;
            }
        }
    }
}
