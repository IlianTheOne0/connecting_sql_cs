namespace ConsoleApplication;
using Data.Database.Commands;

class Program
{
    static async Task Main(string[] args)
    {
        int choice = 0;

        do
        {
            try
            {
                while (choice != 3)
                {
                    Console.WriteLine("1. Create Database");
                    Console.WriteLine("2. Drop Database");
                    Console.WriteLine("3. Create Table");
                    Console.WriteLine("4. Drop Table");
                    Console.WriteLine("5. List Tables");
                    Console.WriteLine("0. Exit");
                    Console.Write("Enter your choice: ");

                    if (int.TryParse(Console.ReadLine(), out choice))
                    {
                        switch (choice)
                        {
                            case 1: { await DatabaseCommands.CreateDatabaseIfNotExist(); } break;
                            case 2: { await DatabaseCommands.DropDatabaseIfExist(); } break;
                            case 3:
                                {
                                    string tableName;
                                    Console.Write("Enter the table name: ");
                                    tableName = Console.ReadLine();
                                    await DatabaseCommands.CreateTableIfNotExist(tableName);
                                }
                                break;
                            case 4:
                                {
                                    string tableName;
                                    Console.Write("Enter the table name to drop: ");
                                    tableName = Console.ReadLine();
                                    await DatabaseCommands.DropTableIfExist(tableName);
                                }
                                break;
                            case 5: { await DatabaseCommands.ListTables(); } break;
                            case 0: { Console.WriteLine("Exiting..."); } break;
                            default: { Console.WriteLine("Invalid choice, please try again."); } break;
                        }
                    }
                    else { Console.WriteLine("Invalid input, please enter a number."); }
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
        } while (true);
    }
}