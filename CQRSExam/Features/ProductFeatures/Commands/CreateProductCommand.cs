using CQRSExam.Context;
using CQRSExam.Models;
using CQRSExam.UnitOfWork;
using MediatR;

namespace CQRSExam.Features.ProductFeatures.Commands;

public class CreateProductCommand : IRequest<int>
{
    public string? Name { get; set; }
    public string? Barcode { get; set; }
    public string? Description { get; set; }
    public decimal BuyingPrice { get; set; }
    public decimal Rate { get; set; }
    
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IUnitOfWork<Product> _unitOfWork;
        public CreateProductCommandHandler(IUnitOfWork<Product> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Barcode = command.Barcode,
                Name = command.Name,
                BuyingPrice = command.BuyingPrice,
                Rate = command.Rate,
                Description = command.Description
            };
            _unitOfWork.Repository.Insert(product);
            await _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
            return product.Id;
        }
    }
}