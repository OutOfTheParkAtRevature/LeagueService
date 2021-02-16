using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataTransfer
{
    public class CreateLeagueDto
    {
        [Required]
        public string LeagueName { get; set; }

        [Required]
        public string SportName { get; set; }
    }
}
