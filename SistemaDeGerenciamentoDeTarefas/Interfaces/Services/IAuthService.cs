namespace SistemaDeGerenciamentoDeTarefas.Interfaces.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(string username);
    }
}
