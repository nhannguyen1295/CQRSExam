using CQRSExam.Context;
using CQRSExam.Models;
using CQRSExam.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSExam.Features.ProductFeatures.Commands;

public class UpdateProductCommand : IRequest<int>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Barcode { get; set; }
    public string? Description { get; set; }
    public decimal BuyingPrice { get; set; }
    public decimal Rate { get; set; }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
    {
        private readonly IUnitOfWork<Product> _unitOfWork;
        public UpdateProductCommandHandler(IUnitOfWork<Product> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Repository.GetByIdAsync(command.Id);
            if (product == null)
            {
                return default;
            }

            product.Barcode = command.Barcode;
            product.Name = command.Name;
            product.BuyingPrice = command.BuyingPrice;
            product.Rate = command.Rate;
            product.Description = command.Description;
            await _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
            return product.Id;
        }
    }
}