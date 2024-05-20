using Application.Libraries;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Abstractions.Message;
public abstract class BaseCommandHandler
{
    protected readonly IDateTime _dateTime;
    protected readonly User UserContext = Libraries.UserContext.Instance;
    public BaseCommandHandler(IServiceProvider serviceProvider)
    {
        _dateTime = serviceProvider.GetService<IDateTime>();
    }
}
