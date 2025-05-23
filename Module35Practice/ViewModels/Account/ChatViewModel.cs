﻿using Module35Practice.Models.Users;

namespace Module35Practice.ViewModels.Account;

public class ChatViewModel
{
    public User You { get; set; }

    public User ToWhom { get; set; }

    public List<Message> History { get; set; }

    public MessageViewModel NewMessage { get; set; }

    public ChatViewModel()
    {
        NewMessage = new MessageViewModel();
    }
}
