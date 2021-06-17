using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CK_CDO_Final.Entities
{
    [Table("COMPANYDETAILS")]
    public class CompanyDetails
    {
        [Key]
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Mã cổ phiếu")]
        public string MA { get; set; }


        [Display(Name = "Tên doanh nghiệp")]
        [Required(ErrorMessage = "Không được để trống")]
        public string TEN { get; set; }

        [Display(Name = "Ngành nghề")]
        [Required(ErrorMessage = "Không được để trống")]
        public string NGANHNGHE { get; set; }

        [Display(Name = "Sàn")]
        [Required(ErrorMessage = "Không được để trống")]
        public string SAN { get; set; }

        [Display(Name = "Khối lượng niêm yết")]
        [Required(ErrorMessage = "Không được để trống")]
        public long KLNY { get; set; }
        
        public ICollection<Hose> HOSES { get; set; }
        public ICollection<Upcom> UPCOMS { get; set; }
        public ICollection<Hnx> HNXS { get; set; }
        public CompanyDetails()
        {
            HOSES = new HashSet<Hose>();
            UPCOMS = new HashSet<Upcom>();
            HNXS = new HashSet<Hnx>();
        }
    }
}
