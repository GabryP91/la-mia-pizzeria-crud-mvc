namespace la_mia_pizzeria_crud_mvc.Models
{
    public class Category
    {
        public int id { get; set; }
        public string Nome { get; set; }

        public List<Pizza> Pizzas { get; set; }
    }
}
