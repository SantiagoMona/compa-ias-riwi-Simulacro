using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;
using simulacro.Data;
using simulacro.Models;


namespace simulacro.Controllers
{
    public class Sectors(DBContext dbcontext) : Controller
    {
        //////////////// LISTAR Y BUSCAR  //////////
        public readonly DBContext _dbcontext = dbcontext;
        public async Task<IActionResult> Index(string buscar){
            var sectors = from sector in _dbcontext.Sectors select sector;
            if(!string.IsNullOrEmpty(buscar)){
                sectors = sectors.Where(s => s.Name.Contains(buscar) || s.Author.Contains(buscar));
            }
            return View(await sectors.ToListAsync());
        }
        //////////////// CREAR  //////////
        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public IActionResult Create(Sector sector){
            _dbcontext.Sectors.Add(sector);
            _dbcontext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //////////////// ELIMINAR  //////////
        public async Task<IActionResult> Delete(int id){
            var sector = await _dbcontext.Sectors.FindAsync(id);
            _dbcontext.Sectors.Remove(sector);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
         //////////////// DETALLES  //////////
        public async Task<IActionResult> Details(int id){
            return View(await _dbcontext.Sectors.FindAsync(id));
        }
        //////////////// EDITAR  //////////
        public async Task<IActionResult> Edit(int id){
            return View(await _dbcontext.Sectors.FindAsync(id));
        }
        [HttpPost]
        public IActionResult Edit(Sector sector){
            _dbcontext.Sectors.Update(sector);
            _dbcontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
