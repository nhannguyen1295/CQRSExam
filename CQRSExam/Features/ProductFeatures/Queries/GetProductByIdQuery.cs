using CQRSExam.Context;
using CQRSExam.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSExam.Features.ProductFeatures.Queries;

public class GetProductByIdQuery:IRequest<Product>
{
    public int Id { get; set; }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IApplicationContext _context;
        public GetProductByIdQueryHandler(IApplicationContext context) => _context = context;
        
        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            return product ?? new Product{Id = 0};
        }
    }
}