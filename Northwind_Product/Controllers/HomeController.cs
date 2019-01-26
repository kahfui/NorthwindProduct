using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Northwind_Product.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Dynamic view with fields mapping from Product configuration editor.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Normal view with listing and binding through POCO
        /// </summary>
        /// <returns></returns>
        public ActionResult Index2()
        {                     
            return View();
        }

        /// <summary>
        /// Dynamic view with result of search indexing from Product configuration editor.
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchIndex()
        {
            return View();
        }
    }
}