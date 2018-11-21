﻿using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Wappa.Domain.Messages;

namespace Wappa.Application.CommandRequests
{
    public class RemoveVehicleRequest : IRequest<Response>
    {
        public RemoveVehicleRequest(int driverId, int id)
        {
            DriverId = driverId;
            Id = id;
        }

        [Required]
        public int DriverId { get; set; }

        [Required]
        public int Id { get; set; }
    }
}