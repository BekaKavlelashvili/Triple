﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Dtos.Coordinate
{
    public class CoordinateDto
    {
        public Guid OrganisationId { get; set; }

        public string Address { get; set; }

        public string OrganisationName { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
