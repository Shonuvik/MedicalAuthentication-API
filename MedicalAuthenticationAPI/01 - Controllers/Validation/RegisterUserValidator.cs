using FluentValidation;
using MedicalAuthenticationAPI.Controllers.V1;

namespace MedicalAuthenticationAPI.Controllers.Validation
{
    public class RegisterUserValidator : AbstractValidator<UserCreateDto>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("Nome é obrigatório.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("E-mail é obrigatório.")
                .EmailAddress().WithMessage("Informe um e-mail válido.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("A Senha é obrigatório.")
                .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");
        }
    }
}
