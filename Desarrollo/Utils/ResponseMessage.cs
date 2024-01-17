namespace Desarrollo;

public class ResponseMessage
{
    #region Fields
    public bool Success { get; set; }
    public string Message { get; set; }=string.Empty;
    public object? Data { get; set; }

    #endregion

    #region Constructor
    public ResponseMessage(bool Success, string Message, object Data)
    {
        this.Success=Success;
        this.Message=Message;
        this.Data=Data;
    }

    public ResponseMessage(bool Success, string Message)
    {
        this.Success=Success;
        this.Message=Message;
        
    }
    #endregion

    public static ResponseMessage SuccessResponse(object? Data)
    {
        ResponseMessage response=new ResponseMessage(true, "SUCCESS", Data);

        return response;
    }

     public static ResponseMessage SuccessResponse()
    {
        ResponseMessage response=new ResponseMessage(true, "SUCCESS");

        return response;
    }

    public static ResponseMessage ErrorResponse(string Message)
    {
        ResponseMessage response=new ResponseMessage(false, Message);

        return response;
    }
}
