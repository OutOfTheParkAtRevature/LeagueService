using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Sport
    {
        [Key]
        [DisplayName("Sport ID")]
        public int SportID { get; set; }
        [DisplayName("Sport")]
        public string SportName { get; set; }

    }
}
