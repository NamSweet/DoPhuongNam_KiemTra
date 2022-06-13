using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoPhuongNam_KiemTra.Models;
namespace DoPhuongNam_KiemTra.Controllers
{
    public class HocPhanController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult ListHocPhan()
        {
            var All_hocphan = from ss in data.HocPhans select ss;

            return View(All_hocphan);
        }
    }
}