namespace Desarollo.Models;

public class PostClienteDTO
{
    public string nombre { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string edad { get; set; } = string.Empty;
}

public class PostEmpresaDto
{
    public string nombre { get; set; } = string.Empty;
}

public class PostEmpleadoDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public int EmpresaId { get; set; }
}

public class PostEmpleadoTDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public int EmpresaId { get; set; }
    public int CargoId { get; set; }
}

public class PostCargoDto
{
    public string Nombre_Cargo { get; set; } = string.Empty;
}


public class PostClienteDto2
{
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int EmpresaId { get; set; }
}


public class EmpresaClientesDTO
{
    public int id {get;set;}
    public string nombre {get;set;}=string.Empty;
    public List<Cliente> clientes {get;set;}
}
