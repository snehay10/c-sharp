using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Xml.Linq;

namespace ecommysql
{
    public class Program
    {
        static long custMobile;
        static string custName;
        static int act = 0;
        static long adminMobile;
        static string adminName;
        string name;
        int price;
        int quantity;
        long admin_mobile;

        static string connectionString = "Data Source=DESKTOP-QNJIJNF\\SQLEXPRESS;Initial Catalog=ecom;Integrated Security=True";

        static void Main(string[] args)
        {
            Console.WriteLine("ENTER YOUR CHOICE \n1:EXISTING CUSTOMER \n2:NEW CUSTOMER \n3:EXISTING OWNER \n4:NEW OWNER \n");
            int person = Convert.ToInt32(Console.ReadLine());
            string ch = "y";

            switch (person)
            {
                case 1:
                    Console.WriteLine("ENTER CUSTOMER mobile number :");
                    custMobile = Convert.ToInt64(Console.ReadLine()); 

                    bool isCustomerPresent = CheckCustPresence(custMobile);

                    if (isCustomerPresent)
                    {
                        act = 1 ;
                       
                    }
                    else
                    {
                        Console.WriteLine("something is wrong ......");
                    }

                    break;

                case 2:
                    Console.Write("ENTER CUSTOMER MOBILE NUMBER : ");
                    custMobile = Convert.ToInt64(Console.ReadLine());

                    Console.Write("ENTER CUSTOMER NAME : ");
                    custName = Console.ReadLine();

                    InsertCustomer(custMobile,custName);
                    act = 1;
                    break;

                case 3:
                    Console.WriteLine("ENTER ADMIN MOBILE NUMBER :");
                    adminMobile = Convert.ToInt64(Console.ReadLine());

                    bool isAdminPresent = CheckAdminPresence(adminMobile);

                    if (isAdminPresent)
                    {
                        act = 2;

                    }
                    else
                    {
                        Console.WriteLine("something is wrong ......");
                    }

                    break;

                case 4:
                    Console.Write("ENTER ADMIN MOBILE NUMBER : ");
                    adminMobile = Convert.ToInt64(Console.ReadLine());

                    Console.Write("ENTER ADMIN NAME : ");
                    adminName = Console.ReadLine();

                    InsertAdmin(adminMobile, adminName);
                    act = 2;
                    break;
                default:
             
                    Console.WriteLine("Invalid choice.");
                    break;
            }




            if (act == 2)
            {
                while (ch == "Y" || ch == "y")
                {
                    Console.WriteLine("\n\nENTER YOUR CHOICE \n1:ADD ITEM INTO LIST\n2:UPDATE LIST\n3:REMOVE ITEM FROM LIST\n4:VIEW LIST\n5:EXIT\n\n");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                              admin_adding(adminMobile);

                            break;


                        case 2:
                            Console.Write("\nENTER NAME OF ITEM : ");
                         //   name = Console.ReadLine();
                            Console.Write("ENTER NEW PRICE OF ITEM : ");
                            int pricenm = Convert.ToInt32(Console.ReadLine());
                            Console.Write("ENTER NEW  QUANTITY OF ITEM : ");
                            int quantitynm = Convert.ToInt32(Console.ReadLine());

                        //    p.admin_update(name, pricenm, quantitynm, admin_mobile);
                            break;

                        case 3:
                            Console.WriteLine("\nENTER NAME OF ITEM : ");
                           // name = Console.ReadLine();

                          //  p.admin_remove(name, admin_mobile);
                            break;

                        case 4:
                            Console.WriteLine("ITEM LIST\n");
                           // p.admin_display(admin_mobile);
                            break;
                        case 5: break;
                    }

                    Console.WriteLine("\n\nDO YOU WANT TO CONTINUE...\nENTER ' Y ' IF YES ");
                    ch = Console.ReadLine();
                }

            }


            Console.ReadLine();

        }







        static void admin_adding(long adminMobile)
        {
            
                
                Console.Write("Enter product name: ");
                string productName = Console.ReadLine();
                Console.Write("Enter product price: ");
                int productPrice = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter product quantity: ");
                int productQuantity = Convert.ToInt32(Console.ReadLine());

               
               
                SqlConnection connection = new SqlConnection(connectionString);

                
                try
                {
                    connection.Open();

                   
                    string sqlCommand = @"
                INSERT INTO product (pname, price, pquatity)
                VALUES (@ProductName, @ProductPrice, @ProductQuantity);";
                    SqlCommand command = new SqlCommand(sqlCommand, connection);
                    command.Parameters.AddWithValue("@ProductName", productName);
                    command.Parameters.AddWithValue("@ProductPrice", productPrice);
                    command.Parameters.AddWithValue("@ProductQuantity", productQuantity);

                  
                    int rowsAffected = command.ExecuteNonQuery();

                   
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Product added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Product addition failed.");
                    }
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine(ex.Message);
                }

                
                connection.Close();
            

        }





        static bool CheckCustPresence(long custMobile)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM customers WHERE cmobile = @custMobile";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@custMobile", custMobile); 

                    SqlDataReader reader = command.ExecuteReader();

                    bool isPresent = reader.Read();

                    reader.Close();

                    return isPresent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error checking customer presence: " + ex.Message);
                return false;
            }
        }



        static bool CheckAdminPresence(long adminMobile)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM admin WHERE amobile = @adminMobile";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@adminMobile", adminMobile);

                    SqlDataReader reader = command.ExecuteReader();

                    bool isPresent = reader.Read();

                    reader.Close();

                    return isPresent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error checking customer presence: " + ex.Message);
                return false;
            }
        }





        static void InsertCustomer(long custMobile, string custName)
        {

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

               
                string sqlCommand = @"
                INSERT INTO customers (cmobile, cname)
                VALUES (@custMobile, @custName);";
                SqlCommand command = new SqlCommand(sqlCommand, connection);
                command.Parameters.AddWithValue("@custMobile", custMobile);
                command.Parameters.AddWithValue("@custName", custName);

               
                int rowsAffected = command.ExecuteNonQuery();

              
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Customer inserted successfully.");
                }
                else
                {
                    Console.WriteLine("Customer insertion failed.");
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }

           
            connection.Close();
        


        
        }



        static void InsertAdmin(long adminMobile, string adminName)
        {

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();


                string sqlCommand = @"
                INSERT INTO admin (amobile, aname)
                VALUES (@adminMobile, @adminName);";
                SqlCommand command = new SqlCommand(sqlCommand, connection);
                command.Parameters.AddWithValue("@adminMobile", adminMobile);
                command.Parameters.AddWithValue("@adminName", adminName);


                int rowsAffected = command.ExecuteNonQuery();


                if (rowsAffected > 0)
                {
                    Console.WriteLine("Admin inserted successfully.");
                }
                else
                {
                    Console.WriteLine("Admin insertion failed.");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            connection.Close();




        }



    }
}
