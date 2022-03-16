using Domain.Enums;
using System;

namespace Domain.DTO.Responses;

public class MessageResponse
{
    public Guid? Code { get; set; }
    public string? Message { get; set; }
    public MessageType Type { get; set; }
}