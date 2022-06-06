using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class document
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public  DateTime date { get; set; }
        public DateTime CreatedDate { get; set; }

        public  DateTime Due_date { get; set; }

        #region relation 
             [ForeignKey("priority")]
            public int priorityID { get; set; }
            public Priority priority { get; set; }

          public Collection<document_files> document_Files { get; set; }

        #endregion
        public document()
        {

        }
    }
}
