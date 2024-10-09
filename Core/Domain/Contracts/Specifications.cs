using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public abstract class Specifications<T> where T : class
    {
        public Expression<Func<T, bool>> Criteria { get; }

        protected Specifications(Expression<Func<T, bool>> criteria)
        {
            this.Criteria = criteria;
        }



        public List<Expression<Func<T, object>>> IncludeExpressions { get; } = new();

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> expressions) => IncludeExpressions.Add(expressions);

        protected void SetOrderBy(Expression<Func<T, object>> expressions) =>this.OrderBy = expressions ;

        protected void SetOrderByDecending(Expression<Func<T, object>> expressions) =>this.OrderByDescending = expressions ;

        protected void ApplyPagination(int PageIndex,int PageSize)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageIndex-1)*PageSize;
        }
    

   
    }
}
//context.set<T>().where(Func<T, bool).Include()
