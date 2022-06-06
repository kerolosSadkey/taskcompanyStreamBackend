using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IUnitOfWork
    {
        IRepository<document> Document { get; set; }
        IRepository<Priority> Priority { get; set; }
        IRepository<document_files> documentFiles { get; set; }
        public void Save();
    }
}
