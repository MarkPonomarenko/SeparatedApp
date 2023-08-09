using Shared.Specifications.Operations;
using System.Linq.Expressions;

namespace Shared.Specifications
{
    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy(T item)
        {
            var predicate = ToExpression().Compile();
            return predicate(item);
        }

        public Specification<T> And (Specification<T> other)
        {
            return new AndOperation<T>(this, other);
        }
    }
}
