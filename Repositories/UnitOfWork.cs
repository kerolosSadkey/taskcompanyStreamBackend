using DataLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
   public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlContext context;

        public UnitOfWork(SqlContext _context)
        {
            context = _context;
            Document = new Repository<document>(context);
            Priority = new Repository<Priority>(context);
            documentFiles = new Repository<document_files>(context);
        }


        public IRepository<document> Document { get; set; }
        public IRepository<Priority> Priority { get; set; }
       
       public  IRepository<document_files> documentFiles { get ; set ; }

        public void  Save()
        {
             context.SaveChanges();
        }
    }
}
