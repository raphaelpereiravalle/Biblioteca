namespace Biblioteca.Application.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
    public IEnumerable<string>? Errors { get; set; }

    public ApiResponse(bool success, string message, T? data = default, IEnumerable<string>? errors = null)
    {
        Success = success;
        Message = message;
        Data = data;
        Errors = errors;
    }

    // Sucesso
    public static ApiResponse<T> Ok(T data, string message = "Operação realizada com sucesso.")
        => new(true, message, data);

    // Criado
    public static ApiResponse<T> Created(T data, string message = "Registro criado com sucesso.")
        => new(true, message, data);

    // Não encontrado
    public static ApiResponse<T> NotFound(string message = "Registro não encontrado.")
        => new(false, message);

    // Erro de validação
    public static ApiResponse<T> ValidationError(IEnumerable<string> errors, string message = "Ocorreram erros de validação.")
        => new(false, message, default, errors);

    // Erro interno
    public static ApiResponse<T> Error(string message = "Ocorreu um erro interno no servidor.")
        => new(false, message);
}
