using Backend_Interview.ViewModel;
using Backend_Interview.ViewModel.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Interview.Controllers
{
    [ApiController]
    [Route("api/math")]
    public class MathController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public MathController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [Route("getAnswer")]
        [HttpPost]
        public IActionResult GetAnswer(GetAnswerViewModel model)
        {
            try
            {
                //int Min = 0;
                //int Max = 1000;

                //// this declares an longeger array with 5 elements
                //// and initializes all of them to their default value
                //// which is zero
                //long[] array = new long[5];

                //Random randNum = new Random();
                //for (long i = 0; i < array.Length; i++)
                //{
                //    array[i] = randNum.Next(Min, Max);
                //}
                var array = model.array;
                var sortResult = Sort(array);
                var data = string.Join(", ", sortResult);
                var result = new ApiSuccessResult<string>("Success to sort!");
                result.ResultObj = data;
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAnswer: ${ex.Message}");
                var result = new ApiErrorResult<string>("Error when GetAnswer");
                return BadRequest(result);
            }

        }


        private static List<long> Sort(long[] array)
        {
            var data = Enumerable.Repeat(long.MinValue, array.Length).ToList();

            if (array.Count() > 1)
            {
                Array.Sort(array);
                Array.Reverse(array);
            }
            var listReq = array.ToList();
            //Dua 10 so lon nhi ra dau tien
            List<long> arrayMaxRangeSecond = new List<long>();
            if (listReq.Count > 10)
            {
                if (listReq.Count < 20)
                {
                    arrayMaxRangeSecond = listReq.GetRange(10, listReq.Count - 10);
                }
                else
                {
                    arrayMaxRangeSecond = listReq.GetRange(10, 10);
                }
            }
            data.RemoveRange(0, arrayMaxRangeSecond.Count);
            data.InsertRange(0, arrayMaxRangeSecond);
            //Dua 10 so lon nhat vao chinh giua
            if (listReq.Count <= 10)
            {
                return listReq;
            }
            var arrayMaxRangeFirst = listReq.GetRange(0, 10);
            int centerIndex = (listReq.Count - arrayMaxRangeFirst.Count) / 2;
            if (centerIndex <= arrayMaxRangeSecond.Count - 1)
            {
                data.RemoveRange(arrayMaxRangeSecond.Count, arrayMaxRangeFirst.Count);
                data.InsertRange(arrayMaxRangeSecond.Count, arrayMaxRangeFirst);
            }
            else
            {
                data.RemoveRange(centerIndex, arrayMaxRangeFirst.Count);
                data.InsertRange(centerIndex, arrayMaxRangeFirst);
            }

            //Dua 10 so lon thứ 3 ra cuối

            List<long> arrayMaxRangeThird = new List<long>();
            if (listReq.Count > 20)
            {
                int totalFirstMaxAndScondMax = arrayMaxRangeFirst.Count + arrayMaxRangeSecond.Count;
                if (listReq.Count < 30)
                {
                    arrayMaxRangeThird = listReq.GetRange(totalFirstMaxAndScondMax, listReq.Count - totalFirstMaxAndScondMax);
                }
                else
                {
                    arrayMaxRangeThird = listReq.GetRange(totalFirstMaxAndScondMax, 10);
                }
                data.RemoveRange(listReq.Count - arrayMaxRangeThird.Count, arrayMaxRangeThird.Count);
                data.InsertRange(listReq.Count - arrayMaxRangeThird.Count, arrayMaxRangeThird);
            }

            //Xep cac so con lai
            var extantIndex = arrayMaxRangeFirst.Count + arrayMaxRangeSecond.Count + arrayMaxRangeThird.Count;
            var extantElements = listReq.GetRange(extantIndex, listReq.Count - extantIndex);
            int index = 0;
            if (extantElements.Count > 0)
            {
                for (int j = 0; j < data.Count; j++)
                {
                    if (data[j] == long.MinValue)
                    {
                        data[j] = extantElements[index];
                        index++;
                    }
                }
            }

            return data;

        }


    }
}
