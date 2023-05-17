using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konyvtar.Contracts
{
    public class Konyv
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Cim { get; set; }

        [Required]
        public string Szerzo { get; set; }

        [Required]
        public string Kiado { get; set; }

        [Required]
        public int LeltariSzam { get; set; }

        [Required]
        public DateTime KiadasEve { get; set; }
    }
}