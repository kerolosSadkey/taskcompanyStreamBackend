using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Models;

namespace ViewModels
{
    public class DocumentViewModel
    {
        public DocumentViewModel()
        {
            createdDate = DateTime.Now;
        }
        public int? id { get; set; }

        public string name { get; set; }

      
        public IEnumerable<document_files> documents { get; set; }

        public DateTime date { get; set; }
        public DateTime? createdDate { get; set; }

        public DateTime due_date { get; set; }

        public int priorityId { get; set; }

        public string priority { get; set; }




    }
}
