using FluentValidation;
using Biblioteca.Application.DTOs;

namespace Biblioteca.Application.Validators;

public class AutorDtoValidator : AbstractValidator<AutorDto>
{
    public AutorDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome do autor é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do autor deve ter no máximo 100 caracteres.");
    }
}
