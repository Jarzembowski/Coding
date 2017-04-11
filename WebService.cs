using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data.OleDb;

namespace WSTest
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {
        public decimal cd_user;
        public decimal cd_id;
       
        [WebMethod]
        public XmlDocument ConsultarImpressaoPendente(int cd_user, int cd_id)
        {
            XmlDocument xml = new XmlDocument();
            string sqlSel;

            if (cd_user > 0)
            {
               SqlConnection cnn = new SqlConnection(getStringConexao());
               cnn.Open();

               sqlSel = "Select * from itens with(nolock)";
               sqlSel += " FOR XML path ('item'), root('itens')";

               SqlCommand cmd = new SqlCommand(sqlSel, cnn);
               XmlReader reader = cmd.ExecuteXmlReader();
               if (reader.Read())
               {
                  xml.Load(reader);
               }else{
                  xml.LoadXml("<itens><message>No item found</message></itens>");
               }
               cnn.Close();
            }else{
               xml.LoadXml("<itens><message>Invalid key</message></itens>");
            }
            return xml;
        }

      [WebMethod]
      private XmlDocument getEmpresaChave(string cd_chave)
        {
            string sqlSel;            
            SqlConnection cnn = new SqlConnection(Application["ConexaoADM"].ToString());
            XmlDocument xml = new XmlDocument();

            sqlSel = "select cd_key from auth with(nolock) where cd_key='" + cd_key + "'";
            SqlCommand cmd = new SqlCommand(sqlSel, cnn);
            XmlReader reader = cmd.ExecuteXmlReader();
         
            if (reader.Read())
            {
               xml.Load(reader);
            }
            else {
               xml.LoadXml("<auth><message>Invalid key</message></auth>");
            }
            cnn.Close();

            return xml;         
        }

        /// <summary>
        /// Retorna um objeto preenchido com os dados da consulta passada por parâmetro
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string ConnectionString, string SQL)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = SQL;
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();

            conn.Open();
            da.Fill(ds);
            conn.Close();

            return ds;
        }

        public static string StripInvalidXmlCharacters(string txt)
        {
            string r = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]";
            return Regex.Replace(txt, r, "", RegexOptions.Compiled);
        }

    }
}