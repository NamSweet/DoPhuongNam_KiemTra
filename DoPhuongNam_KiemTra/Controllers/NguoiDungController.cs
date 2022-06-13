using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoPhuongNam_KiemTra.Models;
namespace DoPhuongNam_KiemTra.Controllers
{
    public class NguoiDungController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        [HttpGet]
        public ActionResult DangKySinhVien()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKySinhVien(FormCollection collection, DangKy dk)
        {

            var NgayDK = String.Format("{0:MM/dd/yyyy}", collection["NgayDK"]);
            var MaSV = collection["MaSV"];
            var XacNhanMaSV = collection["XacNhanMaSV"];
            if (String.IsNullOrEmpty(MaSV))
            {
                ViewData["NhapMSV"] = "Phải nhập mã Sinh Viên!";
            }
            else
            {
                if (!MaSV.Equals(XacNhanMaSV))
                {
                    ViewData["MaSVgiongNhau"] = "Mã số sinh viên phải giống nhau";
                }
                else
                {

                    dk.NgayDK = DateTime.Parse(NgayDK);
                    dk.MaSV = MaSV;

                    data.DangKies.InsertOnSubmit(dk);
                    data.SubmitChanges();

                    return RedirectToAction("ListSinhVien");
                }
            }
            return this.DangKySinhVien();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var MaSV = collection["MaSV"];
            DangKy dk = data.DangKies.SingleOrDefault(n => n.MaSV == MaSV);
            if (dk != null)
            {
                ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                Session["TaiKhoan"] = dk;
            }
            else
            {
                ViewBag.ThongBao = "MSSV không đúng";
            }
            return RedirectToAction("ListSinhVien", "SinhVien");
        }
    }
}