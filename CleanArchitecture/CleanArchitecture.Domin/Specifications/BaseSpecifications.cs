using LinqKit;
using System.Linq.Expressions;

namespace CleanArchitecture.Domin.Specifications
{
    public abstract class BaseSpecifications<T> where T : class
    {
        public Expression<Func<T, bool>> Criteria { get; private set; }
        public List<string> Includes { get; private set; } = new List<string>();
        public List<Expression<Func<T, object>>> OrderBy { get; private set; } = new List<Expression<Func<T, object>>>();
        public List<Expression<Func<T, object>>> OrderByDescending { get; private set; } = new List<Expression<Func<T, object>>>();
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }
        public bool IsTotalCountEnable { get; private set; }
        public bool IsDistinct { get; private set; }

        protected void AddInclude(string incldueExpression)
        {
            Includes.Add(incldueExpression);
        }

        protected void AddCriteria(Expression<Func<T, bool>> criteriaExpression)
        {
            if (Criteria == null)
                Criteria = criteriaExpression!;
            else
                Criteria = Criteria.And(criteriaExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy.Add(orderByExpression);
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending.Add(orderByDescendingExpression);
        }

        protected void ApplyPaging(int PageSize, int PageIndex)
        {
            Skip = PageSize * (PageIndex - 1);
            Take = PageSize;
            IsPagingEnabled = true;
            EnableTotalCount();
        }

        protected void EnableTotalCount()
        {
            IsTotalCountEnable = true;
        }

        protected void EnableDistinct()
        {
            IsDistinct = true;
        }
    }
}
