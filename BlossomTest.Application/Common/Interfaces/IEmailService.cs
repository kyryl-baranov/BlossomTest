namespace BlossomTest.Application.Common.Interfaces;

public interface IEmailService
{
    Task<Result> SendAsync(string receiver, string subject, string body);
}