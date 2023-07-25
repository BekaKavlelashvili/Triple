using GoogleMaps.LocationServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Triple.Application.Commands.Coordinate;
using Triple.Infrastructure.Persistence;
using Triple.Shared;
using Triple.Shared.Results;

namespace Triple.Application.Executors.Coordinate.Command
{
    public class CreateCoordinateCommandExecutor : Executor, IRequestHandler<CreateCoordinateCommand, Result>
    {
        private TripleDbContext _db;

        public CreateCoordinateCommandExecutor(TripleDbContext db)
        {
            _db = db;
        }

        public async Task<Result> Handle(CreateCoordinateCommand request, CancellationToken cancellationToken)
        {
            var organisations = await _db.Organisations.ToListAsync();

            organisations.ForEach(org =>
            {
                //var address = org.OrganisationAddress.Split(',');
                //var street = address[0];
                //var city = address[1];

                var address = "75 Ninth Avenue 2nd and 4th Floors New York, NY 10011";
                var locationService = new GoogleLocationService("AIzaSyA2NHvGvxyXhOF9IYZzxmDjPrVSMwsGAF8");
                var point = locationService.GetLatLongFromAddress(address);
                var latitude = point.Latitude;
                var longitude = point.Longitude;

                var coordinate = new Domain.Aggregates.Coordinate.Coordinate(Guid.NewGuid());

                coordinate.Create(org.EntityId, latitude, longitude);

                _db.Attach(coordinate);
            });

            await _db.SaveChangesAsync();

            return Succeed();
        }
    }
}
