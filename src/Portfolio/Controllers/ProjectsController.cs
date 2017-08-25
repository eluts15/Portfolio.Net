
using Microsoft.AspNetCore.Mvc;
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
            var repoList = StarredRepos.GetStarredRepos();
            return View(repoList);
        }
    }
}
