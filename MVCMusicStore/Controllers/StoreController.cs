using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCMusicStore.Models;

namespace MVCMusicStore.Controllers
{
    public class StoreController : Controller
    {

        MusicStoreEntities storeDB = new MusicStoreEntities();

        //
        // GET: /Store/
        public ActionResult Index()
        {
            //return Content("Hello from Store.Index()"); 

            //var genres = new List<Genre>
            //{
            //    new Genre {Name = "Disco"},
            //    new Genre {Name = "Jazz"},
            //    new Genre {Name = "Rock"}
            //};

            
            var genres = storeDB.Genres.ToList();

            return View(genres);
        }

        //
        // GET: /Store/Browse?Genre=blah
        public ActionResult Browse(string genre)
        {
            //string message = HttpUtility.HtmlEncode("Store.Browse, Genre = " + genre);
            //return Content(message); 

            //var genreModel = new Genre { Name = genre };

            var genreModel = storeDB.Genres.Include("Albums").Single(g => g.Name == genre);
            return View(genreModel);
        }
         
        //
        // GET: /Store/Details/5
        public ActionResult Details(int id)
        {
            //string message = "Store.Details, ID = " + id;
            //return Content(message); 

            //var album = new Album { Title = "Album" + id };

            var album = storeDB.Albums.Find(id);
            return View(album);
        }

	}
}