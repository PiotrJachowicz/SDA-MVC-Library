using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }

        [DisplayName("Tytuł")]
        [Required(ErrorMessage ="Należy podać tytuł")]
        [MaxLength(50, ErrorMessage = "Tytuł nie może mieć więcej niż 50 znaków")]
        public string Title { get; set; }

        [DisplayName("Autor")]
        [Required(ErrorMessage = "Należy podać autora")]
        [MaxLength(50, ErrorMessage = "Autor nie może mieć więcej niż 50 znaków")]
        public string Author { get; set; }

        [DisplayName("Rok wydania")]
        [Required(ErrorMessage = "Należy podać rok wydania")]
        public int? ProductionYear { get; set; }

        [DisplayName("Gatunek")]
        public Genre Genre { get; set; }

        [Required(ErrorMessage = "Należy podać gatunek")]
        public int GenreId { get; set; }
    }
}
