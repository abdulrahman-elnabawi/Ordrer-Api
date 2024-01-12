namespace Demo.HandelResponses
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDeafaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDeafaultMessageForStatusCode(int code)
            => code switch
            {
                400 => "Bad Requrest",
                401 => "You are not authorized!!",
                404 => "Resourse not Found!",
                500 => "Internal Server Error",
                _ => null
            };

    }
}
