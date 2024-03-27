using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace efcoreApp.Controllers
{
    public class OgrenciController:Controller{
        private readonly DataContext _context;
        public OgrenciController(DataContext context)
        {
             _context =  context;
        }
        public async Task<IActionResult> Index(){
            var ogrenciler = await _context.ogrenciler.ToListAsync();
            return View(ogrenciler);        
        }


        public IActionResult Create(){
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model){
            _context.ogrenciler.Add(model);
            await _context.SaveChangesAsync(); 
            return RedirectToAction("Index","Ogrenci");
 
        }
        public async Task<IActionResult> Edit(int? id){
            if(id ==null)
                return NotFound();
            var ogr = await _context.ogrenciler
            .Include(o=>o.KursKayitlari)//önce kurskayıtlarına gider
            .ThenInclude(o=>o.kurs)//kurskayitlari.cs den de kursa ulaşır
            .FirstOrDefaultAsync(p=>p.OgrenciId==id);//FindAsync de kullanılabilir FindAsync(id);

            if(ogr == null)
                return NotFound();
            
            return View(ogr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //html kodu incelendiğinde form altında gizli bir token inputu vardır 
        //bu tokenların validationunu yapar
        public async Task<IActionResult> Edit(Ogrenci model,int id){
            if(id!=model.OgrenciId){
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
                    if(!_context.ogrenciler.Any(o=>o.OgrenciId == model.OgrenciId)){//kayıtın veritabanında olmama durumu
                        return NotFound();
                    }
                    else
                        throw;
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
    
        [HttpGet]
        public async Task<IActionResult> Delete(int? id){
            if(id==null)
                return NotFound();

            var ogrenci = await _context.ogrenciler.FindAsync(id);
            if(ogrenci == null)
                return NotFound();
            
            return View(ogrenci);
            
        }
        
        [HttpPost]//asp.net model binding araştır
        public async Task<IActionResult> Delete([FromForm]int id){

            var ogrenci = await _context.ogrenciler.FindAsync(id);
            if(ogrenci == null)
                return NotFound();
            _context.Remove(ogrenci);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");  

        }
    }
}