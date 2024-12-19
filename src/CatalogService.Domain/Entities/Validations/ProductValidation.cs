using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Entities.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The Name field cannot be empty")
                .Length(3, 50).WithMessage("The Name field must be between 3 and 50 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("The Description field cannot be empty.")
                .MinimumLength(10).WithMessage("The Description field must be at least 10 characters long")
                .MaximumLength(500).WithMessage("The Description field cannot exceed 500 characters");
        }
    }
}
