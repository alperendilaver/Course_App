using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Ogretmen{
        [Key]
        [Display(Name ="Ogretmen Id")]
        public int OgretmenId {get; set;}

        
        [Display(Name ="Ogretmen Adı")]
        public string? OgretmenAd { get; set; }   


        [Display(Name ="Ogretmen Soyadı")]
        public string? OgretmenSoyad { get; set; }
        public string AdSoyad { get{
            return this.OgretmenAd+" "+this.OgretmenSoyad;
        } }
        public string? Eposta {get;set;}
        public string? Telefon { get; set; }
        public DateTime BaslamaTarihi { get; set; }

        public ICollection<Kurs> Kurslar { get; set; }  = new List<Kurs>();
    }
}