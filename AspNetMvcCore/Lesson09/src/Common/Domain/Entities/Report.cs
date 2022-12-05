using Domain.Base.Entities;

namespace Domain.Entities
{
    public class Report : Entity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime SendAt { get; set; }
        public bool IsSent { get; set; }
    }
}
