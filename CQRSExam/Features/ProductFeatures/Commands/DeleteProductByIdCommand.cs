using CQRSExam.Context;
using CQRSExam.Models;
using CQRSExam.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSExam.Features.ProductFeatures.Commands;

public class DeleteProductByIdCommand : IRequest<int>
{
    public int Id { get; set; }
    
    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, int>
    {
        private readonly IUnitOfWork<Product> _unitOfWork;
        public DeleteProductByIdCommandHandler(IUnitOfWork<Product> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Repository.GetByIdAsync(command.Id);
            if (product == null) return default;
            _unitOfWork.Repository.Delete(product);
            await _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
            return product.Id;
        }
    }
}