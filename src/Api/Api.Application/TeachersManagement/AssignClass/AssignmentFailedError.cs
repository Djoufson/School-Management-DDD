
namespace Api.Application.TeachersManagement.AssignClass;

public class AssignmentFailedError : IError
{
    public List<IError> Reasons => new();

    public string Message => "The assignment of the class to the required teacher failed unexpectedly";

    public Dictionary<string, object> Metadata => new();
}
