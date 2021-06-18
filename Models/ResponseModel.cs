namespace WeatherCities.Models
{
    public class ResponseResult
    {
        public static ResponseModel<T> Success<T>(T data)
        {
            return new ResponseModel<T>()
            {
                Success = true,
                Message = "Success",
                Data = data
            };
        }
        public static ResponseModel<string> Error(string message)
        {
            return new ResponseModel<string>()
            {
                Success = false,
                Message = message
            };
        }
    }
    public class ResponseModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

    }
}
