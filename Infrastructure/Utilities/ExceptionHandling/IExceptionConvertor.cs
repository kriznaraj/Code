using System;

namespace Controls.ExceptionHandling
{
    public interface IExceptionConvertor
    {
        object Handle(Exception exception, out bool converted);
    }
}