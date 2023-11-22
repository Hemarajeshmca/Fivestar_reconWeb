using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Recon_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recon.Controllers
{
    public class kendogridController : Controller
    {
        // GET: kendogrid
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult res1([DataSourceRequest] DataSourceRequest request)
        {
            //try
            //{
            //    List<kendogridmodel> obj = new List<kendogridmodel>();
            //    kendogridmodel data = new kendogridmodel();
            //    data.id = 1;
            //    data.address = "chennai";
            //    data.name = "Rahul";
            //    data.sno = 21324;
            //    obj.Add(data);
            //    return Json(obj.ToDataSourceResult(request));
            //}
            //catch (Exception e)
            //{
            //    return Json(e.Message);
            //}
            return View();
        }
    }
}