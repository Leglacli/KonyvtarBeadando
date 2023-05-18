using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konyvtar.Contracts
{
    public class Tag
    {
        [Required]
        public string Nev { get; set; }

        [Required]
        public string Lakcim { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OlvasoSzam { get; set; }

        [Required]
        public DateTime SzuletesiDatum { get; set; }
    }
}
