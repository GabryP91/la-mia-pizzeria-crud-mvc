namespace la_mia_pizzeria_crud_mvc.Models
{
    public class Ingredient
    {
        public int id { get; set; }

        public string Nome { get; set; }

        public float Quantita { get; set; }

        public List<Pizza>? Pizzas { get; set; }


        //METODI
        //public Ingredient() { }

        /*public Ingredient(string nome, float quantita) : this()
        {
            this.Nome = nome;

            this.Quantita = quantita;

        }*/
    }
}
