using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApiApp.Data
{
    [Table("HangHoa")]
    public class HangHoa
    {
        [Key]
        public Guid IdHangHoa { get; set; }

        [Required]
        [MaxLength(300)]
        public string TenHangHoa { get; set; }

        public string MoTa { get; set; }

        [Range(0,double.MaxValue)]
        public double DonGia { get; set; }

        public byte GiamGia { get; set; }

        public Guid? IdLoai { get; set; }

        [ForeignKey("IdLoai")]
        public Loai Loai { get; set; }

        public ICollection<DonHangChiTiet> DonHangChiTiets { get; set; }

        public HangHoa()
        {
            DonHangChiTiets = new List<DonHangChiTiet>();
        }
    }
}
