using back_examen.Common;
namespace back_examen.Commands.Response.Empleado;

public class ResponseEmpleado
{
    public Model.Empleado Empleado { get; set; }
    public PaginationResult<Model.Empleado> listaEmpleado { get; set; }
}