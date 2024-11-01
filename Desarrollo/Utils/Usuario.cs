using System;

namespace Desarrollo.Utils;

public class Usuario
{
    public int Id { get; set; }
    public string IdProveedor { get; set; }=string.Empty;
    public string CorreoElectronico { get; set; }=string.Empty;
    public string NombreCompleto { get; set; }=string.Empty;
    public string Nombre { get; set; }=string.Empty;
    public string Apellido { get; set; }=string.Empty;
    public TipoUsuarioRegistro TipoRegistro { get; set; }
    public TipoUsuario TipoUsuario { get; set; }

}
