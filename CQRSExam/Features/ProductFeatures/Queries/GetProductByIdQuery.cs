using CQRSExam.Context;
using CQRSExam.Models;
using CQRSExam.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSExam.Features.ProductFeatures.Queries;

public class GetProductByIdQuery:IRequest<Product>
{
    public int Id { get; init; }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IUnitOfWork<Product> _unitOfWork;
        public GetProductByIdQueryHandler(IUnitOfWork<Product> unitOfWork) => _unitOfWork = unitOfWork;
        
        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Repository.GetByIdAsync(request.Id);
            return product ?? new Product {Id = 0};
        }
    }
}