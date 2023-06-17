﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Library.Controllers
{
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return this.RedirectToAction("All", "Books");
            }

            return this.View();
        }
    }
}