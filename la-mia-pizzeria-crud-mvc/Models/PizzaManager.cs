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

        //funzione per ottenere tutti dati nella tabella pizza
        public static List<Pizza> GetAllPizza()
        {
            using (PizzaContext db = new PizzaContext())
            {
                return db.Pizza.ToList();
            }
                
        }


        //funzione per Richiamare la pizza tramite id
        public static Pizza GetPizza(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                //restituiscimi la prima pizza con id uguale a quello passato
                Pizza pizza = db.Pizza.FirstOrDefault(p => p.id == id);

                return pizza;
            }
               
        }

        //funzione inserimetno singola pizza
        public static void InsertPizza(Pizza pizza)
        {
            using (PizzaContext db = new PizzaContext())
            {
                db.Pizza.Add(pizza);
                db.SaveChanges();
            }
                
        }

        //funzione inserimetno singola pizza
        public static void Seed()
        {
            using (PizzaContext db = new PizzaContext())
            {
                if (PizzaManager.CountAllPizzas() == 0)
                {

                    PizzaManager.InsertPizza(new Pizza("Margherita", "Molto buona", "~/img/margherita.jpg", 8));
                    PizzaManager.InsertPizza(new Pizza("Diavola", "buona", "~/img/Diavola.jpg", 10.5f));
                    PizzaManager.InsertPizza(new Pizza("Ortolana", "ottima", "~/img/Ortolana.jpg", 8.7f));
                    PizzaManager.InsertPizza(new Pizza("Crudaiola", "discreta", "~/img/Crudaiola.jpg", 11));
                    PizzaManager.InsertPizza(new Pizza("Sfiziosa", "buona", "~/img/Sfiziosa.jpg", 9.4f));
                    PizzaManager.InsertPizza(new Pizza("Porcina", "pessima", "~/img/Porcina.jpg", 6));

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

        public static bool DeletePost(int id)
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

            db.SaveChanges();

        }


    }
}
