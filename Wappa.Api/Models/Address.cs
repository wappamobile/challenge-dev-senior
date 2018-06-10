﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wappa.Api.Models
{
    public class Address
    {
		public int Id { get; set; }

		[Required]
		public String AddressLine { get; set; }

		[Required]
		public String PostalCode { get; set; }

		[Required]
		public String City { get; set; }

		[Required]
		public String State { get; set; }

		public override string ToString()
		{
			return $"{AddressLine} {City} {State} {PostalCode}";
		}
	}
}
