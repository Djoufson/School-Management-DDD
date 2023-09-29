namespace Api.Application.ClassesManagement.Errors;

public class ClassNotFoundError : IError
{
    private readonly string _classsId;

    public ClassNotFoundError(string classsId)
    {
        _classsId = classsId;
    }

    public List<IError> Reasons => new();

    public string Message => $"The class with Id {_classsId} could not be found";

    public Dictionary<string, object> Metadata => throw new NotImplementedException();
}
