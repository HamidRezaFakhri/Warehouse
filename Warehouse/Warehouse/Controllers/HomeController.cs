using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.DataAccess.Base.UOW;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUowProvider _uowProvider;

        public HomeController(IUowProvider uowProvider)
        {
            _uowProvider = uowProvider;
        }

        public async Task<IActionResult> Index()
        {
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
