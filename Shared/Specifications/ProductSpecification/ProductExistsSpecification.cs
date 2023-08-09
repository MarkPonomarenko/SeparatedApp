
using Shared.Data.Entities;
using System.Linq.Expressions;

namespace Shared.Specifications.CourseSpecification
{
    public class ProductExistsSpecification : Specification<Product>
    {
        private readonly Product _product;

        public ProductExistsSpecification(Product product)
        {
            _product = product;
        }
        public override Expression<Func<Product, bool>> ToExpression()
        {
            return product => product.Name == _product.Name && product.Price == _product.Price;
        }
    }
}
