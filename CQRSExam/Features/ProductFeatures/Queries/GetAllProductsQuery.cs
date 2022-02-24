using CQRSExam.Context;
using CQRSExam.Models;
using CQRSExam.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSExam.Features.ProductFeatures.Queries;

public class GetAllProductsQuery:IRequest<IEnumerable<Product>>
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IUnitOfWork<Product> _unitOfWork;
        public GetAllProductQueryHandler(IUnitOfWork<Product> unitOfWork) => _unitOfWork = unitOfWork;
        
        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productList = await _unitOfWork.Repository.GetAsync();
            return productList.ToList().AsReadOnly();
        }
    }
}