﻿using System.Net;

namespace TrashGrounds.Comment.Infrastructure.Exceptions;

public class UnauthorizedException : DomainException
{
    public UnauthorizedException(string message) : base(message, (int)HttpStatusCode.Unauthorized)
    {
    }
}