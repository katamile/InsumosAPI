﻿namespace InsumosAPI.Middleware.Exceptions.BadRequest
{
    public class InvalidSintaxisException : BadRequestException
    {
        public InvalidSintaxisException() : base("Sintaxis no válida.")
        {
        }
    }
}
