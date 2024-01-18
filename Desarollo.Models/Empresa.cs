namespace Desarollo.Models;

public class Empresa
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public List<Cliente> clientes{get;set;}
}
