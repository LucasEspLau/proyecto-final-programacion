namespace trabajo_final_grupo_verde.Models
{
    public interface IMyEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}