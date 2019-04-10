using System;
using System.Collections.Generic;
using System.Text;

namespace SignalrTest.Common
{

    public enum ProtocolMessageType
    {
        Text = 1,
        Video = 2,
        Image = 3,
        File = 4,
        Info = 5,
        JoinedGroup = 6,
        LeftGroup = 7,
        Kick = 8,
        GroupCreated = 9,
    }

    public class ProtocolGroupMessage
    {
        public string Id { get; set; }

        public ProtocolSender From { get; set; }

        public ProtocolSender AffectedMember { get; set; }

        public DateTime Date { get; set; }

        public string GroupId { get; set; }

        public string Body { get; set; }

        public ProtocolMessageType MessageType { get; set; }

        public ProtocolGroupMessage()
        {
            Id = Guid.NewGuid().ToString().ToLower();

            Date = DateTime.UtcNow;
        }
    }
}
