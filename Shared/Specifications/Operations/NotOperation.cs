using System.Linq.Expressions;

namespace Shared.Specifications.Operations
{
    public class NotOperation<T> : Specification<T>
    {
        private readonly Specification<T> _specification;

        public NotOperation(Specification<T> specification)
        {
            _specification = specification;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> expression = _specification.ToExpression();
            var expressionParameter = expression.Parameters[0];
            var expressionBody = Expression.Not(expression.Body);
            var expressionFinal = Expression.Lambda<Func<T, bool>>(expressionBody, expressionParameter);
            return expressionFinal;
        }
    }
}
