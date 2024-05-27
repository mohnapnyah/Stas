namespace PoverkaEZ.Data
{
    public class UserSingleton
    {
        private User _currentUser;

        public User CurrentUser
        {
            get => _currentUser;
            set => _currentUser = value;
        }

        public bool IsAuthenticated => _currentUser != null;
    }
}
