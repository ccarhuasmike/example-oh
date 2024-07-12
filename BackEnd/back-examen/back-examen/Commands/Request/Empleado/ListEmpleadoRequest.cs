using MediatR;
using back_examen.Commands.Response.Empleado;
using back_examen.Commands.Response.Common;
using back_examen.Common;

namespace back_examen.Commands.Request.Empleado;

public class ListEmpleadoRequest : IRequest<Response<ResponseEmpleado>>
{
    public string? Accion { get; set; }
    public Pagination? Pagination { get; set; }
    public int? Id { get; set; }
    public string? documentoId { get; set; }
    public string? nombres { get; set; }
}