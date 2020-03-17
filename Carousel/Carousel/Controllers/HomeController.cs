using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Carousel.Models;

namespace Carousel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IndexModel indexModel = new IndexModel();
            SQLServerConnector sqlServerConnector = new SQLServerConnector();
            List<PostDataCarousel> postList;
            postList = sqlServerConnector.getTopPostsList(3);
            //ViewBag.ListOfPosts = postList;
            indexModel.PostDataCarousels = postList;

            //return View(postList);
            return View(indexModel);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}