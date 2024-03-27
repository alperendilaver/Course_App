using Microsoft.AspNetCore.Mvc;
using efcoreApp.Data;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using Microsoft.AspNetCore.Mvc.Rendering;
using efcoreApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace efcoreApp.Data
{
    public class KursController:Controller{
         private readonly DataContext _context;
         public KursController(DataContext dataContext)
         {
            _context = dataContext;
         }

        [HttpGet]
        public async Task< IActionResult> Create(){
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(),"OgretmenId","AdSoyad");
            return View();
        }

        [HttpPost]    
        public async Task< IActionResult> Create(KursViewModel kurs){
            if(kurs ==null)
                return NotFound();
            if(ModelState.IsValid){
                //parametre olarak KursViewModel alıyoruz ancak bunu Kurs sınıfına çevirerek veritabanına yazıyoruz
                _context.Kurslar.Add(new Kurs(){
                    Baslik=kurs.Baslik,
                    KursId = kurs.KursId,
                    OgretmenId = kurs.OgretmenId
                });
                await _context.SaveChangesAsync();//VERİTABANINA AKTARMA AŞAMASI
                return RedirectToAction("Index");
            }
              ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(),"OgretmenId","AdSoyad");
          
            return View(kurs);
        }

        public async Task<IActionResult> Index(){
            var kurslar = await _context.Kurslar.Include(o=>o.ogretmen).ToListAsync();
            return View(kurslar);
        }


        public async Task<IActionResult> Update(int? id){
            if(id==null)
                return NotFound();
            var kurs = await _context.Kurslar.Include(k=>k.KursKayitlari).ThenInclude(k=>k.ogrenci).
            Select(k=> new KursViewModel{
                Baslik = k.Baslik,
                KursId = k.KursId,
                OgretmenId = k.OgretmenId,
                KursKayitlari =k.KursKayitlari
            })
            .FirstOrDefaultAsync(p=>p.KursId==id);
            if(kurs == null)
                return NotFound();
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(),"OgretmenId","AdSoyad");
           
            return View(kurs);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,KursViewModel kurs)
        {
            if(id!=kurs.KursId)
                return NotFound();
            if(kurs==null)
                return NotFound();
            if(ModelState.IsValid){
                try
                {
                    _context.Kurslar.Update(new Kurs{
                        Baslik = kurs.Baslik,
                        KursId = kurs.KursId,
                        OgretmenId = kurs.OgretmenId
                    });
                    await _context.SaveChangesAsync();
                                
                }
                catch (DbUpdateException)
                {
                    if(!_context.Kurslar.Any(p=>p.KursId==kurs.KursId))
                        return NotFound();
                    else
                        throw;
                }
                
            return RedirectToAction("Index");
        
            }
              ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(),"OgretmenId","AdSoyad");
          
            return View(kurs);
        }
    
        public async Task<IActionResult> Delete(int? id){
            if(id==null)
                return NotFound();           
            var kurs = await _context.Kurslar.FindAsync(id);
            if(kurs==null)
                return NotFound();
            return View(kurs);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([Bind("KursId")]Kurs model){

            var krs = await _context.Kurslar.FindAsync(model.KursId);
            if (krs==null)
                 return NotFound();
            _context.Kurslar.Remove(krs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }

}