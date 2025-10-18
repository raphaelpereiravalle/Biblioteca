using FluentValidation;
using Biblioteca.Application.DTOs;

namespace Biblioteca.Application.Validators;

public class AssuntoDtoValidator : AbstractValidator<AssuntoDto>
{
    public AssuntoDtoValidator()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("A descrição é obrigatória.")
            .MaximumLength(150).WithMessage("A descrição deve ter no máximo 150 caracteres.");
    }
}
