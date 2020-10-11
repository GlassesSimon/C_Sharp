using System;

namespace Task2
{
    public class MyException : Exception
    {
        public override string Message { get; }

        public MyException(string message)
        {
            Message = message;
        }
    }
}