namespace BlazorAppLogin.Authentication
{
    public class UserAccountService
    {
        private List<UserAccount> _users;
        public string a { get; set; } = "";
        public UserAccountService(IConfiguration configuration)
        {

          

            a = configuration.GetConnectionString("myDatabase");


            _users = new List<UserAccount>
            {
                new UserAccount{UserName="Admin", Password="Admin",Role="administrator"},
                new UserAccount{UserName="user", Password="user",Role="user"},
                new UserAccount{UserName="tech", Password="tech",Role="technical"}
            };
        }

        public UserAccount? GetByUserName(string userName)
        {
            return _users.FirstOrDefault(x => x.UserName == userName);
        }
    }
}
