
using MadeInCanadaForum.Models;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using MadeInCanadaForum.Data;

namespace MadeInCanadaForum.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {

        }

        //Home page - all discussions - ../ or ../Home/Index
        public IActionResult Index()
        {
            //create a list of discussions
            List<Discussion> discussions = new List<Discussion>();



            //create 3 discussions
            Discussion discussion1 = new Discussion();
            discussion1.DiscussionId = 1;
            //discussion1.Title = "What's your favourite made-in-Canada company?";
            discussion1.Content = "Do you recommend any companies that manufacture or produce in Canada for every day items that the average person should buy? https://www.reddit.com/r/AskACanadian/comments/1i672cb/whats_your_favourite_madeincanada_company/";
            discussion1.CreateDate = DateTime.Now;
            discussion1.ImageFilename = "discussion1.webp";
            //discussion1.IsVisible = true;

            Discussion discussion2 = new Discussion();
            discussion2.DiscussionId = 2;
            //discussion2.Title = "Looking for Canadian Clothing Companies";
            discussion2.Content = "I have been trying to find smaller Canadian clothing makers, and MAN is it tough. If you try googling you still get large brands that show up, the SHOP app isn't helpful and Etsy is a nightmare. Even Instagram is only useful if you can find shops through tags with other shops. I am mostly looking for \"Small boutique\" type shops. https://www.reddit.com/r/MadeInCanada/comments/15c7pb0/looking_for_canadian_clothing_companies/";
            discussion2.CreateDate = DateTime.Now;
            discussion2.ImageFilename = "discussion2.webp";
            //discussion2.IsVisible = true;

            Discussion discussion3 = new Discussion();
            discussion3.DiscussionId = 3;
            //discussion3.Title = "Are you ready to buy only Canadian or non American products whenever possible?";
            discussion3.Content = "If so, help me update my list! UPDATES as of FEBRUARY 3 please keep the suggestions and corrections coming. Also bring on the snark from the weasel naysayers. https://www.reddit.com/r/AskCanada/comments/1icgizk/are_you_ready_to_buy_only_canadian_or_non/";
            discussion3.CreateDate = DateTime.Now;
            discussion3.ImageFilename = "discussion3.webp";
            //discussion3.IsVisible = true;
            //add to list
            discussions.Add(discussion1);
            discussions.Add(discussion2);
            discussions.Add(discussion3);

            // Pass the list of discussions to the view
            return View(discussions);
        }

        //Discussion Page - one discussion - ../Home/DiscussionDetails/id
        public IActionResult DiscussionDetails(int id)
        {
            //Create a discussion
            Discussion discussion = new Discussion();

            discussion.DiscussionId = id;
            //discussion.Title = "Can we all share Made in Canada products?";
            discussion.Content = "While we shop at any Costco location, can we all keep sharing Made in Canada products. Yes we can look it up while we shop, but if we have something handy, don¡¯t you think it will be handy? https://www.reddit.com/r/CostcoCanada/comments/1ifm558/can_we_all_share_made_in_canada_products/";
            discussion.CreateDate = DateTime.Now;
            discussion.ImageFilename = "madeInCanada.svg";
            discussion.IsVisible = true;

            // Pass the discussion to the view
            return View(discussion);
        }



        //Privacy page
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
