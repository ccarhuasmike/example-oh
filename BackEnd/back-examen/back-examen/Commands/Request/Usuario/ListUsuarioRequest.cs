using back_examen.Commands.Response.Usuario;
using back_examen.Commands.Response.Common;
using back_examen.Common;
using MediatR;
namespace back_examen.Commands.Request.Usuario;

public class ListUsuarioRequest: IRequest<Response<ResponseUsuario>>
{
    
    public string? Accion { get; set; }
    public string? usuario { get; set; }
    public string? password { get; set; }
}