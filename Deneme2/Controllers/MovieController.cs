using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using Deneme2.Models.Entity;
using Deneme2.Models;
using System.Runtime.Remoting.Contexts;
using System.IO;

namespace Deneme2.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        SqlConnection con = new SqlConnection(); //Veritabanı Bağlantı nesnesi.
        SqlCommand com = new SqlCommand(); //Giriş kontrol nesnesi olarak çalışacak.
        SqlCommand com1 = new SqlCommand(); //Giriş kontrol nesnesi olarak çalışacak. 
        SqlCommand com2 = new SqlCommand(); //Giriş kontrol nesnesi olarak çalışacak. 

        SqlDataReader dr; //Veritabanından verileri okumak için.

        void connectionString()
        {
            con.ConnectionString = "data source=DESKTOP-M1DVH41\\SQLEXPRESS; database= MyWebSite; integrated security=SSPI;";
            //Bu satırda bir hata var düzelt onu.// not: Düzeltildi.
        }
        MyWebSiteEntities db = new MyWebSiteEntities();
        public ActionResult movies()
        {
            var degerler = db.movies.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult NewMovie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMovie(NewMovie newMovie, HttpPostedFileBase files)
        {

            if (files != null && files.ContentLength > 0)
            {
                var fileName = Path.GetFileName(files.FileName);
                var path = Path.Combine(Server.MapPath("~/img"), fileName);
                files.SaveAs(path);
            
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "insert into movies (movieName,movieInformation,image,categoryId) values('" + newMovie.movieName + "','" + newMovie.movieInformation + "','" + "../img/" +  fileName + "','" + newMovie.categoryId + "')";
            com.ExecuteNonQuery();
            con.Close();
            Response.Write("<font color=#8b3a3a><center>İşlem Başarılı.</center></font>");
            }
            return View();
        }
        public ActionResult sil(int id)
        {
            int silinecekId;
            connectionString();
            con.Open();
            com1.Connection = con;
            com1.CommandText = "select id from movies";
            dr = com1.ExecuteReader();
            while (dr.Read())
            {             
                if (id == Convert.ToInt32(dr[0]))
                {
                    silinecekId = Convert.ToInt32(dr[0]);
                }
               
            }
            dr.Close();
            com2.Connection = con;
            com2.CommandText = "delete from movies where id='" + id + "'";
            com2.ExecuteNonQuery();
            con.Close();
            Response.Write("<font color=#8b3a3a><center>İşlem Başarılı.</center></font>");
            return View("../Category/Index");
        }

      

    }
}

 
    

