namespace back_examen.Common;

public class Pagination
{
    public int pageSize { get; set; }
    public int pageNumber { get; set; }
    public string? sortOrderColumn { get; set; }
    public string? sortOrderDirection { get; set; }
}