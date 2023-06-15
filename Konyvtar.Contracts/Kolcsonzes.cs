// <copyright file="Kolcsonzes.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Konyvtar.Contracts
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Represents a Kolcsonzes (loan) entity.
    /// </summary>
    public class Kolcsonzes
    {
        /// <summary>
        /// Gets or sets the ID of the Kolcsonzes.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the olvaso szam of the Tag associated with the Kolcsonzes.
        /// </summary>
        [Required]
        public int OlvasoSzam { get; set; }

        /// <summary>
        /// Gets or sets the leltari szam of the Konyv associated with the Kolcsonzes.
        /// </summary>
        [Required]
        public int LeltariSzam { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the Kolcsonzes was made.
        /// </summary>
        [Required]
        public DateTime KolcsonzesIdeje { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the Konyv associated with the Kolcsonzes should be returned.
        /// </summary>
        [Required]
        public DateTime VisszahozasIdeje { get; set; }
    }
}
