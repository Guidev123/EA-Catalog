﻿using FluentValidation;
using FluentValidation.Results;

namespace CatalogService.Application.UseCases
{
    public abstract class Handler
    {
        public string[] GetAllErrors(ValidationResult validationResult) =>
            validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToArray();

        public ValidationResult ValidateEntity<TValidator, TEntity>(TValidator validation, TEntity entity)
            where TValidator : AbstractValidator<TEntity>
            where TEntity : class => validation.Validate(entity);
    }
}
