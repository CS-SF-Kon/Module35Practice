using Module35Practice.Models.Users;

namespace Module35Practice.ViewModels.Account;

public class UserViewModel
{
    public User User { get; set; }

    public UserViewModel(User user)
    {
        User = user;
    }

    public List<User> Friends { get; set; }
}
