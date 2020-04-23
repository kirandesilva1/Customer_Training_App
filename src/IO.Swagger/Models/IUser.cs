namespace IO.Swagger.Models
{
    /// <summary>
    /// User Interface
    /// </summary>
    public interface IUser
    {
        string UserId { get; set; }
        string Username { get; set; }
        string Firstname { get; set; }
        string Lastname { get; set; }
        string Emailaddress { get; set; }
        string Phonenumber { get; set; }
        string Password { get; set; }
        string Token { get; set; }
    }
}