using System;

namespace Controls.ExceptionHandling
{
    public interface IExceptionHandler
    {
        Exception Handle(Exception exception, out  bool handled);
    }
}