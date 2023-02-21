namespace SinaraLib.Models;
public class Customer {
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Surname { get; set; }
    public string? Login { get; set; }
}
