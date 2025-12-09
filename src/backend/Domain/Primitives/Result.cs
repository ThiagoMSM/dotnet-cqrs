namespace Domain.Primitives;

public class Result<T>
{
    private readonly T? _value;

    //result pattern de fato
    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public Error Error { get; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess; // auxiliar

    // Private constructor: força o uso dos métodos de fábrica
    protected Result(T? value, bool isSuccess, Error error)
    {
        // Fail safe: Evita inconsistências, como sucesso com erro, ou não sucesso sem erro
        if (isSuccess && error != Error.None)
            throw new InvalidOperationException();

        if (!isSuccess && error == Error.None)
            throw new InvalidOperationException();

        _value = value;
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result<T> Success(T value) => new(value, true, Error.None);
    public static Result<T> Failure(Error error) => new(default, false, error);
}