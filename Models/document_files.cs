using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class document_files
    {
         [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string path { get; set; }
        #region relation 
        [ForeignKey("document")]
        public int DocuemntID { get; set; }
        public document document { get; set; }
        #endregion
    }
}
