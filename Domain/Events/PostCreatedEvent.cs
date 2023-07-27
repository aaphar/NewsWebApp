using Domain.Common;
using Domain.Entities;

namespace Domain.Events;
public class PostCreatedEvent : BaseEvent
{
    public PostCreatedEvent(Post post)
    {
        Post = post;
    }

    public Post Post { get; }
}

