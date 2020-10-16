using System;

namespace Task2
{
    #warning зачем ты сделала это исключение? это, по большому счёту, бессмысленно, можно было и обычный Exception создать
    #warning просто хочу понять ход твоих мыслей :) 
    public class MyException : Exception
    {
        public override string Message { get; }

        public MyException(string message)
        {
            Message = message;
        }
    }
}