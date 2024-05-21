﻿using la_mia_pizzeria_crud_mvc.Context;
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

           
        } 
            
        
        public IActionResult Index()
        {
            return View(PizzaManager.GetAllPizza(true));
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

        [HttpGet]
        public IActionResult Popolate()
        {
            // Popolare il database con dati di esempio se non ci sono pizze
            PizzaManager.Seed();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Delete()
        {
            PizzaManager.DeleteAllPizza();

            return RedirectToAction("Index");

        }


        // Action che fornisce la view con la form
        // per creare un nuovo post del blog
        [HttpGet]
        public IActionResult Create()
        {

            PizzaFormModel model = new PizzaFormModel();

            model.Pizza = new Pizza();

            model.Categories = PizzaManager.GetAllCategory();

            return View(model);
        }

       


        [HttpPost]
        public async Task<IActionResult> Create(PizzaFormModel data, IFormFile foto)
        {
          
            if (!ModelState.IsValid)
            {

                data.Categories = PizzaManager.GetAllCategory();

                // Ottenere la lista degli errori di validazione
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                // Verifica se ci sono errori o se la foto non è presente
                if (errorMessages.Count > 1 || foto == null || foto.Length == 0)
                {
                    return View("Create", data);
                }

            } 


            string imgFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
            string imgFileName = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
            string imgPath = Path.Combine(imgFolderPath, imgFileName);

            using (var stream = new FileStream(imgPath, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }
            //pizza.Pizza.Foto;

            data.Pizza.Foto = "~/img/" + imgFileName;

            PizzaManager.InsertPizza(data.Pizza);

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
                PizzaFormModel model = new PizzaFormModel(pizzaToEdit, PizzaManager.GetAllCategory());
                //model.CreateTags();
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaFormModel data, IFormFile foto)
        {
            data.Categories = PizzaManager.GetAllCategory();

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

            data.Pizza.Foto = "~/img/" + imgFileName;

            // MODIFICA TRAMITE LAMBDA
            bool result = PizzaManager.UpdatePizza(id,

                pizzaToEdit =>
                {
                    pizzaToEdit.Nome = data.Pizza.Nome;
                    pizzaToEdit.Descrizione = data.Pizza.Descrizione;
                    pizzaToEdit.Prezzo = data.Pizza.Prezzo;
                    pizzaToEdit.Foto = data.Pizza.Foto;
                    pizzaToEdit.Categoryid = data.Pizza.Categoryid;

                });

            if (result == true)
                return RedirectToAction("Index");
            else
                return NotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
           
            if (PizzaManager.DeletePizza(id))
                return RedirectToAction("Index");
            else
                return NotFound();
        }



    }
    }

