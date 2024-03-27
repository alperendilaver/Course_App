using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Data
{
    public class OgretmenController:Controller{
        private readonly DataContext _context;
        public OgretmenController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(){
            var ogretmenler = await _context.Ogretmenler.ToListAsync();
            return View(ogretmenler);
        }

        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Ogretmen ogretmen){
            if(ogretmen == null)
                return NotFound();
            if(ModelState.IsValid){
               
                    ogretmen.BaslamaTarihi=DateTime.Now;
                    await _context.Ogretmenler.AddAsync(ogretmen);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
            }else
                return NotFound();
        }


        public async Task<IActionResult> Delete(int? id){
            if(id == null)
                return NotFound();
            
            var ogretmen = await _context.Ogretmenler.FindAsync(id);
            if(ogretmen == null)
                return NotFound();

            return View(ogretmen);

        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id){
            var ogretmen = await _context.Ogretmenler.Include(o=>o.Kurslar).
            ThenInclude(p=>p.KursKayitlari).FirstOrDefaultAsync(o=>o.OgretmenId==id);
            if(ogretmen==null)
                return NotFound();

            
            // ilişkili kurs kayitları
            var kursKayitlari = await _context.kursKayitlari.Where(k => k.OgretmenId == id).ToListAsync();

            // ilişkili kurs kayitları
            var kurslar = await _context.Kurslar.Where(k => k.OgretmenId == id).ToListAsync();
        
            // Kurs kayıtlarını sil
            foreach (var kayit in kursKayitlari)
            {
                _context.kursKayitlari.Remove(kayit);
            }
            
            // Kursları sil
            foreach (var kayit in kurslar)
            {
                _context.Kurslar.Remove(kayit);
            }

            _context.Remove(ogretmen);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int? id){
            if(id == null)
                return NotFound();
            var ogretmen = await _context.Ogretmenler.Include(o=>o.Kurslar).FirstOrDefaultAsync(m=>m.OgretmenId==id);
            if(ogretmen==null)  
                return NotFound();
            return View(ogretmen);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Ogretmen model,int? id)
        {
            if(id!=model.OgretmenId){
                return NotFound();
            }
            if(ModelState.IsValid){
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();//güncelleme burada yapılır
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!_context.ogrenciler.Any(o=>o.OgrenciId == model.OgretmenId)){//kayıtın veritabanında olmama durumu
                        return NotFound();
                    }
                    else
                        throw;
                }
                return RedirectToAction("Index");
            }
            return View(model);

        }

    }

}