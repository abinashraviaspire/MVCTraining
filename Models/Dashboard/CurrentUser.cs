// Title       :Online Bus Booking System
//  Author      :Abinash(IFET)
// Created at  :01-03-2023
// Updated at  :07-04-2023
// Reviewed by :
// Reviewed at :




using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Collections;

namespace FirstApp.Models
{
public class CurrentUser{
static SqlConnection sqlConnection=new SqlConnection("Data Source=ASPLAP1139;Initial Catalog=BusBooking;Integrated Security=True");

public static string? Name{get;set;}
public static Guid UserId{get;set;}
public string? Date{get;set;}
public string? Destination{get;set;}
public string to="abinashabinash711@gmail.com";
public static string? display(String? Mail)
        {
            sqlConnection.Open();
            SqlCommand command=new SqlCommand("getUser",sqlConnection); 
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@EmailId",Mail);
            SqlDataReader sqlDataReader=command.ExecuteReader();
            while(sqlDataReader.Read()){
                Name=(string)sqlDataReader["Name"];
                UserId=(Guid)sqlDataReader["EmployeeId"];
            }
            sqlConnection.Close();
            return Name;
    }
 public static List<string> destinationList = new List<string>() { "Pondicherry", "Villupuram","Kovalam","Vandalur","Siruseri",};
public static string? bookBus(string? Destination,string Date)
        {   
                var dateTime = DateTime.Now;
                var shortDateValue = dateTime.ToShortDateString();
                string[] bookingDate = Date.Split("-");
                int[] booking = Array.ConvertAll(bookingDate, s => int.Parse(s));
                string[] todayDate = shortDateValue.Split("-");
                int[] today = Array.ConvertAll(todayDate, s => int.Parse(s));
                DateTime dt = Convert.ToDateTime(bookingDate[2]+"/"+bookingDate[1]+"/"+bookingDate[0]);
                DayOfWeek dow = dt.DayOfWeek;
                string day = dow.ToString();
                Console.WriteLine(day);
                if(day!="Sunday" && day!="Saturday"){
                if(booking[0]>=today[2]){
                    if(booking[1]>=today[1]){
                        if(booking[2]>=today[0]){ 
                            if (destinationList.Contains(Destination))
                            {
                                sqlConnection.Open();
                            SqlCommand command=new SqlCommand("bookBus",sqlConnection); 
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@name",Name);
                            command.Parameters.AddWithValue("@userId",UserId);
                            command.Parameters.AddWithValue("@destination",Destination);
                            command.Parameters.AddWithValue("@date",Date);
                            command.ExecuteNonQuery();
                            sqlConnection.Close();
                                return "Success";
                            }else
                            {
                                return "No-Destination";
                            }
                           
                        } 
                    }
                }
                }
                if(day=="Sunday" || day=="Saturday"){
                        return "Leave";
                }
            return "Fail";                 
    }
BookingHistory user=new BookingHistory();
private static List <BookingHistory> History=new  List<BookingHistory> ();
public static IEnumerable GetHistory()  
        {  
                SqlCommand sqlCommand = new SqlCommand("GetCurrentUserHistory", sqlConnection);  
                sqlCommand.CommandType = CommandType.StoredProcedure;  
                sqlCommand.Parameters.AddWithValue("@Name",Name);
                sqlConnection.Open();  
                SqlDataReader rdr = sqlCommand.ExecuteReader();  
                while (rdr.Read())  
                {   BookingHistory user=new BookingHistory();
                    user.Date = rdr["Date"].ToString();  
                    user.Destination = rdr["Destination"].ToString();  
                    History.Add(user);  
                }  
                sqlConnection.Close(); 
                return History;
        } 


        static public string sendNotification(string to,CurrentUser currentUser){
                  
                string from, pass, messageBody;
                MailMessage message=new MailMessage();
                from="abinashabinash711@gmail.com";
                  pass  = "vzkgbrayouwkqrit";
                    messageBody="Hii, "+Name+"\nyour Bookings in Online Bus Booking System is Confirmed.\nYour Bookings on"+currentUser.Date+"in the Destination of "+currentUser.Destination;
                   message.To.Add(new MailAddress(to));
                   message.From=new MailAddress(from);
                     message.Body=messageBody;
                    message.Subject="Bus Booking-Abinash";
                SmtpClient smtp=new SmtpClient("smtp.gmail.com");
        smtp.EnableSsl=true;
        smtp.Port=587;
        smtp.DeliveryMethod=SmtpDeliveryMethod.Network;
        smtp.Credentials=new NetworkCredential(from,pass);
        smtp.Send(message);
        return "sent";
        
}
 
 public static void sendRequest(string Request){
        sqlConnection.Open();
        SqlCommand command=new SqlCommand("sendRequest",sqlConnection); 
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@name",Name);
        command.Parameters.AddWithValue("@userId",UserId);
        command.Parameters.AddWithValue("@message",Request);
        command.ExecuteNonQuery();
        sqlConnection.Close();

 }
    }
}