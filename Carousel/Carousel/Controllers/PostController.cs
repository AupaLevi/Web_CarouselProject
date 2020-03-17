using System;
using Carousel.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Carousel.Controllers
{
    public class PostController : Controller
    {

        public ActionResult AddNewPost()
        {
            SQLServerConnector sqlServerConnector = new SQLServerConnector();
            List<PostDataCarousel> listPosts;
            listPosts = sqlServerConnector.getPostsList();

            ViewBag.ListOfPosts = listPosts;


            return View(listPosts);
        }


        public ActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "aaz02,aaz03 ")] PostDataCarousel postdatacarousel)
        {
            SQLServerConnector sqlServerConnector = new SQLServerConnector();
            List<PostDataCarousel> listPosts;
            listPosts = sqlServerConnector.getPostsList();

            postdatacarousel.Aaz01 = DateTime.Now.ToString("yyyyMMddHHmmss");
            //postdatacarousel.Aaz02 = "";
            //postdatacarousel.Aaz03 = "";
            postdatacarousel.Aaz05 = DateTime.Now.ToString("yyyy-MM-dd ");



            String result = sqlServerConnector.InsertPostData(postdatacarousel);

            return RedirectToAction("AddNewPost", "Post");
        }//Create

        public ActionResult DeletePost(String postID)
        {
            String sqlCriteria = "";
            if (postID != null && !postID.IsEmpty())
            {
                if (postID.StartsWith("*"))
                {
                    postID = postID.Remove(1, 1);
                }
                if (postID.EndsWith("*"))
                {
                    postID = postID.Remove(postID.Length - 1, postID.Length);
                }
                sqlCriteria = "aaz01 LIKE '%" + postID + "%' ";
            }

            SQLServerConnector sqlServerConnector = new SQLServerConnector();
            List<PostDataCarousel> listPosts;
            listPosts = sqlServerConnector.getPostsListOnDemand(sqlCriteria);
            ViewBag.ListOfPosts = listPosts;
            return View("ConfirmDelete", listPosts);
        }//End of DeletePost

        [HttpPost, ActionName("ConfirmedDelete")]
        public ActionResult ConfirmedDeletePost(String postID)
        {
            SQLServerConnector sqlServerConnector = new SQLServerConnector();
            List<PostDataCarousel> listPosts = new List<PostDataCarousel>();
            String result = sqlServerConnector.ConfirmedDelete(postID);
            if (result == "SUCCESS")
            {
                listPosts = sqlServerConnector.getPostsList();
            }
            ViewBag.ListOfPosts = listPosts;

            return RedirectToAction("AddNewPost", "Post");
        }// End of ConfirmedDeletePost

        public ActionResult EditPost(String postID)
        {
            SQLServerConnector sqlServerConnector = new SQLServerConnector();
            List<PostDataCarousel> listPosts;
            PostDataCarousel postDataCarouselForEdit;
            String sqlCriteria = "aaz01 = '" + postID + "'";

            listPosts = sqlServerConnector.getPostsListOnDemand(sqlCriteria);
            postDataCarouselForEdit = listPosts[0];

            ViewBag.PostDataForEdit = postDataCarouselForEdit;

            return View("EditPost", postDataCarouselForEdit);
        }


        [HttpPost, ActionName("ConfirmedEdit")]
        public ActionResult UpdatePost([Bind(Include = "Aaz01,Aaz02,Aaz03")] PostDataCarousel postdatacarousel)
        {
            SQLServerConnector sqlServerConnector = new SQLServerConnector();

            postdatacarousel.Aaz05 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            String result = sqlServerConnector.ConfirmedEdit(postdatacarousel);
            List<PostDataCarousel> listPosts = new List<PostDataCarousel>();
            if (result == "SUCCESS")
            {
                listPosts = sqlServerConnector.getPostsList();
            }

            ViewBag.ListOfPosts = listPosts;
            return RedirectToAction("AddNewPost", "Post");
        }
    }
}