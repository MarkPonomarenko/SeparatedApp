using System.Linq.Expressions;

namespace Shared.Specifications.Operations
{
    public class AndOperation<T> : Specification<T>
    {
        private readonly Specification<T> _leftExpression;
        private readonly Specification<T> _rightExpression;

        public AndOperation(Specification<T> leftExpression, Specification<T> rightExpression)
        {
            _leftExpression = leftExpression;
            _rightExpression = rightExpression;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _leftExpression.ToExpression();
            Expression<Func<T, bool>> rightExpression = _rightExpression.ToExpression();
            var expressionParameter = Expression.Parameter(typeof(T));
            var expressionBody = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
            expressionBody = (BinaryExpression)new ExpressionParameterReplacer(expressionParameter).Visit(expressionBody);
            var expressionFinal = Expression.Lambda<Func<T, bool>>(expressionBody, expressionParameter);
            return expressionFinal;
        }
    }
}
