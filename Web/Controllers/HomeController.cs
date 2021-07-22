using BL.Enitites;
using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModel;
namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Project> _project;
        private readonly IUnitOfWork<Owner> _owner;

        public HomeController(
            IUnitOfWork<Owner> Owner,
            IUnitOfWork<Project> Project)
        {
            _project = Project;
            _owner = Owner;
        }
        public IActionResult Index()
        {

            var HomeViewModel = new HomeViewModel
            {
                Owner = _owner.Entity.GetAll().SingleOrDefault(),
                Projects = _project.Entity.GetAll().ToList()
            };
            return View(HomeViewModel);
        }
        public IActionResult About()
        {
            return View();

        }
        public IActionResult Contact()
        {
            return View();

        }
    }
}