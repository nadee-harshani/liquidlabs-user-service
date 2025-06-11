namespace LiquidLabs.UserService.Domain.Entities
{
    /// <summary>
    /// Entity representing an address associated with a user.
    /// </summary>
    public class Address
    {
        public int Id         { get; set; }
        public int UserId     { get; set; }
        public string Street  { get; set; }
        public string Suite   { get; set; }
        public string City    { get; set; }
        public string Zipcode { get; set; }
    }
}
