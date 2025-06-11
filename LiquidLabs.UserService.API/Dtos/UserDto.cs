namespace LiquidLabs.UserService.API.Dtos
{
    /// <summary>
    /// Data Transfer Object representing a user with personal details
    /// and associated address information.
    /// </summary>
    public class UserDto
    {
        public int Id             { get; set; }
        public string Name        { get; set; }
        public string Username    { get; set; }
        public string Email       { get; set; }
        public string Phone       { get; set; }
        public string Website     { get; set; }
        public AddressDto Address { get; set; }
    }
}
