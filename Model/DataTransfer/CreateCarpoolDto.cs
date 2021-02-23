using System;

namespace Model.DataTransfer
{
    public class CreateCarpoolDto
    {
        public Guid CarpoolID { get; set; }
        public string UserID { get; set; }
    }
}