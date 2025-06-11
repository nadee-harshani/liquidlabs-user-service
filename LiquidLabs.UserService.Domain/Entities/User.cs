namespace LiquidLabs.UserService.Domain.Entities
{
    /// <summary>
    /// Entity representing a user with personal details
    /// </summary>
    public class User
    {
        public int Id          { get; set; }
        public string Name     { get; set; }
        public string Username { get; set; }
        public string Email    { get; set; }
        public string Phone    { get; set; }
        public string Website  { get; set; }
        public Address Address { get; set; }

    }
}
