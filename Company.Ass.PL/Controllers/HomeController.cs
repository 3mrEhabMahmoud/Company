using Company.Ass.PL.Models;
using Company.Ass.PL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace Company.Ass.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            IScopedService scopedService01,
            IScopedService scopedService02,
            ITransentService transentService01,
            ITransentService transentService02,
            ISingletonService singletonService01,
            ISingletonService singletonService02)
        {
            _logger = logger;
            this.scopedService01 = scopedService01;
            this.scopedService02 = scopedService02;

            this.transentService01 = transentService01;
            this.transentService02 = transentService02;

            this.singletonService01 = singletonService01;
            this.singletonService02 = singletonService02;

        }

        public IActionResult TestLifeTime()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"scopedService01 :: {scopedService01.GetGuid()}\n");
            builder.Append($"scopedService01 :: {scopedService02.GetGuid()}\n");
            builder.Append($"scopedService01 :: {transentService01.GetGuid()}\n");
            builder.Append($"scopedService01 :: {transentService02.GetGuid()}\n");
            builder.Append($"scopedService01 :: {singletonService01.GetGuid()}\n");
            builder.Append($"scopedService01 :: {singletonService02.GetGuid()}\n");
            return builder.ToString();

        }

        public IActionResult Index()
        {
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
    }
}
