using la_mia_pizzeria_crud_mvc.Context;
using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;




namespace la_mia_pizzeria_crud_mvc.Controllers
{
    public class PizzaController : Controller
    {
        private readonly PizzaContext _context = new PizzaContext();

        private readonly IWebHostEnvironment _webHostEnvironment;

        public PizzaController()
        {

            // Popolare il database con dati di esempio se non ci sono pizze
            PizzaManager.Seed();
        } 
            
        
        public IActionResult Index()
        {
            return View(PizzaManager.GetAllPizza());
        }

        // Action per visualizzare i dettagli di una pizza
        public IActionResult Details(int id)
        {
            //restituiscimi la prima pizza con id uguale a quello passato
            var pizza = PizzaManager.GetPizza(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }


        // Action che fornisce la view con la form
        // per creare un nuovo post del blog
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Pizza pizza, IFormFile foto)
        {

          
                string imgFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
                string imgFileName = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
                string imgPath = Path.Combine(imgFolderPath, imgFileName);

                using (var stream = new FileStream(imgPath, FileMode.Create))
                {
                    await foto.CopyToAsync(stream);
                }

                pizza.Foto = "~/img/" + imgFileName;

                PizzaManager.InsertPizza(pizza);

                if (!ModelState.IsValid)
                {

                    // Ritorniamo "data" alla view così che la form abbia di nuovo i dati inseriti
                    // (anche se erronei)
                    return View("Create", pizza);

                }

            


            PizzaManager.InsertPizza(pizza);

            return RedirectToAction("Index");
        }

         

        }
    }

