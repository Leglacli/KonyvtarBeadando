﻿namespace Konyvtar.Contracts
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Kolcsonzes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int OlvasoSzam { get; set; }

        [Required]
        public int LeltariSzam { get; set; }

        [Required]
        public DateTime KolcsonzesIdeje { get; set; }

        [Required]
        public DateTime VisszahozasIdeje { get; set; }
    }
}
