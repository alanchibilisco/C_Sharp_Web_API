
namespace Desarollo.Models;

public class Empresa
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public static explicit operator Empresa(EmpresaClientesDTO v)
    {
        throw new NotImplementedException();
    }

    //public List<Cliente> clientes{get;set;}

    //public List<Empleado> empleados{get;set;}
}
