using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
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
                    bool bl = true;
                    int a;
                    while (bl == true)
                    {
                        a = menu();
                        switch (a)
                        {
                            case 1: { SelectAllUsers(connection); break; }
                            case 2:
                                {
                                    Console.WriteLine("Введите id пользователя:");
                                    int id = Convert.ToInt32(Console.ReadLine());
                                    SelectUsersByID(connection, id);
                                    break;
                                }
                            case 3:
                                {
                                    Console.WriteLine("Введите имя пользователя:");
                                    String name = Console.ReadLine();
                                    SelectUsersByName(connection, name);
                                    break;
                                }
                            case 4:
                                {
                                    Console.WriteLine("Введите имя:");
                                    String name = Console.ReadLine();
                                    Console.WriteLine("Введите фамилию:");
                                    String surname = Console.ReadLine();
                                    Console.WriteLine("Введите телефон:");
                                    long phone = Convert.ToInt64(Console.ReadLine());
                                    Console.WriteLine("Введите email:");
                                    String email = Console.ReadLine();
                                    if (!email.Contains('@'))
                                    {
                                        Console.WriteLine("Введен неккоректный email(отсуствует @)");
                                        break;
                                    }
                                    InsertUser(connection, name, surname, phone,email);
                                    break;
                                }
                            case 5:
                                {
                                    Console.WriteLine("Введите id для удаления:");
                                    DeleteUserByID(connection, Convert.ToInt32(Console.ReadLine()));
                                    break;
                                }
                            case 6: { bl = false; break; }
                            default: { Console.WriteLine("Ошибка ввода меню"); break; }
                        }
                        Console.WriteLine("Для продолжения нажмите любую кнопку");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    connection.Close();
                    Console.WriteLine("Подлючение закрыто");
                }
                catch (OleDbException ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                    Console.ReadKey();
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Ошибка: " + e.Message);
                    Console.ReadKey();
                }
            }
        }

        static int menu()
        {
            Console.WriteLine("1) Вывести всех пользователей");
            Console.WriteLine("2) Найти пользователя по id");
            Console.WriteLine("3) Найти пользователя по name");
            Console.WriteLine("4) Добавить пользователя");
            Console.WriteLine("5) Удалить пользователя по id");
            Console.WriteLine("6) Выход");
            int a  = Convert.ToInt32( Console.ReadLine());
            return a;
        }


        static void SelectAllUsers(OleDbConnection connection)
        {
            // создаем объект OleDbCommand
            OleDbCommand myOleDbCommand = connection.CreateCommand();
            myOleDbCommand.CommandText =
                    "SELECT id , name_user, Surname, phone, email " +
                        "FROM Users ";
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = myOleDbCommand;
            DataSet myDataset = new DataSet();
            adapter.Fill(myDataset, "Users");
            DataTable myDataTable = myDataset.Tables["Users"];
            SelectRow(myDataTable);
        }
        static void SelectUsersByID(OleDbConnection connection, int id)
        {
            // создаем объект OleDbCommand
            OleDbCommand myOleDbCommand = connection.CreateCommand();
            myOleDbCommand.CommandText =
                    "SELECT id , name_user, Surname, phone, email " +
                        "FROM Users " +
                        "Where id =" + id;
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = myOleDbCommand;
            DataSet myDataset = new DataSet();
            adapter.Fill(myDataset, "Users");
            DataTable myDataTable = myDataset.Tables["Users"];
            SelectRow(myDataTable);
        }

        static void SelectUsersByName(OleDbConnection connection, String name)
        {
            // создаем объект OleDbCommand
            OleDbCommand myOleDbCommand = connection.CreateCommand();
            myOleDbCommand.CommandText =
                    "SELECT id , name_user, Surname, phone, email " +
                        "FROM Users " +
                        "Where name_user =@name ;" ;
            myOleDbCommand.Parameters.AddWithValue("@name", name);
             OleDbDataAdapter adapter = new OleDbDataAdapter();
             adapter.SelectCommand = myOleDbCommand;
             DataSet myDataset = new DataSet();
             adapter.Fill(myDataset, "Users");
             DataTable myDataTable = myDataset.Tables["Users"];
            SelectRow(myDataTable);
        }

        static void InsertUser(OleDbConnection connection, String name, String surname,long phone, String email)
        {
            OleDbCommand myOleDbCommand = connection.CreateCommand();
            myOleDbCommand.CommandText =
                    @"INSERT INTO Users (name_user,surname, phone,email) VALUES (
                      @name, @surname, @phone, @email)";
            myOleDbCommand.Parameters.AddWithValue("@name", name);
            myOleDbCommand.Parameters.AddWithValue("@surname", surname);
            myOleDbCommand.Parameters.AddWithValue("@phone", phone);
            myOleDbCommand.Parameters.AddWithValue("@email", email);
            myOleDbCommand.ExecuteNonQuery();
            Console.WriteLine("Пользователь успешно добавлен");
        }

        static void DeleteUserByID(OleDbConnection connection, int id)
        {
            OleDbCommand myOleDbCommand = connection.CreateCommand();
            myOleDbCommand.CommandText = "DELETE FROM Users WHERE id=" + id;
            myOleDbCommand.ExecuteNonQuery();
            Console.WriteLine("Пользователь успешно удален");
        }

       

        static void SelectRow(DataTable myDataTable)
        {
            if (myDataTable.Rows.Count == 0)
            {
                Console.WriteLine("Пользователь не найден");
                return;
            }
            Console.WriteLine("Id  |" + "Name \t\t     |" + "Surname \t\t      |" + "Phone \t\t|" + "Email \t ");
            foreach (DataRow dr in myDataTable.Rows)
            {
                Console.WriteLine("{0,-5}{1,-25}{2,-25}{3,-18}{4,-8}", dr["id"], dr["name_user"], dr["surname"], dr["phone"], dr["email"]);
            }
        }

    }
}
