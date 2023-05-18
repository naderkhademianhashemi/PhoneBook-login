using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entity
{
    
    public class Phone
    {
        public int Id { get; set; }
        
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "فامیلی")]
        public string Family { get; set; }

        [Display(Name = "تلفن")]
        public string Tell { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        
    }
}