namespace AuthSystem.Services.Email
{
    public class EmailSender<TUser> : IEmailSender<TUser> where TUser : class
    {
        public async Task SendConfirmationLinkAsync<TUser>(TUser user, string email, string callbackUrl)
		{
			throw new NotImplementedException();
		}
    }
}