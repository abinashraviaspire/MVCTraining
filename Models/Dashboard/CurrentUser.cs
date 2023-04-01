// Title       :Online Bus Booking System
//  Author      :Abinash(IFET)
// Created at  :01-03-2023
// Updated at  :13-03-2023
// Reviewed by :
// Reviewed at :




using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace FirstApp.Models
{
public class CurrentUser{
    static SqlConnection sqlConnection=new SqlConnection("Data Source=JOSEPHABI;Initial Catalog=BusBooking;Integrated Security=True");
CurrentUser currentUser=new CurrentUser();
public static string? Name{get;set;}
public static int UserId{get;set;}

public string? Date{get;set;}
public string? Destination{get;set;}
public static int  BusId=1;

        public static string? display(String? Mail)
        {
                sqlConnection.Open();
                SqlCommand command=new SqlCommand("getUser",sqlConnection); 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmailId",Mail);
                SqlDataReader sqlDataReader=command.ExecuteReader();
                while(sqlDataReader.Read()){
                    Name=Convert.ToString(sqlDataReader[0]);         
                }
                sqlConnection.Close();
                return Name;
    }
public static string? bookBus(string? Destination,string Date)
        {
                Console.WriteLine(Destination+"kkk");
                sqlConnection.Open();
                SqlCommand command=new SqlCommand("bookBus",sqlConnection); 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BusId",BusId);
                command.Parameters.AddWithValue("@UserId",UserId);
                command.Parameters.AddWithValue("@UserName",Name);
                command.Parameters.AddWithValue("@Destination",Destination);
                command.Parameters.AddWithValue("@Date",Date);
                command.ExecuteNonQuery();
                sqlConnection.Close();
                return "Success";
    }
    }
}