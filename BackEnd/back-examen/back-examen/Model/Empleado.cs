namespace back_examen.Model;

public class Empleado
{
    public string? accion { get; set; }
    public int TotalRegistros { get; set; }
    public int? id { get; set; }
    public string? documentoId { get; set; }
    public string? nombres { get; set; }
    public string? apellidos { get; set; }
    public int? edad { get; set; }
    public DateTime? fechaNacimiento { get; set; }
    public decimal? salario { get; set; }
    public DateTime? fechaRegistro { get; set; }
}