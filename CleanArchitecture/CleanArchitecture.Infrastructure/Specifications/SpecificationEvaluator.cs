using CleanArchitecture.Domin.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Specifications
{
    public class SpecificationEvaluator<T> where T : class
    {
        public static (IQueryable<T> data, int count) GetQuery(IQueryable<T> inputQuery,
            BaseSpecifications<T> specifications)
        {
            var query = inputQuery;

            int Count = 0;

            if (specifications.Criteria != null)
                query = query.Where(specifications.Criteria);

            if (specifications.OrderByDescending.Any())
            {
                var orderedQuery = query.OrderByDescending(specifications.OrderByDescending.First());

                foreach (var orderBy in specifications.OrderByDescending.Skip(1))
                {
                    orderedQuery = orderedQuery.ThenByDescending(orderBy);
                }

                query = orderedQuery;
            }

            if (specifications.OrderBy.Any())
            {
                var orderedQuery = query.OrderBy(specifications.OrderBy.First());

                foreach (var orderBy in specifications.OrderBy.Skip(1))
                {
                    orderedQuery = orderedQuery.ThenBy(orderBy);
                }

                query = orderedQuery;
            }

            if (specifications.IsDistinct)
                query = query.Distinct();

            if (specifications.IsTotalCountEnable)
                Count = query.Count();

            if (specifications.IsPagingEnabled)
                query = query.Skip(specifications.Skip).Take(specifications.Take);

            if (specifications.Includes.Any())
                specifications.Includes.ForEach(x => query = query.Include(x));

            return (query, Count);
        }
    }
}
