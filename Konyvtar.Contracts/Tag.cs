// <copyright file="Tag.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Konyvtar.Contracts
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Represents a Tag entity.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Gets or sets the nev of the Tag.
        /// </summary>
        [Required]
        public string? Nev { get; set; }

        /// <summary>
        /// Gets or sets the lakcim of the Tag.
        /// </summary>
        [Required]
        public string? Lakcim { get; set; }

        /// <summary>
        /// Gets or sets the olvaso szam of the Tag.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OlvasoSzam { get; set; }

        /// <summary>
        /// Gets or sets the szuletesi datum of the Tag.
        /// </summary>
        [Required]
        public DateTime SzuletesiDatum { get; set; }
    }
}
