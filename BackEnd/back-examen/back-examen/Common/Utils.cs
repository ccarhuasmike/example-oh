namespace back_examen.Common;

public static class Utils
{
    public static int GetOffset(int pageSize, int pageNumber)
    {
        return (pageNumber - 1) * pageSize;
    }
    public static string BuildOrderBySqlUsingIntepolation(string sortOrderColumn, string sortOrderDirection)
    {
        string orderBy;
        switch (sortOrderColumn)
        {
            case "Name":
                orderBy = "a.[Name]";
                break;
            default:
                orderBy = "a.fechaRegistro";
                break;
        }
        if (!string.IsNullOrEmpty(sortOrderDirection))
        {
            var sortOrder = "asc";
            if (sortOrderDirection == "desc")
            {
                sortOrder = "desc";
            }
            orderBy = $"{orderBy} {sortOrder}";
        }
        return orderBy;
    }
        
}