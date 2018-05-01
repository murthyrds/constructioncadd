using OurProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects;
        }        
    }   
}
