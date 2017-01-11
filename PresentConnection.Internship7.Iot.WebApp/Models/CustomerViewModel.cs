using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PresentConnection.Internship7.Iot.WebApp.Models
{
    public class CustomerViewModel
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
    }
}