using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Shared.Results;

namespace Triple.Application.Commands.Favorite
{
    public class DeleteFavoriteCommand : IRequest<Result>
    {
        public Guid FavoriteId { get; set; }

        public DeleteFavoriteCommand(Guid favoriteId)
        {
            FavoriteId = favoriteId;
        }
    }
}
