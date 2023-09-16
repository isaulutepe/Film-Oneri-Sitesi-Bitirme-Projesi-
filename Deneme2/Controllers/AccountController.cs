using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient; //Sql database kullanacagım için bu kütüphaneyi ekledim.
using System.Web.Mvc;
using Deneme2.Models; //Diğer class dan gelen veriyi kullanabilmek için buraya yabancı kaynak olark ekledim.
using System.Threading;


namespace Deneme2.Controllers
{
    public class AccountController : Controller
    {
        SqlConnection con = new SqlConnection(); //Veritabanı Bağlantı nesnesi.
        SqlCommand com = new SqlCommand(); //Giriş kontrol nesnesi olarak çalışacak.
        SqlDataReader dr; //Veritabanından verileri okumak için.
        SqlDataReader dr2;//Kontrol nesnesi olarak çalışacak.
        SqlCommand com1 = new SqlCommand();//Kontronl nesnesi olarak çalışasak
        SqlCommand com2 ;//Kaydetme işlemi için çalışacak.
        SqlCommand com3; //Filmleri farklı tabloya kaydetmek için.
        SqlCommand com4; //Filmleri okurken kullanılacak command nesnesi.
        SqlCommand com5=new SqlCommand(); //Chooses tablosundan kategorileri çekme komutu için.
        SqlCommand com6 = new SqlCommand(); //Random film üretmek için
      
        SqlDataReader dr3; 
        SqlDataReader dr4; 
        SqlDataReader dr5; 
  

        string secimler;//Kullanıcının seçimlerini içinde tutacak olan değişken.      
       
        void connectionString()
        {
            con.ConnectionString = "data source=DESKTOP-M1DVH41\\SQLEXPRESS; database= MyWebSite; integrated security=SSPI;";
            //Bu satırda bir hata var düzelt onu.// not: Düzeltildi.
        }
        // GET: Account
        [HttpGet] //Get işlemi yapacagımızız tanımladık.
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost] //Gelen veriyi almak için.
        public ActionResult Verify(Account acc) //Diğer clastaki account metodunu çağırdık. Şifre ve kullanıcı adı kontrolu yapacagımız yer burası.
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from userlogin where username='" + acc.Name + "' and user_password='" + acc.Password + "'";
            //Kullanıcı adı ve şifre değerlerini aldık. Kontrol etmek için.
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
                com5.Connection = con;
                com5.CommandText = "select chose from chooses where username='"+acc.Name+"'";
                if (acc.Name == "admin" && acc.Password == "123") //Giriş yapan kullanıcı admin ise 
                {
                    return View("..\\Category\\Index"); //Admin sayfası olarak kullanılan view.
                }
                //Kullanıcı adı giriş yapan kişinin adı ise.
                //Giriş yapan kullanıcının adının ait olduğu satıdaki seçimlerini getirecek.
                dr4 = com5.ExecuteReader(); //Chososes tablosunu okuduk. 
                
                while (dr4.Read())
                {
                    secimler= dr4["chose"].ToString(); //Seçimleri aldık ve secimler değişkenine ekledik.
                }
                dr4.Close();

                com4 = new SqlCommand();
                com4.Connection = con;
                string[] secimleriParcala = secimler.Split('/');

                IList<Movies> allMovies = new List<Movies>();
                //Dizideki eleman sayısı kadar çalış.
                for (int i = 0; i < secimleriParcala.Length-1; i++)
                {               
                        com4.CommandText = "select * from movies where categoryId  LIKE '%" + secimleriParcala[i] + "%'";
                        dr3 = com4.ExecuteReader();                  
                        while (dr3.Read())
                        {
                            Movies moviePack = new Movies();
                            moviePack.id = Convert.ToInt32(dr3[0]); //İd
                            moviePack.movieName = dr3[1].ToString(); //MovieName
                            moviePack.movieInformation = dr3[2].ToString(); //MovieInformation
                            moviePack.image = dr3[3].ToString(); //MovieImage
                            moviePack.category = dr3[4].ToString(); //Category 
                            allMovies.Add(moviePack);
                        }
                        dr3.Close();
                        ViewData["MyData"] = allMovies;
                }

               
                return View("Createe"); //Kullanıcı adı ve şifre doğru ise.
            }
            else
            {
                con.Close();
                Response.Write("<font color=#8b3a3a> Kullanıcı Adı veya Şifre Hatalı lütfen tekrar deneyiniz.</font>");
                return View("Login");
            }
            
        }  
        public ActionResult Insert(Account acc) //Diğer clastaki account metodunu çağırdık. Şifre ve kullanıcı adı kontrolu yapacagımız yer burası.
        {
            connectionString();
            con.Open();
            com1.Connection = con;
            com1.CommandText = "select * from userlogin where username='" + acc.Name1 + "'";
            //Kullanıcı adı veritabanında var mı kontrolu sağlamak için kullandım.
            dr2 = com1.ExecuteReader();
            if (dr2.Read()) //Kullancıı adı veritabanında kayıtlı ise 
            {
                con.Close(); //Bağlantıyı kapat ve error sayfasını yükle.
                Response.Write("<font color=#8b3a3a> Aynı kullanıcı adına sahip kişi zaten kayıtlı.</font>");
                return View("Error");
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
                return View("Login");
            }

       }
        public ActionResult SingUp() //Üye ol tagı na tıklandığında sağlanacak konrol noktası.
        {
            return View("Error");

        }
        public ActionResult RandomMovie()
        {
            connectionString();
            con.Open();
            com6.Connection = con;
            com6.CommandText = "select top 6 * from movies order by newid()";
            dr5 = com6.ExecuteReader();

            IList<Movies> allMovies = new List<Movies>();
            while (dr5.Read())
            {
                Movies moviePack = new Movies();
                moviePack.id = Convert.ToInt32(dr5[0]); //İd
                moviePack.movieName = dr5[1].ToString(); //MovieName
                moviePack.movieInformation = dr5[2].ToString(); //MovieInformation
                moviePack.image = dr5[3].ToString(); //MovieImage
                moviePack.category = dr5[4].ToString(); //Category 
                allMovies.Add(moviePack);
            }
            dr5.Close();        
            ViewData["MyData"] = allMovies;      
            con.Close();
            return View("Createe");
        }
    }
}