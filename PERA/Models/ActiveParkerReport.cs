using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PERA.Models
{
    public class ActiveParkerReport
    {
        public int ID { get; set; }
        public int GarageID { get; set; }
        public Garage Garage { get; set; }
        public DateTime? DateReceived { get; set; }
        public DateTime DateUploaded { get; set; }
        public DateTime MonthYear { get; set; }
        

    }
}
