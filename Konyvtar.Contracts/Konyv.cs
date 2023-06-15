// <copyright file="Konyv.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Konyvtar.Contracts
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Represents a Konyv entity.
    /// </summary>
    public class Konyv
    {
        /// <summary>
        /// Gets or sets the cim of the Konyv.
        /// </summary>
        [Required]
        public string Cim { get; set; }

        /// <summary>
        /// Gets or sets the szerzo of the Konyv.
        /// </summary>
        [Required]
        public string Szerzo { get; set; }

        /// <summary>
        /// Gets or sets the kiado of the Konyv.
        /// </summary>
        [Required]
        public string Kiado { get; set; }

        /// <summary>
        /// Gets or sets the leltari szam of the Konyv.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeltariSzam { get; set; }

        /// <summary>
        /// Gets or sets the kiadas eve of the Konyv.
        /// </summary>
        [Required]
        public DateTime KiadasEve { get; set; }
    }
}