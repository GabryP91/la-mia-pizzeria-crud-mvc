using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_crud_mvc.Models
{
    [Table("Pizza")]
    [Index(nameof(Nome), IsUnique = true)]
    public class Pizza
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public string Foto { get; set; }
        public decimal Prezzo { get; set; }

        public Pizza() { }

        public Pizza(string nome, string descrizione, string foto, decimal prezzo)
        {
            this.Nome = nome;

            this.Descrizione = descrizione;

            this.Foto = foto;

            this.Prezzo = prezzo;

        }

        //override del metodo ToString
        public override string ToString()
        {
            return $"\nNome: {Nome} - Descrizione: {Descrizione} - Foto: {Foto} - Prezzo: {Prezzo}";
        }
    }
}
