using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Specifications
{
    public class ExpressionParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameterExpression;

        public ExpressionParameterReplacer(ParameterExpression parameterExpression)
        {
            _parameterExpression = parameterExpression;
        }

        protected override Expression VisitParameter(ParameterExpression node) 
            => base.VisitParameter(_parameterExpression);
    }
}
