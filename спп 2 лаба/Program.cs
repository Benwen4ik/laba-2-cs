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
                bool bl = true;
                int a;
                try
                {
                    connection.Open();
                    while (bl == true)
                    {
                        switch (option_table())
                        {
                            case 1:
                                {

                                    switch_users(connection);
                                    break;
                                }
                            case 2:
                                {
                                    switch_items(connection);
                                    break;
                                }
                            case 3:
                                {
                                    bl = false;
                                    break;
                                }
                            case 4:
                                {
                                    getTables(connection);
                                    break;
                                }
                            default: { Console.WriteLine("Ошибка ввода меню"); break; }
                        }
                        Console.WriteLine("Для продолжения нажмите любую кнопку");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    connection.Close();
                    Console.WriteLine("Подлючение закрыто");
                }
                catch (InvalidOperationException exp)
                {
                    Console.WriteLine("Ошибка объекта: " + exp.Message);
                    Console.ReadKey();
                    Console.Clear();
                }
                catch (OleDbException ex)
                {
                    Console.WriteLine("Ошибка источника данных: " + ex.Message);
                    Console.ReadKey();
                    Console.Clear();
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Ошибка формата: " + e.Message);
                    Console.ReadKey();
                    Console.Clear();
                }
                catch (Exception er)
                {
                    Console.WriteLine("Ошибка: " + er.Message);
                    Console.ReadKey();
                    Console.Clear();
                }

            }
        }

        static int menu_users()
        {
            Console.Clear();
            Console.WriteLine("Выбрана таблица Users. Выберете функцию");
            Console.WriteLine("1) Вывести всех пользователей");
            Console.WriteLine("2) Найти пользователя по id");
            Console.WriteLine("3) Найти пользователя по name");
            Console.WriteLine("4) Добавить пользователя");
            Console.WriteLine("5) Удалить пользователя по id");
            Console.WriteLine("6) Вернуться к выбору таблиц");
            int a  = Convert.ToInt32( Console.ReadLine());
            return a;
        }

        static int menu_items()
        {
            Console.Clear();
            Console.WriteLine("Выбрана таблица Items. Выберете функцию");
            Console.WriteLine("1) Вывести все товары");
            Console.WriteLine("2) Найти товары по id");
            Console.WriteLine("3) Найти товары по name");
            Console.WriteLine("4) Найти товары пользователя по id");
            Console.WriteLine("5) Добавить товар");
            Console.WriteLine("6) Удалить  товар");
            Console.WriteLine("7) Вернуться к выбору таблиц");
            int a = Convert.ToInt32(Console.ReadLine());
            return a;
        }

        static int option_table()
        {
            Console.WriteLine("Выберете таблицу для работы");
            Console.WriteLine("1) Users");
            Console.WriteLine("2) Items");
            Console.WriteLine("3) Выход c программы");
            int a = Convert.ToInt32(Console.ReadLine());
            return a;
        }

        static void switch_users( OleDbConnection connection)
        {
            bool bl = true;
            while (bl == true)
            {
                switch (menu_users())
                {
                    case 1:
                        {
                            Console.Clear();
                           // Console.WriteLine("В"); 
                            SelectAllUsers(connection); break; }
                    case 2:
                        {
                            Console.Clear();
                            Console.WriteLine("Введите id пользователя:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            SelectUsersByID(connection, id);
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();
                            Console.WriteLine("Введите имя пользователя:");
                            String name = Console.ReadLine();
                            SelectUsersByName(connection, name);
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();
                            Console.WriteLine("Введите имя:");
                            String name = Console.ReadLine();
                            if (name.Length > 20)
                            {
                                Console.WriteLine("Длина имени должна быть не более 20 символов");
                                break;
                            }
                            Console.WriteLine("Введите фамилию:");
                            String surname = Console.ReadLine();
                            if (surname.Length > 20)
                            {
                                Console.WriteLine("Длина имени должна быть не более 20 символов");
                                break;
                            }
                            Console.WriteLine("Введите телефон:");
                            // long phone = Convert.ToInt64(Console.ReadLine());
                            String phone = Console.ReadLine();
                            if (phone.Length > 16)
                            {
                                Console.WriteLine("Длина телефона должна быть не более 16 символов");
                                break;
                            }
                            Console.WriteLine("Введите email:");
                            String email = Console.ReadLine();
                            if (email.Length > 30)
                            {
                                Console.WriteLine("Длина email должна быть не более 30 символов");
                                break;
                            }
                            if (!email.Contains('@'))
                            {
                                Console.WriteLine("Введен неккоректный email(отсуствует @)");
                                break;
                            }
                            if (!email.EndsWith(".ru") && !email.EndsWith(".by") && !email.EndsWith(".org"))
                            {
                                Console.WriteLine("Введен неккоректный email(отсуствует .ru, .by, .org)");
                                break;
                            }
                            InsertUser(connection, name, surname, phone, email);
                            break;
                        }
                    case 5:
                        {
                            Console.Clear();
                            Console.WriteLine("Введите id для удаления:");
                            DeleteUserByID(connection, Convert.ToInt32(Console.ReadLine()));
                            break;
                        }
                    case 6: { bl = false; return; break; }
                    default: { Console.WriteLine("Ошибка ввода меню"); break; }
                }
                Console.WriteLine("Для продолжения нажмите любую кнопку");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void switch_items(OleDbConnection connection)
        {
            bool bl = true;
            while(bl == true)
            {
                switch (menu_items())
                {
                    case 1:
                        {
                            Console.Clear();
                            SelectAllItems(connection);
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            Console.WriteLine("Введите id предмета:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            SelectItemsByID(connection, id);
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();
                            Console.WriteLine("Введите имя предмета:");
                            String name = Console.ReadLine();
                            SelectItemsByName(connection, name);
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();
                            Console.WriteLine("Введите id пользователя:");
                            int id = Convert.ToInt32(Console.ReadLine());
                            SelectItemsByUser(connection, id);
                            break;
                        }
                    case 5:
                        {
                            Console.Clear();
                            Console.WriteLine("Введите предмет: ");
                            String name = Console.ReadLine();
                            Console.WriteLine("Введите количество: ");
                            int count = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите  id пользователя: ");
                            int id_u = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();
                            InsertItems(connection, name, count, id_u);
                            break;
                        }
                    case 6:
                        {
                            Console.Clear();
                            Console.WriteLine("Введите id для удаления:");
                            DeleteItemsByID(connection, Convert.ToInt32(Console.ReadLine()));
                            break;
                        }
                    case 7: { bl = false; return; break; }
                    default: { Console.WriteLine("Ошибка ввода меню"); break; }
                }
                Console.WriteLine("Для продолжения нажмите любую кнопку");
                Console.ReadKey();
                Console.Clear();
            }

        }

        static DataTable createDataTable(string tableName, OleDbCommand myOleDbCommand)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = myOleDbCommand;
            DataSet myDataset = new DataSet();
            adapter.Fill(myDataset, tableName);
            return myDataset.Tables[tableName];
        }


        static void SelectAllUsers(OleDbConnection connection)
        {
            // создаем объект OleDbCommand
            OleDbCommand myOleDbCommand = connection.CreateCommand();
            myOleDbCommand.CommandText =
                    "SELECT id , name_user, Surname, phone, email " +
                        "FROM Users";
            SelectRow(createDataTable("Users", myOleDbCommand));
        }
        static void SelectUsersByID(OleDbConnection connection, int id)
        {
            SelectRow(SearchUserById(connection,id));
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
            SelectRow(createDataTable("Users", myOleDbCommand));
        }

        static void InsertUser(OleDbConnection connection, String name, String surname,String phone, String email)
        {
            OleDbCommand myOleDbCommand = connection.CreateCommand();
            myOleDbCommand.CommandText =
                    @"INSERT INTO Users (name_user,surname, phone,email) VALUES (
                      @name, @surname, @phone, @email);";
            myOleDbCommand.Parameters.AddWithValue("@name", name);
            myOleDbCommand.Parameters.AddWithValue("@surname", surname);
            myOleDbCommand.Parameters.AddWithValue("@phone", phone);
            myOleDbCommand.Parameters.AddWithValue("@email", email);
            myOleDbCommand.ExecuteNonQuery();
            Console.WriteLine("Пользователь успешно добавлен");
        }

        static void DeleteUserByID(OleDbConnection connection, int id)
        {
            if (SearchUserById(connection, id).Rows.Count == 0)
            {
                Console.WriteLine("Ошибка. Пользователя с id=" + id + " не найдено");
                return;
            }
            OleDbCommand myOleDbCommand = connection.CreateCommand();
            myOleDbCommand.CommandText = "DELETE FROM Users WHERE id=" + id;
            myOleDbCommand.ExecuteNonQuery();
            if (SearchItemsByUser(connection,id).Rows.Count != 0)
            {
                foreach (DataRow row in SearchItemsByUser(connection, id).Rows)
                {
                    DeleteItemsByID(connection, Convert.ToInt32(row["id"]));
                }
            }
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
            Console.WriteLine("----+------------------------+------------------------+-----------------+--------------");
            foreach (DataRow dr in myDataTable.Rows)
            {
                Console.WriteLine("{0,-4}|{1,-24}|{2,-24}|{3,-17}|{4,-7}", dr["id"], dr["name_user"], dr["surname"], dr["phone"], dr["email"]);
            }
        }

        static void SelectRowItems(DataTable myDataTable)
        {
            if (myDataTable.Rows.Count == 0)
            {
                Console.WriteLine("Ничего не найдено");
                return;
            }
            Console.WriteLine("Id  |" + "Item \t\t     |" + "Count \t\t      |" + "ID_User \t");
            Console.WriteLine("----+------------------------+------------------------+----------------");
            foreach (DataRow dr in myDataTable.Rows)
            {
                Console.WriteLine("{0,-4}|{1,-24}|{2,-24}|{3,-17}", dr["id"], dr["item"], dr["count"], dr["id_user"]);
            }
        }


        static void SelectAllItems(OleDbConnection connection)
        {
            // создаем объект OleDbCommand
            OleDbCommand myOleDbCommand = connection.CreateCommand();
            myOleDbCommand.CommandText =
                    "SELECT id , item, count, id_user " +
                        "FROM items";
            SelectRowItems(createDataTable("Items", myOleDbCommand));
        }

        static void SelectItemsByID(OleDbConnection connection, int id)
        {
            // создаем объект OleDbCommand
            OleDbCommand myOleDbCommand = connection.CreateCommand();
            myOleDbCommand.CommandText =
                    "SELECT id , item, count, id_user " +
                        "FROM Items " +
                        "Where ID =" + id;
            SelectRowItems(createDataTable("Items", myOleDbCommand));
        }

        static void SelectItemsByName(OleDbConnection connection, String name)
        {
            // создаем объект OleDbCommand
            OleDbCommand myOleDbCommand = connection.CreateCommand();
            myOleDbCommand.CommandText =
                    "SELECT id , item, count, id_user " +
                        "FROM Items " +
                        "Where item =@name ;";
            myOleDbCommand.Parameters.AddWithValue("@name", name);
            SelectRowItems(createDataTable("Items", myOleDbCommand));
        }

        static void SelectItemsByUser(OleDbConnection connection, int id)
        {
            if (SearchUserById(connection, id).Rows.Count == 0)
            {
                Console.WriteLine("Ошибка. Пользователя с id=" + id + " не найдено");
                return;
            }
            OleDbCommand myOleDbCommand = connection.CreateCommand();
            myOleDbCommand.CommandText =
                    "SELECT id , item, count, id_user " +
                        "FROM Items " +
                        "Where id_user =@id ;";
            myOleDbCommand.Parameters.AddWithValue("@id", id);
            SelectRowItems(createDataTable("Items", myOleDbCommand));
        }


        static void InsertItems(OleDbConnection connection, String item, int count, int id_u)
        {
            if (SearchUserById(connection,id_u).Rows.Count == 0)
            {
                Console.WriteLine("Ошибка. Пользователя с id=" + id_u + " не найдено" );
                return;
            }
            OleDbCommand myOleDbCommand = connection.CreateCommand();
            myOleDbCommand.CommandText =
                    @" INSERT INTO Items ( [item] , [count] , [id_user] ) VALUES (
                     @item, @count , @id ) ";
            myOleDbCommand.Parameters.AddWithValue("@item", item);
            myOleDbCommand.Parameters.AddWithValue("@count", count);
            myOleDbCommand.Parameters.AddWithValue("@id", id_u);
            myOleDbCommand.ExecuteNonQuery();
            Console.WriteLine("Предмет успешно добавлен для пользователя " + id_u);
        }

        static void DeleteItemsByID(OleDbConnection connection, int id)
        {
            if (SearchItemsById(connection, id).Rows.Count == 0)
            {
                Console.WriteLine("Ошибка. Предмета с id=" + id + " не найдено");
                return;
            }
            OleDbCommand myOleDbCommand = connection.CreateCommand();
            myOleDbCommand.CommandText = "DELETE FROM Items WHERE id=" + id;
            myOleDbCommand.ExecuteNonQuery();
            Console.WriteLine("Предмет успешно удален");
        }

        static DataTable SearchUserById(OleDbConnection connection, int id)
        {
            OleDbCommand Command = connection.CreateCommand();
            Command.CommandText =
                    "SELECT id , name_user, Surname, phone, email " +
                        "FROM Users " +
                        "Where id =" + id;
            return createDataTable("Users", Command);
        }

        static DataTable SearchItemsById(OleDbConnection connection, int id)
        {
            OleDbCommand Command = connection.CreateCommand();
            Command.CommandText =
                    "SELECT id , item, count, id_user " +
                        "FROM Items " +
                        "Where ID =" + id;
            return createDataTable("Items", Command);
        }

        static DataTable SearchItemsByUser(OleDbConnection connection, int id)
        {
            OleDbCommand Command = connection.CreateCommand();
            Command.CommandText =
                    "SELECT id , item, count, id_user " +
                        "FROM Items " +
                        "Where id_user =" + id;
            return createDataTable("Items", Command);
        }

        static void getTables(OleDbConnection connection)
        {
            DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            foreach (DataRow item in dt.Rows)
            {
                Console.WriteLine((string)item["TABLE_NAME"]);
            }
        }

    }
}
