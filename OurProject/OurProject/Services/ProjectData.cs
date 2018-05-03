using Microsoft.EntityFrameworkCore;
using OurProject.Entities;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurProject.Services
{
    public interface IProjectData
    {
        IEnumerable<Project> GetAll();
        Project Add(Project newProject);
        void commit();
        Project Get(int id);
    }

    public class SqlIProjectData : IProjectData
    {
        private OurProjectDbContext _context;

        public SqlIProjectData(OurProjectDbContext Context)
        {
            _context = Context;
        }

        public Project Add(Project newProject)
        {
            _context.Add(newProject);
            return newProject;
        }

        public void commit()
        {
            _context.SaveChanges();
        }

        public Project Get(int id)
        {
            return _context.Projects.FirstOrDefault(r => r.ProjectId == id);
        }     

        //public async Task<IEnumerable<Project>> GetAll(int page=1)
        //{
        //    var qry = _context.Projects.AsNoTracking().OrderBy(p => p.ProjectName);
        //    var model = await PagingList.CreateAsync(qry, 10, page);
        //    return model;
        //}

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects;
        }
    }   
}
