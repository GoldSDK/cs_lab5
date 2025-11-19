using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    static Museum museum = new Museum();

    static void Main()
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;
        museum.LoadData("exhibits.csv", "visitors.csv", "tickets.csv");

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== управление музеем ===");
            Console.WriteLine("1. просмотр базы данных");
            Console.WriteLine("2. удаление элементов");
            Console.WriteLine("3. добавление элементов");
            Console.WriteLine("4. выполнение запросов");
            Console.WriteLine("5. сохранение изменений");
            Console.WriteLine("6. выход");
            Console.Write("выбери действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewDataMenu();
                    break;
                case "2":
                    DeleteMenu();
                    break;
                case "3":
                    AddMenu();
                    break;
                case "4":
                    QueriesMenu();
                    break;
                case "5":
                    SaveData();
                    break;
                case "6":
                    Console.WriteLine("выход из программы...");
                    return;
                default:
                    Console.WriteLine("неверный выбор!!!!!!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void ViewDataMenu()
    {
        Console.Clear();
        museum.ViewAllData();
        Console.WriteLine("\nнажми любую клавишу для продолжения...");
        Console.ReadKey();
    }

    static void DeleteMenu()
    {
        Console.Clear();
        Console.WriteLine("=== удаление элементов ===");
        Console.WriteLine("1. удалить экспонат");
        Console.WriteLine("2. удалить посетителя");
        Console.WriteLine("3. удалить билет");
        Console.Write("выбери действие: ");

        string choice = Console.ReadLine();
        Console.Write("введи id для удаления: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            bool success = false;
            switch (choice)
            {
                case "1":
                    success = museum.DeleteExhibit(id);
                    break;
                case "2":
                    success = museum.DeleteVisitor(id);
                    break;
                case "3":
                    success = museum.DeleteTicket(id);
                    break;
                default:
                    Console.WriteLine("неверный выбор!!!!!");
                    break;
            }

            if (success)
                Console.WriteLine("удаление успешно");
            else
                Console.WriteLine("нету элемента с таким id");
        }
        else
        {
            Console.WriteLine("неверный формат id");
        }

        Console.WriteLine("нажми любую клавишу для продолжения...");
        Console.ReadKey();
    }

    static void AddMenu()
    {
        Console.Clear();
        Console.WriteLine("=== добавление элементов ===");
        Console.WriteLine("1. добавить экспонат");
        Console.WriteLine("2. добавить посетителя");
        Console.WriteLine("3. добавить билет");
        Console.Write("выбери действие: ");

        string choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    Console.Write("ID: ");
                    int id1 = int.Parse(Console.ReadLine());
                    Console.Write("название: ");
                    string name = Console.ReadLine();
                    Console.Write("эпоха: ");
                    string era = Console.ReadLine();
                    museum.AddExhibit(id1, name, era);
                    break;

                case "2":
                    Console.Write("ID: ");
                    int id2 = int.Parse(Console.ReadLine());
                    Console.Write("имя: ");
                    string visitorName = Console.ReadLine();
                    Console.Write("возраст: ");
                    int age = int.Parse(Console.ReadLine());
                    Console.Write("пол: ");
                    string gender = Console.ReadLine();
                    museum.AddVisitor(id2, visitorName, age, gender);
                    break;

                case "3":
                    Console.Write("ID: ");
                    int id3 = int.Parse(Console.ReadLine());
                    Console.Write("ID экспоната: ");
                    int exhibitId = int.Parse(Console.ReadLine());
                    Console.Write("ID посетителя: ");
                    int visitorId = int.Parse(Console.ReadLine());
                    Console.Write("дата (гггг-мм-дд): ");
                    DateTime date = DateTime.Parse(Console.ReadLine());
                    Console.Write("стоимость: ");
                    decimal cost = decimal.Parse(Console.ReadLine());
                    museum.AddTicket(id3, exhibitId, visitorId, date, cost);
                    break;

                default:
                    Console.WriteLine("неверный выбор!!!!");
                    break;
            }
            Console.WriteLine("добавление успешно");
        }
        catch (Exception ex)
        {
            Console.WriteLine("ошибка при добавлении: " + ex.Message);
        }

        Console.WriteLine("нажми любую клавишу для продолжения...");
        Console.ReadKey();
    }

    static void QueriesMenu()
    {
        Console.Clear();
        Console.WriteLine("=== выполнение запросов ===");
        Console.WriteLine("1. вывести экспонаты определенной эпохи");
        Console.WriteLine("2. найти выручку по эпохе и месяцу");
        Console.WriteLine("3. найти всех посетителей по эпохе");
        Console.WriteLine("4. найти количество уникальных посетителей по эпохе");
        Console.Write("выбери запрос: ");

        string choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    Console.Write("введи эпоху: ");
                    string era1 = Console.ReadLine();
                    var exhibits = museum.ExhibitsEra(era1);
                    Console.WriteLine("\nрезультат:");
                    foreach (var exhibit in exhibits)
                    {
                        Console.WriteLine(exhibit);
                    }
                    break;

                case "2":
                    Console.Write("введи эпоху: ");
                    string era2 = Console.ReadLine();
                    Console.Write("введи год: ");
                    int year = int.Parse(Console.ReadLine());
                    Console.Write("введи месяц: ");
                    int month = int.Parse(Console.ReadLine());
                    decimal revenue = museum.Revenue(era2, year, month);
                    Console.WriteLine("\nвыручка: " + revenue);
                    break;

                case "3":
                    Console.Write("введи эпоху: ");
                    string era3 = Console.ReadLine();
                    var visitors = museum.VisitorsEra(era3);
                    Console.WriteLine("\nпосетители:");
                    foreach (var visitor in visitors)
                    {
                        Console.WriteLine(visitor);
                    }
                    break;

                case "4":
                    Console.Write("введи эпоху: ");
                    string era4 = Console.ReadLine();
                    int count = museum.VisitorsCountEra(era4);
                    Console.WriteLine("\nколичество уникальных посетителей: " + count);
                    break;

                default:
                    Console.WriteLine("неверный выбор!!!!!");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("ошибка при выполнении запроса: " + ex.Message);
        }

        Console.WriteLine("\nнажми любую клавишу для продолжения...");
        Console.ReadKey();
    }

    static void SaveData()
    {
        museum.SaveData("exhibits.csv", "visitors.csv", "tickets.csv");
        Console.WriteLine("данные сохранены");
        Console.WriteLine("нажми любую клавишу для продолжения...");
        Console.ReadKey();
    }
}