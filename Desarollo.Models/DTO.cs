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
    public int Id {get;set;}
    public string Nombre {get;set;}=string.Empty;
    public List<Cliente> Clientes {get;set;}
}

public class EmpresaEmpleadosDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; }=string.Empty;
    public List<Empleado> Empleados {get;set;}
}


public class EmpleadosDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; }=string.Empty;
    public string Apellido { get; set; }=string.Empty;
    public string Empresa { get; set; }=string.Empty;
    public string Cargo { get; set; }=string.Empty;
}

public class EmpresaEmpleadoQueryResponse
{
    public int id { get; set; }
    public string nombre { get; set; }=string.Empty;
    public int empleadoId { get; set; }
    public string empleadoNombre { get; set; }=string.Empty;
    public string empleadoApellido { get; set; }=string.Empty;
    public string empleadoCargo { get; set; }=string.Empty;
}

public class EmpleadoItem
{
    public int empleadoId { get; set; }
    public string empleadoNombre { get; set; }=string.Empty;
    public string empleadoApellido { get; set; }=string.Empty;
    public string empleadoCargo { get; set; }=string.Empty;

}

public class EmpresaEmpleadoListResponse
{
    public int id { get; set; }
    public string nombre { get; set; }=string.Empty;
    public List<EmpleadoItem>? Empleados { get; set; }
}


public class PostUserDto
{
    public string Email { get; set; }=string.Empty;
    public string Password { get; set; }=string.Empty;
    public string Role { get; set; }=string.Empty;
}


public class UserResponseDto
{
    public int Id { get; set; }
    public string Email { get; set; }=string.Empty;
    public string Password { get; set; }=string.Empty;
    public string role { get; set; }=string.Empty;
}