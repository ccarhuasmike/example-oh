using back_examen.Commands.Request.Empleado;
using back_examen.Commands.Response.Common;
using back_examen.Commands.Response.Empleado;
using back_examen.Common;
using back_examen.Data;
using Dapper;
using MediatR;

namespace back_examen.Queries.Empleado;

public class ObtenerEmpleadoQueriesHandler : IRequestHandler<ListEmpleadoRequest, Response<ResponseEmpleado>>
{
    private readonly IConexion _db;

    public ObtenerEmpleadoQueriesHandler(IConexion db)
    {
        _db = db;
    }

    public async Task<Response<ResponseEmpleado>> Handle(ListEmpleadoRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = new ResponseEmpleado { };
            using var cn = await _db.CreateConnectionAsync();

            if (request.Accion == "ObtenerEmpleadoPorId")
            {
                if (request.Id > 0)
                {
                    response.Empleado = (await cn.QueryAsync<Model.Empleado>(
                        @"
                                 SELECT 
                                    [Extent1].*
                                 FROM   tb_empleado AS [Extent1]
                                 WHERE [Extent1].[Id]  =@Id",
                        new { request.Id })).FirstOrDefault();
                }
            }

            if (request.Accion == "ListarPaginadoEmpleado")
            {
                var offset = Utils.GetOffset(request.Pagination.pageSize, request.Pagination.pageNumber);
                var orderBySql = Utils.BuildOrderBySqlUsingIntepolation(request.Pagination.sortOrderColumn,
                    request.Pagination.sortOrderDirection);
                var sql =
                    $@"
                    SELECT  
                    COUNT([Extent1].[Id]) OVER() TotalRegistros, 
                    [Extent1].*
                    FROM    tb_empleado AS [Extent1]
                    where (UPPER ([Extent1].documentoId ) LIKE '%'+@documentoId+'%' or isnull(@documentoId,'') = '')    
                    AND (UPPER ([Extent1].nombres +' '+[Extent1].apellidos ) LIKE '%'+@nombres+'%' or isnull(@nombres,'') = '')
                      ";
                sql += $@" 
	                ORDER BY [Extent1].[Id] desc
	                OFFSET @Offset ROWS
	                FETCH NEXT @PageSize ROWS ONLY";
                var multi = (await cn.QueryAsync<Model.Empleado>(sql, new
                {
                    request.Pagination.pageSize,
                    offset,
                    request.documentoId,
                    request.nombres
                })).ToList();
                response.listaEmpleado = new PaginationResult<Model.Empleado>
                {
                    TotalRowCount = multi.FirstOrDefault() == null ? 0 : multi.FirstOrDefault().TotalRegistros,
                    Rows = multi
                };
            }
            return new Response<ResponseEmpleado> { Body = response, Code = 200, Success = true, Message = "" };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}