
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetStarredRepos()
        {
            List<StarredRepos> repoList = StarredRepos.GetStarredRepos();
            return View(repoList);
        }
    }
}
