using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Ogrenci{
        //id => primary key
        [Key] //propertyi primary yapar 
        //yahut Id veya OgrenciId yazdığımızda otomatik olarak primary key oluyor
        [Display(Name ="Ogrenci Id")]
        public int OgrenciId { get; set; }
        [Display(Name ="Ogrenci Adı")]
        public string? OgrenciAd { get; set; }
        [Display(Name ="Ogrenci Soyadı")]
        public string? OgrenciSoyad { get; set; }

        public string AdSoyad { get{
            return this.OgrenciAd + " "+this.OgrenciSoyad;
        }}
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }

        public ICollection<KursKayit> KursKayitlari {get;set;} = new List<KursKayit>();
        

    }
}