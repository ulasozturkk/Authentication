namespace Shared.Dtos;

public class ErrorDto
{
    public List<string> Errors { get; private set; }

    public ErrorDto(string error) {
        Errors = new List<string> ();
        Errors.Add(error);
    }

    public ErrorDto(List<string> errors) {
        Errors = errors;
    }
}
