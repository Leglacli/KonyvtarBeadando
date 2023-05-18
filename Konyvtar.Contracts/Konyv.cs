using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konyvtar.Contracts
{
    public class Konyv
    {
        [Required]
        public string Cim { get; set; }

        [Required]
        public string Szerzo { get; set; }

        [Required]
        public string Kiado { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeltariSzam { get; set; }

        [Required]
        public DateTime KiadasEve { get; set; }
    }
}