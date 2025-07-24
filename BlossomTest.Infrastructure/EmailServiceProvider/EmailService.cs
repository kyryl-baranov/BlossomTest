namespace BlossomTest.Infrastructure.EmailServiceProvider;

public class EmailService : IEmailService
{    
    public Task<Result> SendAsync(string receiver, string subject, string body)
    {
        throw new NotImplementedException();
    }
}