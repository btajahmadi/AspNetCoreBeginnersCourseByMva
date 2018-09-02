using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreBeginnersCourseByMva.Pages.Models
{
    public class Customer
    {
        public Customer()
        {
            Id = Guid.NewGuid().ToString();
        }
       public string Id { get; set; }
        [Required, StringLength(15)]
        public string Name { get; set; }
    }
}