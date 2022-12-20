﻿using MapacenBackend.Entities;
using System.ComponentModel.DataAnnotations;

namespace MapacenBackend.Models.AddressDtos
{
    public class CreateAddressDto
    {
        //TODO poprawić regexa żeby polskie znaki przyjmował 
        [Required]
        [MaxLength(64)]
        [MinLength(1)]
        [RegularExpression(@"^[a-żA-Ż]+\-?[a-żA-Ż]+$")]  // Accepts upper and lower case letters and one hyphen
        public string City { get; set; }

        [Required]
        [MaxLength(64)]
        [MinLength(1)]
        [RegularExpression(@"^[a-żA-Ż]+(\s?[a-żA-Ż]+)*$")] //Accepts upper and lower case letters and spaces
        public string Street { get; set; }

        [Required]
        [MaxLength(64)]
        [MinLength(1)]
        [RegularExpression(@"\d{2}-\d{3}")]
        public string PostalCode { get; set; }

        [Required]
        [Range(1, 5000)]
        public int Number { get; set; }

        [Required]
        public int CountyId { get; set; }
    }
}
