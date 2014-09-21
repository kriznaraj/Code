using System;

namespace Controls.ExceptionHandling
{
    public interface IExceptionManager
    {
        Exception Handle(Exception exception, out PostHandleAction action);

        object Convert(Exception exception);
    }
}