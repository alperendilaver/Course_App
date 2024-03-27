using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Data
{
    public class DataContext : DbContext{
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }
        public DbSet<Kurs> Kurslar => Set<Kurs>();

        public DbSet<Ogrenci> ogrenciler => Set<Ogrenci>();

        public DbSet<KursKayit> kursKayitlari=> Set<KursKayit>();

        public DbSet<Ogretmen> Ogretmenler=> Set<Ogretmen>();
    }
}