using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OurProject.Entities;
using OurProject.Models;
using OurProject.Models.Attachments;
using OurProject.Services;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ReflectionIT.Mvc.Paging;


namespace OurProject.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        
        private IProjectData _projectData;
        private UserManager<User> _userManager;
        private readonly IHostingEnvironment _env;
        private readonly IFileProvider _fileProvider;
        private readonly OurProjectDbContext _context;

        public ProjectController(IProjectData projectData, UserManager<User> userManager, IHostingEnvironment env, 
            IFileProvider fileProvider, OurProjectDbContext Context)
        {
            _projectData = projectData;
            _userManager = userManager;
            _env = env;
           this._fileProvider = fileProvider;
            _context = Context;
        }
        [HttpGet]
        public IActionResult Create()
        {          
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var newProject = new Project();
                newProject.ProjectNumber = model.ProjectNumber;
                newProject.ProjectName = model.ProjectName;
                newProject.StockLength = model.StockLength;
                newProject.StandardsType = model.StandardsType;
                newProject.AccessoriesList = model.AccessoriesList;
                newProject.SuportBar = model.SuportBar;
                newProject.TitleBlock = model.TitleBlock;
                newProject.Description = model.Description;
                newProject.Link = model.Link;
                newProject = _projectData.Add(newProject);
                //_projectData.commit();
                var userid = _userManager.GetUserId(HttpContext.User);

                //Attachments Upload.
                if (file == null || file.Length == 0)
                {
                    Content("file not found");
                }
                else
                {
                    var webRoot = _env.WebRootPath;                    
                    string userId = Convert.ToString(userid);
                    var dateTime = DateTime.Now.ToString("dd-mm-yyyy");
                    if (!System.IO.Directory.Exists(webRoot + "/UserFiles/"))
                    {
                        System.IO.Directory.CreateDirectory(webRoot + "/UserFiles/");
                    }
                    if (!System.IO.Directory.Exists(webRoot + "/UserFiles/" + userId + "/" + dateTime + "/"))
                    {
                        System.IO.Directory.CreateDirectory(webRoot + "/UserFiles/" + userId + "/" + dateTime + "/");
                    }

                    //Creating dynamic folders for every user and every day
                    var path = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot" + "/UserFiles/" + userId + "/" + dateTime + "/", file.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    newProject.AttachmentName = path;
                }
                newProject.UserId = userid;
                
                //Save the project in db. 
                _projectData.commit();

                //Once record is saved view page redirected to Details.
                return RedirectToAction("Details");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int page=1)
        {
            var model = new ProjectDetailsModel();
            model.Projects = _projectData.GetAll();
            return View(model);

            //return View(await _context.Projects.ToListAsync());

            //var qry = _context.Projects.AsNoTracking().OrderBy(p => p.ProjectName);
            //var model = await PagingList.CreateAsync(qry, 10, page);
            //return View(model);
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var model = _projectData.Get(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _projectData.Get(id);            
            if (model == null)
            {                
                return RedirectToAction("Index");
            }            
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectModel model, IFormFile file, FilesViewModel mod)
        {
            var project = _projectData.Get(id);            
            if (ModelState.IsValid)
            {
                project.ProjectNumber = model.ProjectNumber;
                project.ProjectName = model.ProjectName;
                project.StockLength = model.StockLength;
                project.StandardsType = model.StandardsType;
                project.AccessoriesList = model.AccessoriesList;
                project.SuportBar = model.SuportBar;
                project.TitleBlock = model.TitleBlock;
                project.Description = model.Description;
                project.Link = model.Link;

                //Get Uploaded attachments.
                var newItem = new FilesViewModel();
                foreach (var item in this._fileProvider.GetDirectoryContents(""))
                {
                    mod.Files.Add(
                        new FileDetails { Name = item.Name, Path = item.PhysicalPath });
                }                

                if (file == null || file.Length == 0)
                    return Content("file not found");
                
                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        file.GetFilename());

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                project.AttachmentName = file.FileName;
                _projectData.commit();
                return RedirectToAction("Details");                
            }
            return View();
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var model = _projectData.Get(id);
            if(model != null)
            {
                _context.Projects.Remove(_context.Projects.FirstOrDefault(e => e.ProjectId == id));
                _projectData.commit();
                return RedirectToAction("Details");
            }
            return RedirectToAction("Details");
        }
    }
}