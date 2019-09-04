using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebStoreUi.Controllers
{
    public class UploadController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                // сохраняем файл в папку Files в проекте
                upload.SaveAs(Server.MapPath("~/Files/" + fileName));
            }
            return RedirectToAction("Index");
        }

        //Даже если мы все сделаем правильно, мы можем получить ошибку, если попытаемся загрузить файл размером больше 4 МБ.
        //4 мегабайта - ограничение, действующее на стороне сервера. Однако мы можм его переопределить.

        [HttpPost]
        public ActionResult UploadMany(IEnumerable<HttpPostedFileBase> uploads)
        {
            foreach (var file in uploads)
            {
                if (file != null)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = System.IO.Path.GetExtension(file.FileName);

                    var Random = new Random();
                    fileName += Random.Next().ToString();
                    fileName += extension;

                  //  System.IO.Path.ChangeExtension(fileName, extension); 
                    // сохраняем файл в папку Files в проекте
                    file.SaveAs(Server.MapPath("~/Files/" + fileName));
                }
            }

            return RedirectToAction("Index");
        }
    }
}