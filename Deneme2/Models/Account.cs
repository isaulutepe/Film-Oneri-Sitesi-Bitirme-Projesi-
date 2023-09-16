using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Deneme2.Models
{
    public class Account
    {
        public string Name { get; set;} //Kullancıı girişden gelen isim verisi.
        public string Password { get; set; } //kullanıcı girişten gelen şifre verisi.
        public string Name1 { get; set; } //Üye ol'dan gelen isim verisi.
        public string Password1 { get; set; } //Üye ol'dan gelen şifre verisi.
        public string Aksiyon { get; set; } //Araba resmi seçilirse On değeri döndür.
        public string Komedi { get; set; } //Komedi resmi seçilirse On değeri döndür.
        public string Dram { get; set; } //Dram resmi seçilirse On değeri döndür.
        public string Abdmovie { get; set; } //Dizi resmi seçilirse On değeri döndür.
        public string Animasyon { get; set; } //Aksiyon resmi seçilirse On değeri döndür.
        public string Bilimkurgu { get; set; } //Romantik resmi seçilirse On değeri döndür.
        public string Dizi { get; set; } //Romantik resmi seçilirse On değeri döndür.
        public string Romantik { get; set; } //Romantik resmi seçilirse On değeri döndür.
        public string AileFilmleri { get; set; } //Romantik resmi seçilirse On değeri döndür.
      
    }
    public class Movies
    {
        public int id { get; set; }
        public string movieName { get; set; }
        public string movieInformation { get; set; }
        public string image { get; set; }
        public string category { get; set; }

    }
    public class Choses //Kullanıcı seçimlerini veritabanından almak için.
    {
        public string username { get; set; }
        public string chose { get; set; }

    }
    public class NewUser
    {
        public string Name1 { get; set; } //Kullancıı girişden gelen isim verisi.
        public string Password1 { get; set; } //kullanıcı girişten gelen şifre verisi.     
        public string Aksiyon { get; set; } //Araba resmi seçilirse On değeri döndür.
        public string Komedi { get; set; } //Komedi resmi seçilirse On değeri döndür.
        public string Dram { get; set; } //Dram resmi seçilirse On değeri döndür.
        public string Abdmovie { get; set; } //Dizi resmi seçilirse On değeri döndür.
        public string Animasyon { get; set; } //Aksiyon resmi seçilirse On değeri döndür.
        public string Bilimkurgu { get; set; } //Romantik resmi seçilirse On değeri döndür.
        public string Dizi { get; set; } //Romantik resmi seçilirse On değeri döndür.
        public string Romantik { get; set; } //Romantik resmi seçilirse On değeri döndür.
        public string AileFilmleri { get; set; } //Romantik resmi seçilirse On değeri döndür.

    }
    public class NewMovie
    {
        public string movieName { get; set; } //filmAdı
        public string movieInformation { get; set; } //dilmBilgi    
        public string movieImage { get; set; } //filmResim
        public string categoryId { get; set; } //filmKategoriId


    }
    public class NewCategory
    {   
        public int categoryId { get; set; } //İd
        public string categoryName { get; set; } //Kategori Adı    
      
    }

}