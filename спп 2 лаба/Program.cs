using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace спп_2_лаба
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\учеба\\6 сем\\ЭРУД ССП\\Database.accdb;";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection opened successfully.");
                    bool bl = true;
                    int a;
                    while(bl == true)
                    {
                        a = menu();
                        switch (a)
                        {
                            case 1: { SelectAllUsers(connection); break; }
                            case 2:
                                {
                                    Console.WriteLine("Enter id:");
                                    int id = Convert.ToInt32(Console.ReadLine());
                                    SelectUsersByID(connection, id );
                                    break;
                                }
                            case 3:
                                {
                                    Console.WriteLine("Enter name:");
                                    String name = Console.ReadLine();
                                    SelectUsersByName(connection, name);
                                    break;
                                }
                            case 4:
                                {
                                    Console.WriteLine("Enter name:");
                                    String name = Console.ReadLine();
                                    Console.WriteLine("Enter surname:");
                                    String surname = Console.ReadLine();
                                    Console.WriteLine("Enter phone:");
                                    int phone = Convert.ToInt32(Console.ReadLine());
                                    InsertUser(connection, name, surname, phone);
                                    break;
                                }
                            case 5:
                                {
                                    Console.WriteLine("Enter delete user id:");
                                    DeleteUserByID(connection, Convert.ToInt32(Console.ReadLine()) );
                                    break;
                                }
                            case 6: { bl = false; break; }
                            default: { Console.WriteLine("Error input"); break; }
                        }
                      //  Console.WriteLine("Enter key");
                        Console.ReadKey();
                        Console.Clear();
                    }
                   // DeleteUserByID(connection, 4);
                    //InsertUser(connection, "dimka", "popov", 228);
                    //SelectAllUsers(connection);
                   // SelectUsersByID(connection, 4);
                   // SelectUsersByName(connection, "dimon");
                   // string s = Console.ReadLine();

                    connection.Close();
                    Console.WriteLine("Connection closed.");
                }
                catch (OleDbException ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    Console.ReadKey();
                }
            }
        }

        static int menu()
        {
            Console.WriteLine("1) Select all users");
            Console.WriteLine("2) Select user by id");
            Console.WriteLine("3) Select user by name");
            Console.WriteLine("4) insert user");
            Console.WriteLine("5) delete user by id");
            Console.WriteLine("6) exit");
            int a  = Convert.ToInt32( Console.ReadLine());
            return a;
        }


        static void SelectAllUsers(OleDbConnection connection)
        {
            // создаем объект OleDbCommand
            OleDbCommand myOleDbCommand = connection.CreateCommand();
            myOleDbCommand.CommandText =
                    "SELECT id , name_user, Surname, phone, date_create " +
                        "FROM Users ";
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = myOleDbCommand;
            DataSet myDataset = new DataSet();
            adapter.Fill(myDataset, "Users");
            DataTable myDataTable = myDataset.Tables["Users"];
            foreach (DataRow dr in myDataTable.Rows)
            { 
                Console.WriteLine("Name=" + dr["name_user"] + "   " + "surname=" + dr["surname"] +
                   "   " + "phone=" + dr["phone"] +
               "   " + "date=" + dr["date_create"] +
               "   " + "id=" + dr["id"]);

            }

        }
        static void SelectUsersByID(OleDbConnection connection, int id)
        {
            // создаем объект OleDbCommand
            OleDbCommand myOleDbCommand = connection.CreateCommand();
            myOleDbCommand.CommandText =
                    "SELECT id , name_user, Surname, phone, date_create " +
                        "FROM Users " +
                        "Where id =" + id;
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = myOleDbCommand;
            DataSet myDataset = new DataSet();
            adapter.Fill(myDataset, "Users");
            DataTable myDataTable = myDataset.Tables["Users"];
            if (myDataTable.Rows.Count ==0)
            {
                Console.WriteLine("Not found user");
                return;
            }
            foreach (DataRow dr in myDataTable.Rows)
            { 
                Console.WriteLine("Name=" + dr["name_user"] + "   " + "surname=" + dr["surname"] +
                   "   " + "phone=" + dr["phone"] +
               "   " + "date=" + dr["date_create"] +
               "   " + "id=" + dr["id"]);

            }
        }

        static void SelectUsersByName(OleDbConnection connection, String name)
        {
            // создаем объект OleDbCommand
            OleDbCommand myOleDbCommand = connection.CreateCommand();
            myOleDbCommand.CommandText =
                    "SELECT id , name_user, Surname, phone, date_create " +
                        "FROM Users " +
                        "Where name_user =@name ;" ;
            myOleDbCommand.Parameters.AddWithValue("@name", name);
            // создаем объект OleDbDataReader и вызываем метод ExecuteReader() для выполнения введенного SQL-запроса
         //   OleDbDataReader dr = myOleDbCommand.ExecuteReader();
            // Читаем первую (в нашем случае - и единственную) строку ответа базы данных с помощью метода Read() объекта OleDbDataReader
          //  dr.Read();
             OleDbDataAdapter adapter = new OleDbDataAdapter();
             adapter.SelectCommand = myOleDbCommand;
             DataSet myDataset = new DataSet();
             adapter.Fill(myDataset, "Users");
             DataTable myDataTable = myDataset.Tables["Users"];
            if (myDataTable.Rows.Count == 0)
            {
                Console.WriteLine("Not found user");
                return;
            }
            foreach (DataRow dr in myDataTable.Rows)
            {
                Console.WriteLine("Name=" + dr["name_user"] + "   " + "surname=" + dr["surname"] +
                   "   " + "phone=" + dr["phone"] +
               "   " + "date=" + dr["date_create"] +
               "   " + "id=" + dr["id"]);

            }
        }

        static void InsertUser(OleDbConnection connection, String name, String surname, int phone)
        {
            OleDbCommand myOleDbCommand = connection.CreateCommand();
            myOleDbCommand.CommandText =
                    @"INSERT INTO Users (name_user,surname, phone) VALUES (
                      @name, @surname, @phone)";
            myOleDbCommand.Parameters.AddWithValue("@name", name);
            myOleDbCommand.Parameters.AddWithValue("@surname", surname);
            myOleDbCommand.Parameters.AddWithValue("@phone", phone);
            myOleDbCommand.ExecuteNonQuery();
            Console.WriteLine("User add");
        }

        static void DeleteUserByID(OleDbConnection connection, int id)
        {
            OleDbCommand myOleDbCommand = connection.CreateCommand();
            myOleDbCommand.CommandText = "DELETE FROM Users WHERE id=" + id;
            myOleDbCommand.ExecuteNonQuery();
            Console.WriteLine("User delete");
        }

    }
}
