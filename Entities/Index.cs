using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CK_CDO_Final.Entities
{
    [Table("Index")]
    public class Index
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Chỉ số")]
        [Required(ErrorMessage = "Không được để trống")]
        [MaxLength(10)]
        public string CHISO { get; set; }

        [Display(Name = "Ngày")]
        [Required(ErrorMessage = "Không được để trống")]
        public DateTime NGAY { get; set; }

        [Display(Name = "Mở cửa")]
        [Required(ErrorMessage = "Không được để trống")]
        public float MOCUA { get; set; }

        [Display(Name = "Trần")]
        [Required(ErrorMessage = "Không được để trống")]
        public float TRAN { get; set; }

        [Display(Name = "Sàn")]
        [Required(ErrorMessage = "Không được để trống")]
        public float SAN { get; set; }

        [Display(Name = "Đóng cửa")]
        [Required(ErrorMessage = "Không được để trống")]
        public float DONGCUA { get; set; }

        [Display(Name = "Khối lượng")]
        [Required(ErrorMessage = "Không được để trống")]
        public long KHOILUONG { get; set; }
    }
}
