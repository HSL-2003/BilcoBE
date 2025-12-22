using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class DuyetPhieuBaoTriDTO
    {
        [Required]
        public int MaPhieu { get; set; }
        
        public string GhiChu { get; set; }
    }
}
