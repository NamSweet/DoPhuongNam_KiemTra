using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoPhuongNam_KiemTra.Models;
namespace DoPhuongNam_KiemTra.Controllers
{
    public class SinhVienController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        // GET: SinhVien
        public ActionResult ListSinhVien()
        {
            var All_sinhvien = from tt in data.SinhViens select tt;

            return View(All_sinhvien);
        }
        public ActionResult Detail(string id)
        {
            var D_sinhvien = data.SinhViens.Where(m => Convert.ToString(m.MaSV) == id).First();
            return View(D_sinhvien);
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, SinhVien s)
        {
            var E_MaSV = collection["MaSV"];
            var E_HoTen = collection["HoTen"];
            var E_GioiTinh = collection["GioiTinh"];
            var E_Hinh = collection["Hinh"];
            var E_NgaySinh = Convert.ToDateTime(collection["NgaySinh"]);
            var E_MaNganh = collection["MaNganh"];
            if (string.IsNullOrEmpty(E_HoTen))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.MaSV = E_MaSV;
                s.HoTen = E_HoTen.ToString();
                s.GioiTinh = E_GioiTinh.ToString();
                s.NgaySinh = E_NgaySinh;
                s.Hinh = E_Hinh.ToString();
                s.MaNganh = E_MaNganh.ToString();
                data.SinhViens.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("ListSinhVien");
            }
            return this.Create();
        }
        public ActionResult Edit(string id)
        {
            var E_sinhvien = data.SinhViens.First(m => Convert.ToString(m.MaSV) == id);
            return View(E_sinhvien);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var E_sinhvien = data.SinhViens.First(m => Convert.ToString(m.MaSV) == id);
            var E_hoten = collection["HoTen"];
            var E_gioitinh = collection["GioiTinh"];
            var E_ngaysinh = Convert.ToDateTime(collection["NgaySinh"]);
            var E_hinh = collection["Hinh"];
            var E_manganh = collection["MaNganh"];
            E_sinhvien.MaSV = id.ToString();

            if (string.IsNullOrEmpty(E_hoten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_sinhvien.HoTen = E_hoten;
                E_sinhvien.GioiTinh = E_gioitinh;
                E_sinhvien.NgaySinh = E_ngaysinh;
                E_sinhvien.Hinh = E_hinh;
                E_sinhvien.MaNganh = E_manganh;
                UpdateModel(E_sinhvien);
                data.SubmitChanges();
                return RedirectToAction("ListSinhVien");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(string id)
        {
           var E_sinhvien = data.SinhViens.First(m => Convert.ToString(m.MaSV) == id);
            return View(E_sinhvien);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var E_sinhvien = data.SinhViens.First(m => Convert.ToString(m.MaSV) == id);
            data.SinhViens.DeleteOnSubmit(E_sinhvien);
            data.SubmitChanges();
            return RedirectToAction("ListSinhVien");
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/Images/" + file.FileName));
            return "/Content/Images/" + file.FileName;
        }
    }
}