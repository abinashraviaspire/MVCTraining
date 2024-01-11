using System.ComponentModel.DataAnnotations;
namespace FirstApp.Models{

public class Request{
public Guid UserId{get;set;}
[Key]
public int Id{get;set;}
public string? Name{get;set;}
 [StringLength(100,MinimumLength =10,ErrorMessage ="Please Give a Description")]
public string? Message{get;set;}
    }
}