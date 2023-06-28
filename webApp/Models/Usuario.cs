using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Xml.Serialization;

namespace webApp.Models
{
    [Serializable]
    [XmlRoot("Usuarios"), XmlType("Usuario")]
    public class Usuario
    {
        //public static List<Usuario> Usuarios { get; } = new List<Usuario>();

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        public string Foto { get; set; }


        public static List<Usuario> ListarUsuario()
        {
            string xmlData = HttpContext.Current.Server.MapPath("~/App_Data/Usuario.xml");
            DataSet ds = new DataSet();
            ds.ReadXml(xmlData);

            var usuarios = new List<Usuario>();

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var usuario = new Usuario
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Nombre = row["Nombre"].ToString(),
                        Contraseña = row["Contraseña"].ToString(),
                        Foto = row["Foto"].ToString()
                    };

                    usuarios.Add(usuario);
                }
            }

            return usuarios;
        }

        public Usuario Obtener(int id)
        {
            string xmlData = HttpContext.Current.Server.MapPath("~/App_Data/Usuario.xml");
            DataSet ds = new DataSet();
            ds.ReadXml(xmlData);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (Convert.ToInt32(row["Id"]) == id)
                    {
                        var usuario = new Usuario
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            Nombre = row["Nombre"].ToString(),
                            Contraseña = row["Contraseña"].ToString(),
                            Foto = row["Foto"].ToString()
                        };

                        return usuario;
                    }
                }
            }

            return null;
        }

        public ResponseModel ValidarLogin(string usuario, string password)
        {
            var rm = new ResponseModel();
            try
            {
                string xmlData = HttpContext.Current.Server.MapPath("~/App_Data/Usuario.xml");
                DataSet ds = new DataSet();
                ds.ReadXml(xmlData);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string nombre = row["Nombre"].ToString();
                        string contraseña = row["Contraseña"].ToString();

                        if (nombre == usuario && contraseña == HashHelper.MD5(password))
                        {
                            int id = Convert.ToInt32(row["Id"]);
                            SessionHelper.AddUserToSession(id.ToString());
                            rm.SetResponse(true);
                            return rm;
                        }
                    }
                }

                rm.SetResponse(false, "Usuario o contraseña incorrectos.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                rm.SetResponse(false, "Ha ocurrido un error durante la validación del inicio de sesión.");
            }
            return rm;
        }

        //Metodo Actualizar Perfil

        public ResponseModel GuardarPerfil(HttpPostedFile Foto)
        {
            var rm = new ResponseModel();

            try
            {
                
            }
            catch (Exception)
            {
                throw;
            }
            return rm;
        }
    }

    
}