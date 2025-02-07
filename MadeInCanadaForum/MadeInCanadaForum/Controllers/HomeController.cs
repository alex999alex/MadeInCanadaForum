
using MadeInCanadaForum.Models;
using Microsoft.AspNetCore.Mvc;

namespace MadeInCanadaForum.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {

        }

        //Home page - all discussions
        public IActionResult Index()
        {
            //create a list of discussions

            //create 3 discussions

            //add to list

            return View();
        }

        //Discussion Page
        public IActionResult DiscussionDetails(int id)
        {
            //Create a discussion
            Discussion discussion = new Discussion();

            discussion.DiscussionId = id;
            discussion.Title = "Can we all share Made in Canada products?";
            discussion.Content = "While we shop at any Costco location, can we all keep sharing Made in Canada products. Yes we can look it up while we shop, but if we have something handy, don¡¯t you think it will be handy? https://www.reddit.com/r/CostcoCanada/comments/1ifm558/can_we_all_share_made_in_canada_products/";
            discussion.CreateDate = DateTime.Now;
            discussion.ImageFilename = "madeInCanada.svg";
            discussion.IsPublic = true;

            return View();
        }



        //Privacy page
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
