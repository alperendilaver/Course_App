using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Models{
    public class KursKayitViewModel
    {
        [Key]
        public int KayitId { get; set; }
        public int OgrenciId { get; set; }
        public int KursId { get; set; }

        public int OgretmenId { get; set; }
        
        public DateTime KayitTarihi { get; set; }
    }
}