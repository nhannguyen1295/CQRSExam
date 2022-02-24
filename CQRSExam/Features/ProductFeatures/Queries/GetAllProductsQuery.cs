using CQRSExam.Context;
using CQRSExam.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSExam.Features.ProductFeatures.Queries;

public class GetAllProductsQuery:IRequest<IEnumerable<Product>>
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IApplicationContext _context;
        public GetAllProductQueryHandler(IApplicationContext context) => _context = context;
        
        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productList = await _context.Products.ToListAsync(cancellationToken: cancellationToken);
            return productList.AsReadOnly();
        }
    }
}