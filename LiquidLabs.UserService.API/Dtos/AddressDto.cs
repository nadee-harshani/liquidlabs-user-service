namespace LiquidLabs.UserService.API.Dtos
{
    /// <summary>
    /// Data Transfer Object representing an address associated with a user.
    /// </summary>
    public class AddressDto
    {
        public int Id         { get; set; }
        public int UserId     { get; set; }
        public string Street  { get; set; }
        public string Suite   { get; set; }
        public string City    { get; set; }
        public string Zipcode { get; set; }
    }
}
