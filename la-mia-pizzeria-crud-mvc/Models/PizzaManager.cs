using la_mia_pizzeria_crud_mvc.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_crud_mvc.Models
{
    public class PizzaManager
    {

        private static PizzaContext db = new PizzaContext();

        //funzione per verificare che la tabella pizza sia popolata
        public static int CountAllPizzas()
        {
            return db.Pizza.Count();
        }

        //funzione per ottenere tutti dati nella tabella pizza
        public static List<Pizza> GetAllPizza()
        {
            return db.Pizza.ToList();
        }


        //funzione per Richiamare la pizza tramite id
        public static Pizza GetPizza(int id)
        {
            //restituiscimi la prima pizza con id uguale a quello passato
            Pizza pizza = db.Pizza.FirstOrDefault(p => p.id == id);

            return pizza;

        }

        //funzione inserimetno singola pizza
        public static void InsertPizza(Pizza pizza)
        { 
            db.Pizza.Add(pizza);
            db.SaveChanges();
        }

        //funzione inserimetno singola pizza
        public static void Seed()
        {
            if (PizzaManager.CountAllPizzas() == 0)
            {
                PizzaManager.InsertPizza(new Pizza("Margherita", "Molto buona", "~/img/margherita.jpg", 8));
                PizzaManager.InsertPizza(new Pizza("Diavola", "buona", "~/img/Diavola.jpg", 10.5f));
                PizzaManager.InsertPizza(new Pizza("Ortolana", "ottima", "~/img/Ortolana.jpg", 8.7f));
                PizzaManager.InsertPizza(new Pizza("Crudaiola", "discreta", "~/img/Crudaiola.jpg", 11));
                PizzaManager.InsertPizza(new Pizza("Sfiziosa", "buona", "~/img/Sfiziosa.jpg", 9.4f));
                PizzaManager.InsertPizza(new Pizza("Porcina", "pessima", "~/img/Porcina.jpg", 6));

            }
        }

    }
}
