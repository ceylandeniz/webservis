using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// WebService için özet açıklama
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Bu Web Hizmeti'nin, ASP.NET AJAX kullanılarak komut dosyasından çağrılmasına, aşağıdaki satırı açıklamadan kaldırmasına olanak vermek için.
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Tasarlanmış bileşenleri kullanıyorsanız şu satırı açıklamadan çıkarın
        //InitializeComponent(); 
    }

    [WebMethod]
    public Bilgiler BilgileriGetir(string _tckn)
    {
        Bilgiler _bilgiler = new Bilgiler();
        string connString = ConfigurationManager.ConnectionStrings["DBBaglan"].ConnectionString;

        using (SqlConnection Conn = new SqlConnection())
        {
            SqlCommand cmd = new SqlCommand("BILGILERI_GETIR", Conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@tckn";
            parameter.Value = _tckn;

            cmd.Parameters.Add(parameter);
            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                _bilgiler.tckn = dr["tckn"].ToString();
                _bilgiler.isim = dr["isim"].ToString();
                _bilgiler.soyisim = dr["soyisim"].ToString();
                _bilgiler.dersnotu = Convert.ToInt32(dr["dersnotu"]);
            }
        }


        return _bilgiler;
    }

}
