using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace probando_web.DAO
{
    public class ConexionDAO
    {
        SqlCommand comandosql;
        SqlDataAdapter adaptador;
        DataSet datasetadaptador;
        SqlConnection coneccion;

        //DESKTOP-TT12AGM
        public ConexionDAO()
        {
            adaptador = new SqlDataAdapter();
            comandosql = new SqlCommand();
            coneccion = new SqlConnection();



        }

        public SqlConnection establecerConexion()
        {
            //string cs = "Data Source=KAREN\\SQLEXPRESS; Initial catalog=ProyectoSOS;  integrated security=true";
            // string cs = "Data Source=DESKTOP-TT12AGM; Initial catalog=ProyectoSOS;  integrated security=true";
            //string cs = "Data Source=DESKTOP-TT12AGM\\SQLEXPRESS; Initial catalog=ProyectoSOS;  integrated security=true";
            //string cs=  "Data Source=SQL7002.site4now.net;Initial Catalog=DB_A336B7_sospro;User Id=DB_A336B7_sospro_admin;Password=LIO10!chan";
            string cs = "Data Source=SQL7004.site4now.net;Initial Catalog=DB_A336B7_sos;User Id=DB_A336B7_sos_admin;Password=LIO10!chan";

            coneccion = new SqlConnection(cs);
            return coneccion;
        }


        public void abrirConexion()
        {
            coneccion.Open();
        }
        public void cerrarConexion()
        {
            coneccion.Close();
        }
        public DataSet EjecutarSentencia(SqlCommand SqlComando)
        {

            // SELECT (Devolver registros)
            adaptador = new SqlDataAdapter();
            comandosql = new SqlCommand();
            datasetadaptador = new DataSet();

            comandosql = SqlComando;
            comandosql.Connection = this.establecerConexion();
            this.abrirConexion();
            adaptador.SelectCommand = comandosql;
            adaptador.Fill(datasetadaptador);
            this.cerrarConexion();
            return datasetadaptador;

        }
        public int EjecutarComando(SqlCommand SqlComando)
        {
            // INSERT, DELETE, UPDATE
            comandosql = new SqlCommand();
            comandosql = SqlComando;
            comandosql.Connection = this.establecerConexion();
            this.abrirConexion();
            int id = 0; id = Convert.ToInt32(comandosql.ExecuteScalar());
            this.cerrarConexion();
            return id;

        }
    }
}