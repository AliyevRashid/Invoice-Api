namespace Invoice_Api.DTO
{
    public class ReqisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
