﻿using Microsoft.EntityFrameworkCore;
using Module35Practice.Models.Users;

namespace Module35Practice.Data.Repository;

public class MessageRepository : Repository<Message>
{
    public MessageRepository(AppContext db) : base(db)
    {

    }

    public List<Message> GetMessages(User sender, User recipient)
    {
        Set.Include(x => x.Recipient);
        Set.Include(x => x.Sender);

        var from = Set.AsEnumerable().Where(x => x.SenderId == sender.Id && x.RecipientId == recipient.Id).ToList();
        var to = Set.AsEnumerable().Where(x => x.SenderId == recipient.Id && x.RecipientId == sender.Id).ToList();

        var itog = new List<Message>();
        itog.AddRange(from);
        itog.AddRange(to);
        itog.OrderBy(x => x.Id);
        return itog;
    }
}
