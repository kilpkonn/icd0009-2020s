namespace PublicApi.DTO.v1.Identity
{
    public class Register
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
    }
}