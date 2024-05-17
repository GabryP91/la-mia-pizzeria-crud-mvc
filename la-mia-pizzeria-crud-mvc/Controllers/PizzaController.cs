using la_mia_pizzeria_crud_mvc.Context;
using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata.Ecma335;




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
          
            if (!ModelState.IsValid)
            {
                // ottengo la lista degli errori
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
           
                // Verifica se uno degli errori riguarda il campo "foto"
              
                if (errorMessages.Count != 1 || foto == null || foto.Length == 0)
                {
                    return View("Create", pizza);
                }

            }


            string imgFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
            string imgFileName = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
            string imgPath = Path.Combine(imgFolderPath, imgFileName);

            using (var stream = new FileStream(imgPath, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }

            pizza.Foto = "~/img/" + imgFileName;

            PizzaManager.InsertPizza(pizza);

            return RedirectToAction("Index"); 
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            // Prendo il post AGGIORNATO da database, non
            // uno passato da utente alla action
            var pizzaToEdit = PizzaManager.GetPizza(id);

            if (pizzaToEdit == null)
            {
                return NotFound();
            }
            else
            {
                return View(pizzaToEdit);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Pizza data, IFormFile foto)
        {
           
            if (!ModelState.IsValid)
            {
               
                // ottengo la lista degli errori
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                // Verifica se uno degli errori riguarda il campo "foto"

                if (errorMessages.Count != 1 || foto == null || foto.Length == 0)
                {
                    return View("Create", data);
                }
            }

            // Ottieni il nome del file immagine caricato dall'utente
            string imgFileName = Path.GetFileName(foto.FileName);

            string imgFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
            string imgPath = Path.Combine(imgFolderPath, imgFileName);

            data.Foto = "~/img/" + imgFileName;

            // MODIFICA TRAMITE LAMBDA
            bool result = PizzaManager.UpdatePizza(id,

                pizzaToEdit =>
                {
                    pizzaToEdit.Nome = data.Nome;
                    pizzaToEdit.Descrizione = data.Descrizione;
                    pizzaToEdit.Prezzo = data.Prezzo;
                    pizzaToEdit.Foto = data.Foto;

                });

            if (result == true)
                return RedirectToAction("Index");
            else
                return NotFound();

        }



    }
    }

