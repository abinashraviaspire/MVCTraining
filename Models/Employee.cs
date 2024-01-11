// Title       :Online Bus Booking System
//  Author      :Abinash(IFET)
// Created at  :01-03-2023
// Updated at  :13-03-2023
// Reviewed by :
// Reviewed at :

using System.ComponentModel.DataAnnotations;

namespace FirstApp.Models;
public class Employee{
  public Guid EmployeeId{get;set;}
  public string? Name{get;set;}

  [Required(ErrorMessage = "Enter e-mail")]
  [EmailAddress]
  [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please Provide Valid Email")]  
  public string? EmailId{get;set;}

  [Required(ErrorMessage = "Enter Password")]
  [DataType(DataType.Password)]
  public string? userPassword{get;set;}
   public DateTime DateOfBirth { get; set; }

    public string? Department{get;set;}
}
