using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace proekt.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }
        public String name { get; set; }
        public String country { get; set; }
    }
}