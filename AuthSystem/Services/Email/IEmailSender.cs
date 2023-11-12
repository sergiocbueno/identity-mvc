namespace AuthSystem.Services.Email
{
    public interface IEmailSender<TUser> where TUser : class
    {
        Task SendConfirmationLinkAsync<TUser>(TUser user, string email, string callbackUrl);
    }
}