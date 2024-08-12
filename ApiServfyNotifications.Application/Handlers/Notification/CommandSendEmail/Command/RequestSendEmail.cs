using Amazon.Runtime.Internal;
using ApiServfyNotifications.Application.Handlers.Notification.CommandSendEmail.Response;
using FluentValidation;
using MediatR;

namespace ApiServfyNotifications.Application.Handlers.Notification.CommandSendEmail.Command
{
    public class RequestSendEmail : IRequest<ResponseSendEmail>
    {
        public string NotificationType { get; set; }
        public IDictionary<string, string> Params { get; set; }
        public ICollection<RequestSendEmailTo> To { get; set; }
    }

    public class RequestSendEmailTo
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }

    public class RequestSendEmailValidator : AbstractValidator<RequestSendEmail>
    {
        public RequestSendEmailValidator()
        {
            RuleFor(x => x.NotificationType).NotNull().WithMessage("NotificationType is required.").NotEqual("").WithMessage("NotificationType not be empty.");
            RuleFor(x => x.Params).NotNull().WithMessage("Params is required.");
            RuleFor(x => x.To).NotNull().WithMessage("To is required.");
        }
    }
}
