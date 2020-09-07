using Microsoft.AspNetCore.Mvc;
using MvcXmlDemo.Models;
using System;
using System.IO;
using System.Xml.Serialization;

namespace MvcXmlDemo.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home
        public IActionResult Index()
        {
            return View();
        }

         // POST: /Home/Index
         [HttpPost]
         public IActionResult Index(Category cat)
         {
            string msg;
            try
            {
               XmlSerializer ser = new XmlSerializer(typeof(Category));
               FileStream s = new FileStream("App_Data\\category.xml",
                                             FileMode.Create);
               ser.Serialize(s, cat);
               s.Close();
               msg = "Category has been saved";
            }
            catch (Exception ex)
            {
               msg = ex.Message;
            }
            ViewBag.Message = msg;
            return View("Save", cat);
         }

         // GET: /Home/Restore
         public IActionResult Restore()
         {
            string msg;
            Category cat = new Category();
            try
            {
               XmlSerializer ser = new XmlSerializer(typeof(Category));
               FileStream s = new FileStream("App_Data\\category.xml",
                                             FileMode.Open);
               cat = (Category)ser.Deserialize(s);
               s.Close();
               msg = "Category has been restored";
            }
            catch (Exception ex)
            {
               msg = ex.Message;
            }
            ViewBag.Message = msg;
            return View("Restore", cat);
         }
   }
}
