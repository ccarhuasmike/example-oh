namespace back_examen.Commands.Response.Common;

public class Response<T>
{
    public Response()
    {
    }
    public Response(T body, T err, bool success = true, int code = 200, int totalRecord = 0, byte[] file = null!, String information = null!, int? Id = 0!, String Codigo = null!)
    {
        Success = success;
        Body = body;
        Id = Id;
        Error = err;
        Message = string.Empty;
        Errors = null!;
        Code = code;
        TotalRecord = totalRecord;
        File = file;
        Information = information;
        Codigo = Codigo;
    }

    public bool Success { get; set; }
    public T Body { get; set; }
    public byte[] File { get; set; }
    public T Error { get; set; }
    public string[] Errors { get; set; }
    public int Code { get; set; }
    public string Message { get; set; }
    public int ?Id { get; set; }
    public string Information { get; set; }
    public string Codigo { get; set; }
    public int TotalRecord { get; set; }
}