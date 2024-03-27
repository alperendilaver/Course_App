using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class KursKayitController : Controller{
        private readonly DataContext _context;
        public KursKayitController(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task< IActionResult> Index(){
            var kursKayitlari = await _context.kursKayitlari
            .Include(x=>x.ogrenci)//include join yapar
            .Include(x=>x.kurs)
            .ToListAsync();
            return View(kursKayitlari);
        }

        public async Task<IActionResult> Create(){
            ViewBag.Ogrenciler = new SelectList(await _context.ogrenciler.ToListAsync(),"OgrenciId","AdSoyad");
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(),"KursId","Baslik");
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(),"OgretmenId","AdSoyad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KursKayit kursKayit){
            if(kursKayit ==null)
                return NotFound();
            kursKayit.KayitTarihi = DateTime.Now;
            _context.kursKayitlari.Add(kursKayit);
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id){
            if (id == null)
                return NotFound();
            var kurs = await _context.kursKayitlari.FindAsync(id);
            if( kurs==null)
                return NotFound();
            return View(kurs);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id){
            var kurs = await _context.kursKayitlari.FirstOrDefaultAsync(o=>o.KayitId==id);

            if(kurs==null)
                return NotFound();
            
            _context.kursKayitlari.Remove(kurs);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }
    }
}