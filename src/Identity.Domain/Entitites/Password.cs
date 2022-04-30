namespace Identity.Domain.Entities
{
    public class Password
    {
        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}
