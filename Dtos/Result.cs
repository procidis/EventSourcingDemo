using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingDemo.Dtos
{
    public class Result
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public int Code { get; set; }
    }


    public class FailResult : Result
    {
        public FailResult(string message)
        {
            Message = message;
            IsSuccess = false;
        }

        public FailResult(string message, int code) : this(message)
        {
            Code = code;
        }
    }

    public class SuccessResult : Result
    {
        public SuccessResult()
        {
            Code = 200;
            IsSuccess = true;
        }

        public SuccessResult(int code)
        {
            Code = code;
            IsSuccess = true;
        }

        public SuccessResult(int code, string message) : this(code)
        {
            Message = message;
        }
    }

    public class SuccessDataResult<T> : SuccessResult
    {
        public SuccessDataResult(T data) : base()
        {
            Data = data;
        }

        public SuccessDataResult(T data, int code) : base(code)
        {
            Data = data;
        }

        public SuccessDataResult(T data, int code, string message) : base(code, message)
        {
            Data = data;
        } 
        public T Data { get; set; }
    }
}
