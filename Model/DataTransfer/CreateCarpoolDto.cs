using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataTransfer
{
    public class CreateCarpoolDto
    {
        public Guid CarpoolID { get; set; }
        public string UserID { get; set; }
    }
}
