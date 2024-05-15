using la_mia_pizzeria_crud_mvc.Context;
using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_crud_mvc.Controllers
{
    public class PizzaController : Controller
    {
        private readonly PizzaContext _context = new PizzaContext();

        public PizzaController()
        {
            
            // Popolare il database con dati di esempio se non ci sono pizze
            if (_context.Pizza.Count() == 0)
            {
                Pizza prova = new Pizza("Margherita", "Molto buona", "~/img/margherita.jpg", 8);
                Pizza prova1 = new Pizza("Diavola", "buona", "~/img/Diavola.jpg", 10.5f);
                Pizza prova2 = new Pizza("Ortolana", "ottima", "~/img/Ortolana.jpg", 8.7f);
                Pizza prova3 = new Pizza("Crudaiola", "discreta", "~/img/Crudaiola.jpg", 11);
                Pizza prova4 = new Pizza("Sfiziosa", "buona", "~/img/Sfiziosa.jpg", 9.4f);
                Pizza prova5 = new Pizza("Porcina", "pessima", "~/img/Porcina.jpg", 6);

                _context.Pizza.Add(prova);
                _context.Pizza.Add(prova1);
                _context.Pizza.Add(prova2);
                _context.Pizza.Add(prova3);
                _context.Pizza.Add(prova4);
                _context.Pizza.Add(prova5);


                _context.SaveChanges();
            }
        }
        public IActionResult Index()
        {
            var pizze = _context.Pizza.ToList();

            return View(pizze);
        }

        // Action per visualizzare i dettagli di una pizza
        public IActionResult Details(int id)
        {
            //restituiscimi la prima pizza con id uguale a quello passato
            var pizza = _context.Pizza.FirstOrDefault(p => p.id == id);

            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }
    }
}
