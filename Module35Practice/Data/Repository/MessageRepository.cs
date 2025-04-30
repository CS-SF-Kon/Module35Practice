using Module35Practice.Models.Users;

namespace Module35Practice.Data.Repository;

public class MessageRepository : Repository<Message>
{
    public MessageRepository(AppContext db) : base(db)
    {

    }
}
