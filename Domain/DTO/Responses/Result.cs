using Domain.Enums;

namespace Domain.DTO.Responses;

public class Result 
{
    public List<MessageResponse> Messages { get; private set; }
    public bool HasErro { get; private set; } = false;

    public Result()
    {
        Messages = new List<MessageResponse>();
    }

    public void WithMessage(string message, Guid? code = null)
    {
        Messages.Add(new MessageResponse
        {
            Message = message,
            Type = MessageType.Information,
            Code = code,
        });
    }

    public void WithError(string message, Guid? code = null) => WithError(message, 400, code);
    public void WithError(string message, int httpCode, Guid? code = null)
    {
        Messages.Add(new MessageResponse
        {
            Message = message,
            Type = MessageType.Error,
            Code = code,
        });

        HasErro = true;
    }
}

public class Result<T> : Result
{
    public T? Data { get; set; }
    
    public Result()
    {
        
    }
}