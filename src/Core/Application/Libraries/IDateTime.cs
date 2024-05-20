namespace Application.Libraries;

public interface IDateTime
{
    DateTimeOffset Now { get; }
}