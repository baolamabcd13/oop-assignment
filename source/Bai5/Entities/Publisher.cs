namespace Entities
{
    public class Publisher
    {
        public string PublisherCode { get; set; }
        public string PublisherName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Publisher publisher && PublisherCode == publisher.PublisherCode;
        }

        public override int GetHashCode()
        {
            return PublisherCode.GetHashCode();
        }
    }
}