using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;
using simulacro.Data;
using simulacro.Models;

namespace simulacro.Controllers
{
    public class CompaniesController : Controller
    {
        public readonly DBContext _context;
        public CompaniesController(DBContext context)
        {
            _context = context;
        }
        //////////////// BUSCAR Y LISTAR  //////////
        public async Task<IActionResult> Index(string buscar)
        {
            var companies = from company in _context.Companies select company;
            if (!string.IsNullOrEmpty(buscar))
            {
                companies = companies.Where(c => c.Name.Contains(buscar) || c.Phone.Contains(buscar) || c.LegalRepresentative.Contains(buscar) || c.Nit.Contains(buscar) || c.Address.Contains(buscar));
            }
            return View(await companies.ToListAsync());
        }

        //////////////// CREATE //////////
        public IActionResult Create(){
            return View();
        }
        [HttpPost]

        public IActionResult Create(Company company){
            _context.Companies.Add(company);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //////////////// ELIMINAR  //////////
        public async Task<IActionResult> Delete(int id){
            var company = await _context.Companies.FindAsync(id);
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        ////////////////  DETAILS  //////////
        public async Task<IActionResult> Details(int id){
            return View(await _context.Companies.FindAsync(id));
        }

        //////////////// EDITAR  //////////
        
        public async Task<IActionResult> Edit(int id){
            return View(await _context.Companies.FindAsync(id));
        }
        [HttpPost]
        public IActionResult Edit(Company company){
            _context.Companies.Update(company);
            _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}