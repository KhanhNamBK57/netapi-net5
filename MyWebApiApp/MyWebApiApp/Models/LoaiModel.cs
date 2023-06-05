using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApiApp.Models
{
    public class LoaiModelVM
    {
     
        [Required]
        [MaxLength(50)]
        public string TenLoai { get; set; }

    }

    public class LoaiModel : LoaiModelVM
	{
      public Guid IdLoai { get; set; }
	}
}
