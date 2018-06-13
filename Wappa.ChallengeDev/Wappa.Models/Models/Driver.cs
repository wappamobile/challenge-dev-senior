﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Wappa.Contracts.Models
{
    public class Driver
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Car Car { get; set; }
        public Address Address { get; set; }

        public void GenerateUID()
        {
            Id = Guid.NewGuid().ToString();
        }
    }

    public class Address
    {
        public string StreetName { get; set; }
        public string Number { get; set; }
        public string AddressComplement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public Location Location { get; set; }
    }

    public class Car
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
    }

    
}
