using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Deneme2.Models.Entity;


namespace Deneme2.Controllers
{
    public class ChoosesController : Controller
    {
        // GET: Chooses
        MyWebSiteEntities db = new MyWebSiteEntities();
        public ActionResult chooses()
        {
            var degerler = db.chooses.ToList();
            return View(degerler);
        }
    }
}