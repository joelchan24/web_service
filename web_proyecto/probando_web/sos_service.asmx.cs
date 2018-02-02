using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using probando_web.DAO;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Data;

using Newtonsoft.Json;
using System.IO;
namespace probando_web
{
    
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
     [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        ConexionDAO obj = new ConexionDAO();

        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hola a todos";
        //}

        [ScriptMethod(ResponseFormat =ResponseFormat.Json)]
        [WebMethod]
        public void mostrar_usuarios()
        {
            
            SqlCommand conuslta =new  SqlCommand("select * from usuario");
            
            string salidaJson = string.Empty;
            salidaJson = JsonConvert.SerializeObject(obj.EjecutarSentencia(conuslta));

            HttpContext Contexto = HttpContext.Current;
            Contexto.Response.ContentType = "application/json";
            Contexto.Response.Write(salidaJson);
            Contexto.Response.End();
        }
        [WebMethod]
        public void mostrar_puntos_aprovados()
        {

            SqlCommand conuslta = new SqlCommand(" select  p.ID as clave ,u.Nombre  as usuario,zona ,comentario ,p.fecha as fecha1,n.Peligro as peli from [Puntos-peligrosos] p INNER JOIN [Niveles-peligro] n on n.id=p.id_peligro inner join Usuario u on u.ID=p.id_usuario where p.Estatus=0");

            string salidaJson = string.Empty;
            salidaJson = JsonConvert.SerializeObject(obj.EjecutarSentencia(conuslta));

            HttpContext Contexto = HttpContext.Current;
            Contexto.Response.ContentType = "application/json";
            Contexto.Response.Write(salidaJson);
            Contexto.Response.End();
        }
        [WebMethod]
        public void mostrar_Niveles_peligro()
        {

            SqlCommand conuslta = new SqlCommand("select * from [Niveles-peligro]");

            string salidaJson = string.Empty;
            salidaJson = JsonConvert.SerializeObject(obj.EjecutarSentencia(conuslta));

            HttpContext Contexto = HttpContext.Current;
            Contexto.Response.ContentType = "application/json";
            Contexto.Response.Write(salidaJson);
            Contexto.Response.End();
        }
        [WebMethod]
        public void login(string correo,string contraseña)
        {
            
            SqlCommand cmd = new SqlCommand("select ID_tipo, correo from usuario where	correo=@correo and contraseña=@contra");
            cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = correo;
            cmd.Parameters.Add("@contra", SqlDbType.VarChar).Value = contraseña;
            cmd.CommandType = CommandType.Text;
            //int resultado = obj.EjecutarComando(cmd);
           

            string salidaJson = string.Empty;
            salidaJson = JsonConvert.SerializeObject(obj.EjecutarComando(cmd));

            HttpContext Contexto = HttpContext.Current;
            Contexto.Response.ContentType = "application/json";
            Contexto.Response.Write(salidaJson);
            Contexto.Response.End();
            
        }
        [WebMethod]
        public String Guardar_usuario(string nombre,string contraseña,DateTime fecha,string telefono,string correo,string apellido,byte foto,string sexo)
        {
            
            SqlCommand cmd = new SqlCommand("insert into usuario (nombre,contraseña,fecha,id_tipo,telefono,correo,Apellido,sexo,foto) values (@nom,@contra,@fecha,@id_tipo,@telefono,@correo,@apellido,@sexo,@foto)");
            
            cmd.Parameters.Add("@nom", SqlDbType.VarChar).Value = nombre;
            cmd.Parameters.Add("@contra", SqlDbType.VarChar).Value = contraseña;
            cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fecha.ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@id_tipo", SqlDbType.Int).Value = 1;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar).Value = telefono;
            cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = correo;
            cmd.Parameters.Add("@apellido", SqlDbType.VarChar).Value = apellido;
            cmd.Parameters.Add("@foto", SqlDbType.VarBinary).Value = foto;

            cmd.Parameters.Add("@sexo", SqlDbType.VarChar).Value = sexo;
            string salida = string.Empty;
            salida = JsonConvert.SerializeObject(obj.EjecutarComando(cmd));

            HttpContext Contexto = HttpContext.Current;
            Contexto.Response.ContentType = "application/json";
            Contexto.Response.Write(salida);
            Contexto.Response.End();


            return "hecho";
        }
        [WebMethod]
        public String Guardar_puntos(string Nombre,string longitud,string latitud,string zona, string contraseña, int id_usuario,int status,int id_peligro,DateTime fecha,byte imagen,string url,string comentario )
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Puntos-peligrosos]([id_peligro],[Longitud] ,[Latitud],[Zona],[id_usuario],[Estatus],[fecha] ,[imagen],[comentario],[url],[comentadmin]) VALUES(@id_peligro,@longitud,@latitud,@zona,@id_usuario,@estatus,@fecha,@imagen,@comentario,@url,@comentadmin )");
            cmd.Parameters.Add("@id_peligro", SqlDbType.Int).Value = id_peligro;
            cmd.Parameters.Add("@longitud", SqlDbType.VarChar).Value = longitud;
            cmd.Parameters.Add("@latitud", SqlDbType.VarChar).Value = latitud;
            cmd.Parameters.Add("@zona", SqlDbType.VarChar).Value = zona;
            cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;
            cmd.Parameters.Add("@estatus", SqlDbType.Bit).Value =status;
            cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = fecha.ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@imagen", SqlDbType.VarBinary).Value = imagen;
            cmd.Parameters.Add("@url", SqlDbType.VarChar).Value = url;
            cmd.Parameters.Add("@comentario", SqlDbType.VarChar).Value = comentario;
            cmd.Parameters.Add("@comentadmin", SqlDbType.VarChar).Value = "Enviado para verificación";
            cmd.CommandType = CommandType.Text;
            string salida = string.Empty;
            salida = JsonConvert.SerializeObject(obj.EjecutarComando(cmd));

            HttpContext Contexto = HttpContext.Current;
            Contexto.Response.ContentType = "application/json";
            Contexto.Response.Write(salida);
            Contexto.Response.End();


            return "hecho";
        }
        [WebMethod]
        public void delitos_aprovados()
        {

            SqlCommand cmd = new SqlCommand(" select (select COUNT(*) from [Puntos-peligrosos] where Estatus = 0)as rechazados,(select COUNT(*) from [Puntos-peligrosos] where Estatus = 1)as aprobados ");
     
            cmd.CommandType = CommandType.Text;
            //int resultado = obj.EjecutarComando(cmd);


            string salidaJson = string.Empty;
            salidaJson = JsonConvert.SerializeObject(obj.EjecutarComando(cmd));

            HttpContext Contexto = HttpContext.Current;
            Contexto.Response.ContentType = "application/json";
            Contexto.Response.Write(salidaJson);
            Contexto.Response.End();

        }
        [WebMethod]
        public void estadisticas()
        {

            SqlCommand cmd = new SqlCommand("select n.Peligro , count( n.Peligro) as total from [Puntos-peligrosos] p inner join [niveles-peligro] n on n.ID=p.id_peligro   GROUP BY n.Peligro");

            cmd.CommandType = CommandType.Text;
            //int resultado = obj.EjecutarComando(cmd);


            string salidaJson = string.Empty;
            salidaJson = JsonConvert.SerializeObject(obj.EjecutarComando(cmd));

            HttpContext Contexto = HttpContext.Current;
            Contexto.Response.ContentType = "application/json";
            Contexto.Response.Write(salidaJson);
            Contexto.Response.End();

        }
        //public String Guardar(string Nombre, string Apellido, string val)
        //{
        //    SqlCommand comanod = new SqlCommand("insert into usario (nombre)values(@nom) ");
        //    comanod.Parameters.Add("@nom", SqlDbType.VarChar).Value = Nombre;
        //    byte[] bytes = Convert.FromBase64String(Apellido);


        //    string filePath = Server.MapPath("~/foto.png");
        //    File.WriteAllBytes(filePath, bytes);


        //    return "hecho";
        //}

    }
}
