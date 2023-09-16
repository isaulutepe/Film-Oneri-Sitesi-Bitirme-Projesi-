using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Deneme2.Models;
using Deneme2.Models.Entity;


namespace Deneme2.Controllers
{
    public class newCategoryController : Controller
    {
        // GET: Movie
        SqlConnection con = new SqlConnection(); 
        SqlCommand com = new SqlCommand();    
        SqlDataReader dr;
        SqlCommand com1 = new SqlCommand();

        SqlCommand com2 = new SqlCommand(); 
        SqlCommand com3 = new SqlCommand(); 

        SqlDataReader dr1; 



        void connectionString()
        {
            con.ConnectionString = "data source=DESKTOP-M1DVH41\\SQLEXPRESS; database= MyWebSite; integrated security=SSPI;";
            //Bu satırda bir hata var düzelt onu.// not: Düzeltildi.
        }

        public ActionResult Category()
        {
            connectionString();
            con.Open();
            com1.Connection = con;
            com1.CommandText = "select * from category";
            dr = com1.ExecuteReader();

            IList<NewCategory> allcategory = new List<NewCategory>();
         
                while (dr.Read())
                {
                  NewCategory categoryPack = new NewCategory();
                  categoryPack.categoryId = Convert.ToInt32(dr[0]); //İd
                  categoryPack.categoryName = dr[1].ToString(); //Kategori adı
                  allcategory.Add(categoryPack);
                }
                dr.Close();
            con.Close();

            ViewData["MyData"] = allcategory;
          
            return View();
        } //Listeleme

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(NewCategory cate)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "insert into category (name) values('" + cate.categoryName+ "')";
            com.ExecuteNonQuery();
            con.Close();
            Response.Write("<font color=#8b3a3a><center>İşlem Başarılı.</center></font>");
            return View();
        }
        public ActionResult sil(int id)
        {
            int silinecekId;
            connectionString();
            con.Open();
            com2.Connection = con;
            com2.CommandText = "select id from category";
            dr1 = com2.ExecuteReader();
            while (dr1.Read())
            {
                if (id == Convert.ToInt32(dr1[0]))
                {
                    silinecekId = Convert.ToInt32(dr1[0]);
                }

            }
            dr1.Close();
            com3.Connection = con;
            com3.CommandText = "delete from category where id='" + id + "'";
            com3.ExecuteNonQuery();
            con.Close();

            Response.Write("<font color=#8b3a3a><center>İşlem Başarılı.</center></font>");

            return View("../Category/Index");
        }

    }
}