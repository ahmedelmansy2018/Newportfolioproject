using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BL.Enitites;

using Microsoft.AspNetCore.Hosting;
using Web.ViewModel;
using System.IO;
using BL.Interfaces;
using AutoMapper;

namespace Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IUnitOfWork<Project>_project;
        private  IWebHostEnvironment _webHost;
        private readonly IMapper _mapper;

        public ProjectsController(IUnitOfWork<Project> Project
            , IWebHostEnvironment webHost
            , IMapper mapper)
        {
            _project = Project;
            _webHost = webHost;
           _mapper = mapper;
        }

        // GET: Projects
        public IActionResult Index()
        {
            return View(_project.Entity.GetAll());
        }

        // GET: Projects/Details/5
        public IActionResult  Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = _project.Entity.GetByID(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.File!=null)
                {
                    string Uploads = Path.Combine(_webHost.WebRootPath, @"img\portfolio");
                    string FullPath = Path.Combine(Uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(FullPath, FileMode.Create));
                    //var Project = new Project
                    //{
                    //    ProjectName = model.ProjectName,
                    //    ImageProject = model.File.FileName,
                    //    Description = model.Description
                    //};
                    //Automapper
                    Project P = _mapper.Map<Project>(model);
                    _project.Entity.Insert(P);
                    _project.Save();
                    return RedirectToAction(nameof(Index));
                }
                
            }
            return View(model);
        }

        // GET: Projects/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = _project.Entity.GetByID(id);
            if (project == null)
            {
                return NotFound();
            }
            //viewmodel
            //ProjectViewModel model = new ProjectViewModel
            //{
            //    Id = project.Id,
            //    ProjectName = project.ProjectName,
            //    ImageProject = project.ImageProject,
            //    Description = project.Description
            //};
            //Auto mapper
            ProjectViewModel model = _mapper.Map<ProjectViewModel>(project);

            return View(model);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult  Edit(Guid id, ProjectViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string Uploads = Path.Combine(_webHost.WebRootPath, @"img\portfolio");
                    string FullPath = Path.Combine(Uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(FullPath, FileMode.Create));
                    //var Project = new Project
                    //{
                    //    Id=model.Id,
                    //    ProjectName = model.ProjectName,
                    //    ImageProject = model.File.FileName,
                    //    Description = model.Description
                    //};
                    //AutoMapper
                    Project P = _mapper.Map<Project>(model);

                    _project.Entity.Update(P);
                    _project.Save();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Projects/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = _project.Entity.GetByID(id) ;
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _project.Entity.Delete(id);
            _project.Save();
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        public IActionResult Deleted(Guid id)
        {
            //if (id == null)
            //    return BadRequest();

            //var movie = _project.Entity.GetByID(id);

            //if (movie == null)
            //    return NotFound();

            _project.Entity.Delete(id);
            _project.Save();

            return Ok();
        }
        private bool ProjectExists(Guid id)
        {
            return _project.Entity.GetAll().Any(e => e.Id == id);
        }
    }
}
