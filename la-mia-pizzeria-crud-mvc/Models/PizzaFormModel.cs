using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_crud_mvc.Models
{
    public class PizzaFormModel
    {
        public Pizza Pizza { get; set; } 

        public List<Category>? Categories { get; set; }


        //INGREDIENTI
        public List<SelectListItem>? Ingredients { get; set; } // Gli elementi degli ingredienti selezionabili per la select (analogo a Categories)

        public List<string>? SelectedIngredients { get; set; } // Gli ID degli elementi (ingredienti) effettivamente selezionati

        public PizzaFormModel() { }

        public PizzaFormModel(Pizza p, List<Category> c)
        {
            this.Pizza = p;
            this.Categories = c;
        }

        public void CreateIngredients()
        {
            this.Ingredients = new List<SelectListItem>();

            this.SelectedIngredients = new List<string>();

            var IngredientsFromDB = PizzaManager.GetAllIngredient();

            foreach (var Ingrediente in IngredientsFromDB) // es. ingrediente1, ingrediente2, ingrediente3... ingrediente10
            {
                bool isSelected = this.Pizza.Ingredients?.Any(t => t.id == Ingrediente.id) == true; //se la tabella Pizza ha tra i suoi ingredienti almeno uno degli ingredienti
                
                if (isSelected)
                    this.SelectedIngredients.Add(Ingrediente.id.ToString()); // lista degli elementi selezionati


                this.Ingredients.Add(new SelectListItem() // lista degli elementi selezionabili
                {
                    Text = $"{Ingrediente.Nome} (Quantità: {Ingrediente.Quantita})",  // Nome visualizzato con quantità
                    Value = Ingrediente.id.ToString(), // SelectListItem vuole una generica stringa, non un int
                    Selected = isSelected // es. tag1, tag5, tag9
                });
 
            }
        }
    }
}
