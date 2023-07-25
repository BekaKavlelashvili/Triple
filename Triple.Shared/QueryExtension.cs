using Triple.Shared;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Triple.Shared
{
    public static class QueryExtension
    {
        public static IQueryable<T> ToPaging<T>(this IQueryable<T> source, int page, int pageSize)
        {
            return source.Skip(page * pageSize).Take(pageSize);
        }

        public static IQueryable<T> FilterAndSort<T>(this IQueryable<T> source, List<Filtering> filters, List<Sorting> sortings)
        {
            filters.ForEach(f =>
            {
                string filter = $"x => x.{f.FilterName}.ToLower().Contains({f.FilterValue.ToLower()})";
                var exp = DynamicExpressionParser.ParseLambda<T, bool>(ParsingConfig.Default, false, filter, null);
                var func = exp.Compile();

                source = source.Where(func).AsQueryable();
            });

            if (sortings.Any())
            {
                string sort = $"x => x.{sortings[0].SortName}";
                var exp = DynamicExpressionParser.ParseLambda<T, bool>(ParsingConfig.Default, false, sort, null);
                var func = exp.Compile();

                var res = source.OrderBy(func);
                switch (sortings[0].SortOrder)
                {
                    case SortOrder.ASC:
                        res = res.OrderBy(func);
                        break;
                    case SortOrder.DESC:
                        res = res.OrderByDescending(func);
                        break;
                    default:
                        break;
                }

                for (int i = 1; i < sortings.Count; i++)
                {
                    sort = $"x => x.{sortings[i].SortName}";
                    exp = DynamicExpressionParser.ParseLambda<T, bool>(ParsingConfig.Default, false, sort, null);
                    func = exp.Compile();

                    switch (sortings[i].SortOrder)
                    {
                        case SortOrder.ASC:
                            res = res.ThenBy(func);
                            break;
                        case SortOrder.DESC:
                            res = res.ThenByDescending(func);
                            break;
                        default:
                            break;
                    }
                }

                source = res.AsQueryable();
            }

            return source;
        }
    }
}
