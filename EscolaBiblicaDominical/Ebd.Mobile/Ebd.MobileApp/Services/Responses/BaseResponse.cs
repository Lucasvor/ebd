namespace Ebd.Mobile.Services.Responses
{
    public class BaseResponse<T> : AbstractResponse
    {
        public BaseResponse(T data)
        {
            IsSuccess = true;
            Data = data;
        }

        public BaseResponse(Exception exception)
        {
            IsSuccess = false;
            Exception = exception;
        }

        public bool IsSuccess { get; private set; }
        public bool HasError => !IsSuccess;
        public T Data { get; private set; }

        public static BaseResponse<T> Sucesso(T data) => new BaseResponse<T>(data);

        public static BaseResponse<T> Falha(Exception exception) => new BaseResponse<T>(exception);

        public Exception Exception { get; private set; }
    }
}
