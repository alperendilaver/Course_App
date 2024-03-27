using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data{
    public class Kurs{

        [Display(Name ="Kurs Id")]
        public int KursId { get; set; }
        
        [Display(Name ="Kurs Adı")]
        public string? Baslik { get; set; }

        public Ogretmen ogretmen {get;set;}=null!;
        public int OgretmenId {get;set;}

        
        public ICollection<KursKayit> KursKayitlari {get;set;} = new List<KursKayit>();
        

    }
}