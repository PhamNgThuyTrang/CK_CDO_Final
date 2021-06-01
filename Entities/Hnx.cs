using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CK_CDO_Final.Entities
{
    [Table("Hnx")]
    public class Hnx
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Mã cổ phiếu")]
        [Required(ErrorMessage = "Không được để trống")]
        [MaxLength(10)]
        public string MA { get; set; }

        [Display(Name = "Ngày")]
        [Required(ErrorMessage = "Không được để trống")]
        public DateTime NGAY { get; set; }

        [Display(Name = "Giá mở cửa")]
        [Required(ErrorMessage = "Không được để trống")]
        public float GIAMOCUA { get; set; }

        [Display(Name = "Giá trần")]
        [Required(ErrorMessage = "Không được để trống")]
        public float GIATRAN { get; set; }

        [Display(Name = "Giá sàn")]
        [Required(ErrorMessage = "Không được để trống")]
        public float GIASAN { get; set; }

        [Display(Name = "Giá đóng cửa")]
        [Required(ErrorMessage = "Không được để trống")]
        public float GIADONGCUA { get; set; }

        [Display(Name = "Khối lượng")]
        [Required(ErrorMessage = "Không được để trống")]
        public long KHOILUONG { get; set; }
    }
}
