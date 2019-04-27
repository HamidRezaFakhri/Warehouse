using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Warehouse.DataAccess.Base.UOW;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> _logger;

        private readonly IUowProvider _uowProvider;

        public HomeController(IUowProvider uowProvider, ILogger<HomeController> logger)
        {
            _uowProvider = uowProvider;
            _logger = logger;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string pass)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(pass) || !IsUserValid(userName, pass))
            {
                return View("~/Views/Home/Login.cshtml", "نام کاربری و یا کلمه عبور اشتباه می باشد!");
            }

            HttpContext.Session.SetInt32("IUI", userName.Equals("Admin") ? 1 : 2);

            return RedirectToAction("Index", "Home");
        }

        private bool IsUserValid(string userName, string pass)
        {
            if (
                    (userName.Equals("Admin") && pass.Equals("Admin#321"))
                    ||
                    (userName.Equals("User") && pass.Equals("User#123"))
               )
            {
                HttpContext.Session.SetString("userName", userName);
                return true;
            }
            return false;
        }

        public ActionResult DisplayBarcode()
        {
            return View();
        }

        public ActionResult RenderBarcode(string userid)
        {
            //using (Neodynamic.Web.MVC.Barcode.BarcodeProfessional bcp = new Neodynamic.Web.MVC.Barcode.BarcodeProfessional())
            //{
            //    //Set the desired barcode type or symbology
            //    bcp.Symbology = Neodynamic.Web.MVC.Barcode.Symbology.QRCode;
            //    //Set value to encode
            //    bcp.Code = userid;
            //    //Generate barcode image
            //    byte[] imgBuffer = bcp.GetBarcodeImage(System.Drawing.Imaging.ImageFormat.Jpeg);
            //    //Write image buffer to Response obj
            //    return File(imgBuffer, "image/jpeg");
            //}

            
            Image img = null;
            using (var ms = new MemoryStream())
            {
                var writer = new ZXing.BarcodeWriter<Image>()
                {
                    Format = ZXing.BarcodeFormat.CODE_128,
                    //Options = new EncodingOptions
                    //{
                    //    Height = 80,
                    //    Width = 280,
                    //    PureBarcode = true,
                    //    Margin = 10,
                    //},
                };
                writer.Options.Height = 80;
                writer.Options.Width = 280;
                writer.Options.PureBarcode = true;
                img = writer.Write(userid);
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return File(ms.ToArray(), "image/jpeg");
            }
        }

        //[ServiceFilter(typeof(AuthenticationFilter))]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Hi");
            Log.Logger.ForContext("OtherData", "Test Data").Information("Index method called!!!");
            //await Seed();

            IEnumerable<Store> stores = null;

            using (var uow = _uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Store>();

                stores = await repository.GetAllAsync();
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Seed()
        {
            var stores = new List<Store>
            {
                new Store{ Name = "انبار مرکزی"}
            };

            using (var uow = _uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Store>();

                foreach (var item in stores)
                {
                    repository.Add(item);
                }

                await uow.SaveChangesAsync();
            }

            return View();
        }
    }
}
