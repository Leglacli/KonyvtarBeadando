using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konyvtar.Contracts
{
    public class Tagok
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nev { get; set; }

        [Required]
        public string Lakcim { get; set; }

        [Required]
        public int OlvasoSzam { get; set; }

        [Required]
        public DateTime SzuletesiDatum { get; set; }
    }
}
