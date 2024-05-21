using la_mia_pizzeria_crud_mvc.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_crud_mvc.Models
{
    public class PizzaManager
    {

       
        //funzione per verificare che la tabella pizza sia popolata
        public static int CountAllPizzas()
        {
            using (PizzaContext db = new PizzaContext())
            {
                return db.Pizza.Count();
            }
               
        }

        //funzione per verificare che la tabella ingrediente sia popolata
        public static int CountAllIngredients()
        {
            using (PizzaContext db = new PizzaContext())
            {
                return db.Ingrediente.Count();
            }

        }

        //funzione per ottenere tutti dati nella tabella pizza
        public static List<Pizza> GetAllPizza(bool includeCategories = false)
        {
            using (PizzaContext db = new PizzaContext())
            {
                if (includeCategories) return db.Pizza.Include(c => c.Category).Include(i => i.Ingredients).ToList();

                else return db.Pizza.ToList();
            }
                
        }

        //funzione per ottenere tutti dati nella tabella categoria
        public static List<Category> GetAllCategory()
        {
            using (PizzaContext db = new PizzaContext())
            {
                return db.Category.ToList();
            }

        }

        //funzione per ottenere tutti dati nella tabella categoria
        public static List<Ingredient> GetAllIngredient()
        {
            using (PizzaContext db = new PizzaContext())
            {
                return db.Ingrediente.ToList();
            }

        }


        //funzione per Richiamare la pizza tramite id
        public static Pizza GetPizza(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                //restituiscimi la prima pizza con id uguale a quello passato con tutte la categoria
                Pizza pizza = db.Pizza
                    .Where(p => p.id == id)
                    .Include(c => c.Category)
                    .Include(i => i.Ingredients)
                    .FirstOrDefault();

                return pizza;
            }
               
        }

        //funzione inserimetno singolo ingrediente
        public static void InsertIngredient(Ingredient ingrediente)
        {
            using (PizzaContext db = new PizzaContext())
            {
            
                db.Ingrediente.Add(ingrediente);
                db.SaveChanges();
            }

        }

        //funzione inserimetno singola pizza
        public static void InsertPizza(Pizza pizza, List<string> SelectedIngredients = null)
        {
            using (PizzaContext db = new PizzaContext())
            {
                if (SelectedIngredients != null)
                {
                    pizza.Ingredients = new List<Ingredient>();

                    // Trasformiamo gli ID scelti in Ingredient da aggiungere tra i riferimenti in Pizza
                    foreach (var IngredientsId in SelectedIngredients)
                    {
                        int id = int.Parse(IngredientsId);
                        var ingredient = db.Ingrediente.FirstOrDefault(t => t.id == id); // PostManager.GetTagById(id); NON usiamo GetTagById() perché usa un db context diverso e ciò causerebbe errore in fase di salvataggio - usiamo lo stesso context all'interno della stessa operazione
                        pizza.Ingredients.Add(ingredient);
                    }
                }

                db.Pizza.Add(pizza);
                db.SaveChanges();
            }
                
        }

        //funzione inserimetno singola pizza
        public static void Seed()
        {
            using (PizzaContext db = new PizzaContext())
            {
                var ingredientsMargherita = new List<Ingredient>
                {
                    new Ingredient {Nome = "Mozzarella", Quantita = 15 },
                    new Ingredient {  Nome = "Pomodoro", Quantita = 50 },
                    new Ingredient { Nome = "Basilico", Quantita = 5 }
                };

                var ingredientsDiavola = new List<Ingredient>
                {
                    new Ingredient {Nome = "Mozzarella", Quantita = 15 },
                    new Ingredient { Nome = "Salame Piccante", Quantita = 15 },
                    new Ingredient { Nome = "Pomodoro", Quantita = 50 }
                };

                var ingredientsCrudaiola = new List<Ingredient>
                {
                    new Ingredient { Nome = "Mozzarella", Quantita = 15 },
                    new Ingredient { Nome = "Rucola",  Quantita = 25 },
                    new Ingredient { Nome = "Pomodoro", Quantita = 50 },
                    new Ingredient { Nome = "Parmigiano",  Quantita = 15 }
                };

                var ingredientsOrtolana = new List<Ingredient>
                {
                    new Ingredient { Nome = "Mozzarella", Quantita = 15 },
                    new Ingredient { Nome = "Rucola",  Quantita = 25 },
                    new Ingredient { Nome = "Zucchine", Quantita = 20 },
                    new Ingredient { Nome = "Melanzana",  Quantita = 1}
                };

                var ingredientsSfiziosa = new List<Ingredient>
                {
                    new Ingredient { Nome = "Mozzarella", Quantita = 15 },
                    new Ingredient { Nome = "Rucola",  Quantita = 25 },
                    new Ingredient { Nome = "Pomodoro", Quantita = 50 },
                    new Ingredient { Nome = "Bresaola",  Quantita = 10 }
                };

                var ingredientsPorcina = new List<Ingredient>
                {
                    new Ingredient { id = 1, Nome = "Mozzarella", Quantita = 15 },
                    new Ingredient { id = 5, Nome = "Rucola",  Quantita = 25 },
                    new Ingredient { id = 11, Nome = "Basilico", Quantita = 5 },
                    new Ingredient { id = 10, Nome = "Funghi Porcini",  Quantita = 20 }
                };


                if (PizzaManager.CountAllIngredients() == 0 && PizzaManager.CountAllPizzas() == 0)
                {

                    PizzaManager.InsertPizza(new Pizza("Margherita", "Molto buona", "~/img/margherita.jpg", 8, 1, ingredientsMargherita));
                    PizzaManager.InsertPizza(new Pizza("Diavola", "buona", "~/img/Diavola.jpg", 10.5f, 1, ingredientsDiavola));
                    PizzaManager.InsertPizza(new Pizza("Ortolana", "ottima", "~/img/Ortolana.jpg", 8.7f, 3, ingredientsOrtolana));
                    PizzaManager.InsertPizza(new Pizza("Crudaiola", "discreta", "~/img/Crudaiola.jpg", 11, 1, ingredientsCrudaiola));
                    PizzaManager.InsertPizza(new Pizza("Sfiziosa", "buona", "~/img/Sfiziosa.jpg", 9.4f, 3, ingredientsSfiziosa));
                    PizzaManager.InsertPizza(new Pizza("Porcina", "pessima", "~/img/Porcina.jpg", 6, 2, ingredientsPorcina));


                    db.SaveChanges();

                }

            } 
        }

        public static bool UpdatePizza(int id, Action<Pizza> edit)
        {

            using PizzaContext db = new PizzaContext();

            //ricerca e restituisce la prima posizione con lo stesso id passato
            var post = db.Pizza.FirstOrDefault(p => p.id == id);

            if (post == null)
                return false;

            //restituisce il dato alla funzione lambda per farlo modificare
            edit(post);

            db.SaveChanges();

            return true;
        }

        public static bool DeletePizza(int id)
        {
            using PizzaContext db = new PizzaContext();


            var pizza = db.Pizza.FirstOrDefault(p => p.id == id);

            if (pizza == null)
                return false;

            db.Pizza.Remove(pizza);

            db.SaveChanges();

            return true;
        }

        public static void DeleteAllPizza()
        {
            using PizzaContext db = new PizzaContext();

            
            foreach (Pizza pizza in db.Pizza.ToList())
            {
                db.Pizza.Remove(pizza);
            }

            foreach (Ingredient ingrediente in db.Ingrediente.ToList())
            {
                db.Ingrediente.Remove(ingrediente);
            }

            db.SaveChanges();

        }


    }
}
