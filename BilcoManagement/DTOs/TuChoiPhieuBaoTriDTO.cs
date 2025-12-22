using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class TuChoiPhieuBaoTriDTO
    {
        [Required]
        public int MaPhieu { get; set; }
        
        [Required(ErrorMessage = "Lý do từ chối không được để trống")]
        [StringLength(500, ErrorMessage = "Lý do từ chối không được vượt quá 500 ký tự")]
        public string LyDoTuChoi { get; set; }
    }
}
