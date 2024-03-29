namespace Shared.Dtos;

public class ResponseDto<T> where T: class
{
    public T? Data {get; set;}
    public int StatusCode {get; set;}

    public ErrorDto? Error { get; set; }

    public static ResponseDto<T> Success(T data, int statusCode) {
        return new ResponseDto<T> {Data = data,StatusCode = statusCode , Error = null};
    }

    public static ResponseDto<T> Success(int statusCode) {
        return new ResponseDto<T> {StatusCode = statusCode};
    }

    public static ResponseDto<T> Fail(ErrorDto error,int statusCode){
        return new ResponseDto<T> {Data = null,Error = error,StatusCode = statusCode};
    }

    public static ResponseDto<T> Fail(string errorMessage,int statusCode){
        var errordto = new ErrorDto(errorMessage);
        return new ResponseDto<T> {Data = null,Error = errordto,StatusCode = statusCode};
    }
}
