using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Deneme2.Models.Entity;
using Deneme2.Models; //Diğer class dan gelen veriyi kullanabilmek için buraya yabancı kaynak olark ekledim.
using System.Data.SqlClient; //Sql database kullanacagım için bu kütüphaneyi ekledim.

namespace Deneme2.Controllers
{
    public class usersController : Controller
    {
        
        SqlConnection con = new SqlConnection(); 
        SqlCommand com = new SqlCommand(); 
        SqlDataReader dr; 
        SqlDataReader dr2;
        SqlCommand com1 = new SqlCommand();
        SqlCommand com2;
        SqlCommand com3;
        SqlCommand com4= new SqlCommand();


        MyWebSiteEntities db = new MyWebSiteEntities();
        public ActionResult Index()
        {
            var degerler = db.userlogin.ToList();
            return View(degerler);
        }

        public ActionResult sil(string username)
        {         
            connectionString();
            con.Open();
            com4.Connection = con;
            com4.CommandText = "delete from userlogin where username='" + username + "'";
            com4.ExecuteNonQuery();
            con.Close();
       
            Response.Write("<font color=#8b3a3a><center>İşlem Başarılı.</center></font>");
            return View("../Category/Index");
        }


        [HttpGet]
        public ActionResult NewUser()
        {
            return View();
        }

        void connectionString()
        {
            con.ConnectionString = "data source=DESKTOP-M1DVH41\\SQLEXPRESS; database= MyWebSite; integrated security=SSPI;";
            //Bu satırda bir hata var düzelt onu.// not: Düzeltildi.
        }
        [HttpPost]
        public ActionResult NewUser(Account acc) //Diğer clastaki account metodunu çağırdık. Şifre ve kullanıcı adı kontrolu yapacagımız yer burası.
        {
            connectionString();
            con.Open();
            com1.Connection = con;
            com1.CommandText = "select * from userlogin where username='" + acc.Name1 + "'";
            //Kullanıcı adı veritabanında var mı kontrolu sağlamak için kullandım.
            dr2 = com1.ExecuteReader();
            if (dr2.Read()) //Kullancı adı veritabanında kayıtlı ise 
            {
                con.Close(); //Bağlantıyı kapat ve error sayfasını yükle.
                Response.Write("<font color=#8b3a3a><center> Aynı kullanıcı adına sahip kişi zaten kayıtlı.</center></font>");
                return View();
            }

            else //eğer kullanıcı adı kayıtlı değil ise kullanıcı adı ve şifreyi veritabanına kaydet ve create ekranını yükle.
            {
                con.Close();
                con.Open();
                string kayit = "insert into userlogin (username,user_password) values (@Name1,@Password1)";
                com2 = new SqlCommand(kayit, con);
                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                com2.Parameters.AddWithValue("@Name1", acc.Name1);
                com2.Parameters.AddWithValue("@Password1", acc.Password1);
                com2.ExecuteNonQuery();
                con.Close();

                //Seçimleri farklı bir tabloya kaydettim.
                con.Open();
                string secimkayit = "insert into chooses (username,chose) values (@Name1,@Chose)";
                com3 = new SqlCommand(secimkayit, con);
                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                com3.Parameters.AddWithValue("@Name1", acc.Name1); //Anahtar alan olarak görev yapacak olan kısım kullanıcı adı çünkü herkesin adı farklı olmak zorunda.          
                com3.Parameters.AddWithValue("@Chose", (acc.Aksiyon == "on" ? "4/" : "") + (acc.Komedi == "on" ? "2/" : "") +
                    (acc.Dram == "on" ? "7/" : "") + (acc.Abdmovie == "on" ? "3/" : "") + (acc.Animasyon == "on" ? "1/" : "") +
                    (acc.Bilimkurgu == "on" ? "5/" : "") + (acc.Dizi == "on" ? "6/" : "") + (acc.Romantik == "on" ? "9/" : "") +
                    (acc.AileFilmleri == "on" ? "8/" : ""));
                com3.ExecuteNonQuery();
                con.Close();

                Response.Write("<font color=#1b8bb4>İşlem başarılı, artık giriş yapıp devam edebilirsiniz.</font>");
                return View();
            }
        }


    }



}