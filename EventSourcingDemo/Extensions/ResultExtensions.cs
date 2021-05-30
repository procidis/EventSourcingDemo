using EventSourcingDemo.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcingDemo.Extensions
{
    public static class ResultExtensions
    {
        public static SuccessDataResult<T> ToDataResult<T>(this T data) where T : class
        {
            return new SuccessDataResult<T>(data);
        }

        public static IActionResult ToActionResult<T>(this T data) where T : Result
        {
            if (data == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(data);
        }
    }
}
