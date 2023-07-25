﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Pack;
using Triple.Application.Shared;
using Triple.Shared;

namespace Triple.Application.Queries.Favorite
{
    public class SearchFavoriteQuery : SortAndPagingQuery, IRequest<QueryResultOfList<PackDto>>
    {
    }
}
