using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        public string AddToCart(int id, string title)
        {
            return string.Format("Item {0}-{1} added to cart", id, title);
        }
       
    }
}