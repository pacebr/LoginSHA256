using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace testezaobolado
{
    public static class Conexao
    {
        public static SqlConnection conn;

        private static SqlConnection conexaoBanco()
        {
            var conexao = new SqlConnection(
                @"Data Source=exodusgamestore.database.windows.net; User ID=vgdsno; Password=Vgdsn@05031906; initial catalog=exodusDb;"  //Connection Timeout = 1;
            );
            conn = conexao;

            return conn;
        }

        public static void Conectar()
        {
            conexaoBanco().Open();
        }

        public static void Fechar()
        {
            conexaoBanco().Close();
        }
    }
}
