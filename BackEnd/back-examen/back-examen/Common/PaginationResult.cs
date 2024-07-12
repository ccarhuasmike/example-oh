namespace back_examen.Common;

public class PaginationResult<T>
{
    public int TotalRowCount { get; set; }
    public ICollection<T> Rows {get;set;}
}