using back_examen.Commands.Request.Usuario;
using back_examen.Commands.Response.Common;
using back_examen.Commands.Response.Usuario;
using back_examen.Data;
using Dapper;
using MediatR;

namespace back_examen.Queries.Usuario;

public class ObtenerUsuarioQueriesHandler : IRequestHandler<ListUsuarioRequest, Response<ResponseUsuario>>
{
    private readonly IConexion _db;

    public ObtenerUsuarioQueriesHandler(IConexion db)
    {
        _db = db;
    }

    public async Task<Response<ResponseUsuario>> Handle(ListUsuarioRequest request, CancellationToken cancellationToken)
    {
        string mensaje="";
        bool respuesta=false;
        try
        {
            var response = new ResponseUsuario { };
            using var cn = await _db.CreateConnectionAsync();
           
            if (request.Accion == "ValidarUsuario")
            {
                response.Usuario = (await cn.QueryAsync<Model.Usuario>(
                    @"
                                 SELECT 
                                    [Extent1].*
                                 FROM   tb_usuario AS [Extent1]
                                 WHERE [Extent1].[usuario]  =@usuario
                                 and [Extent1].[password]  =@password
                                 ",
                    new { request.usuario, request.password})).FirstOrDefault();
                
                if (response.Usuario == null)
                {
                    mensaje = "usuario y/o password incorrecto.";
                    respuesta = false;
                }
                else
                {
                    mensaje = "usuario y password correcto";
                    respuesta = true;
                }
            }
            return new Response<ResponseUsuario> { Body = response, Code = 200, Success = respuesta, Message = mensaje };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}