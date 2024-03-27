using System.ComponentModel.DataAnnotations;
using efcoreApp.Data;

namespace efcoreApp.Models{
    public class KursViewModel{

        [Display(Name ="Kurs Id")]
        public int KursId { get; set; }
        [Display(Name ="Kurs Adı")]
        [Required]
        [StringLength(50)]        
        public string? Baslik { get; set; }
        [Required]
        public int OgretmenId {get;set;}
        
        public ICollection<KursKayit> KursKayitlari {get;set;} = new List<KursKayit>();
    }
}