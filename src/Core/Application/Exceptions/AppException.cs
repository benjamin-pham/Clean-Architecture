using Application.Libraries;
using System.Globalization;

namespace Application.Exceptions;
public class AppException : Exception
{
    public AppException() : base() { }

    public AppException(string message) : base(message) { }

    public AppException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }

    public static void NotFoundId(Type type, object id)
    {
        Result.Failure($"{type.Name}Id {id} not found");
    }
    public static void NotFoundId<TObject>(TObject obj, object id)
    {
       
        if (obj == null)
        {
            NotFoundId(typeof(TObject), id);
        }
    }
    public static void CheckDataExist<TObject>(TObject obj, object id)
    {
        if (obj == null)
        {
            NotFoundId(typeof(TObject), id);
        }
    }

}