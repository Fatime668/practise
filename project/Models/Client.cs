using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
