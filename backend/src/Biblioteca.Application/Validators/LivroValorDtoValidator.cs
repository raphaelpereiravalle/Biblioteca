using FluentValidation;
using Biblioteca.Application.DTOs;

namespace Biblioteca.Application.Validators;

public class LivroValorDtoValidator : AbstractValidator<LivroValorDto>
{
    public LivroValorDtoValidator()
    {
        RuleFor(x => x.IdLivro).GreaterThan(0).WithMessage("O Id do livro deve ser informado.");
        RuleFor(x => x.TipoVenda).NotEmpty().WithMessage("O tipo de venda é obrigatório.");
        RuleFor(x => x.Valor).GreaterThan(0).WithMessage("O valor deve ser maior que zero.");
    }
}
