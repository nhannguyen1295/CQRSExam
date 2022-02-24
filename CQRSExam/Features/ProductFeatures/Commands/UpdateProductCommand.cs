using CQRSExam.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSExam.Features.ProductFeatures.Commands;

public class UpdateProductCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Barcode { get; set; }
    public string Description { get; set; }
    public decimal BuyingPrice { get; set; }
    public decimal Rate { get; set; }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
    {
        private readonly IApplicationContext _context;
        public UpdateProductCommandHandler(IApplicationContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(a => a.Id == command.Id, cancellationToken: cancellationToken);
            if (product == null)
            {
                return default;
            }

            product.Barcode = command.Barcode;
            product.Name = command.Name;
            product.BuyingPrice = command.BuyingPrice;
            product.Rate = command.Rate;
            product.Description = command.Description;
            await _context.SaveChanges();
            return product.Id;
        }
    }
}