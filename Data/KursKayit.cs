using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data{
    public class KursKayit
    {
        [Key]
        public int KayitId { get; set; }
        public int OgrenciId { get; set; }
        public Ogrenci ogrenci {get;
        set;}=null!;//ogrenci tablosuna join işlemi yapar
        public int KursId { get; set; }

        public Kurs kurs { get; set; } = null!;//navigation property

        public Ogretmen ogretmen {get;set;}=null!;
        public int? OgretmenId { get; set; }
        
        public DateTime KayitTarihi { get; set; }
    }
}