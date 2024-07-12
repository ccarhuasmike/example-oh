using back_examen.Commands.Request.Empleado;
using back_examen.Commands.Response.Empleado;
using MediatR;
using back_examen.Commands.Response.Common;
using back_examen.Data;
using Dapper;

namespace back_examen.Commands.Empleado;

public class RegistrarEmpleadoQueriesHandler : IRequestHandler<EmpleadoRequest, Response<ResponseEmpleado>>
{
    private readonly IConexion _db;

    public RegistrarEmpleadoQueriesHandler(IConexion db)
    {
        _db = db;
    }

    public async Task<Response<ResponseEmpleado>> Handle(EmpleadoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            using var cn = await _db.CreateConnectionAsync();
            int? returnIds = 0;
            string mensaje;
            bool respuesta;
            if (request.Empleado?.accion == "Registrar" && request.Empleado?.id == 0)
            {
                Model.Empleado empleado = (await cn.QueryAsync<Model.Empleado>(
                    @"
                                 SELECT 
                                    [Extent1].*
                                 FROM   tb_empleado AS [Extent1]
                                 WHERE [Extent1].[documentoId]  =@documentoId",
                    new { request.Empleado.documentoId })).FirstOrDefault();

                if (empleado != null)
                {
                    mensaje = "El número de documento ya existe.";
                    respuesta = false;
                }
                else
                {
                    request.Empleado.fechaRegistro = DateTime.Now;
                    returnIds = await cn.ExecuteScalarAsync<int>(
                        @"insert into tb_empleado(                                    
                                    documentoId,
                                    nombres,
                                    apellidos,
                                    edad,
                                    fechaNacimiento,
                                    salario,
                                    fechaRegistro
                                    )
                                values (                                    
                                    @documentoId,
                                    @nombres,
                                    @apellidos,
                                    @edad,
                                    @fechaNacimiento,
                                    @salario,
                                    @fechaRegistro
                        ); select CAST(SCOPE_IDENTITY() as int)",
                        request.Empleado);
                    mensaje = returnIds > 0
                        ? "Se registro correctamente"
                        : "Hubo un error, por favor comuníquese con el administrador del sistema.";
                    respuesta = returnIds > 0;
                }
            }
            else
            {
                var update =
                    @"update tb_empleado set
                            nombres              =@nombres,
                            apellidos            =@apellidos,
                            edad                 =@edad,
                            fechaNacimiento      =@fechaNacimiento,
                            salario              =@salario                           
                    OUTPUT INSERTED.Id where Id = @Id";
                var returnId = await cn.ExecuteScalarAsync<int>(update, request.Empleado);

                returnIds = request.Empleado.id;
                mensaje = returnId > 0
                    ? "Se actualizo correctamente"
                    : "Hubo un error, por favor comuníquese con el administrador del sistema.";
                respuesta = returnId > 0;
            }

            return new Response<ResponseEmpleado>
                { Body = null!, Code = 200, Success = respuesta, Message = mensaje, Id = returnIds };
        }
        catch (Exception e)
        {
            return new Response<ResponseEmpleado>
            {
                Body = null!, Code = 500, Success = false,
                Message = $"Hubo un error, por favor comuníquese con el administrador del sistema.{e}"
            };
        }
    }
}