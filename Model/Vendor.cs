using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Vendor
    {
        [Key]
        [DisplayName("Vendor ID")]
        public Guid VendorID { get; set; }
        [DisplayName("Vendor Name")]
        public string VendorName { get; set; }
        [DisplayName("Vendor Info")]
        public string VendorInfo { get; set; }
    }
}
