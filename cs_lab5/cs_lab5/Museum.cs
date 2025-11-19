using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;

public class Museum
{
    public List<Exhibit> Exhibits
    {
        get;
        set;
    }
    public List<Visitor> Visitors
    {
        get;
        set;
    }
    public List<Ticket> Tickets
    {
        get;
        set;
    }

    public Museum()
    {
        Exhibits = new List<Exhibit>();
        Visitors = new List<Visitor>();
        Tickets = new List<Ticket>();
    }

    public void LoadData(string exhibitsFile, string visitorsFile, string ticketsFile)
    {
        try
        {
            if (File.Exists(exhibitsFile))
            {
                var lines = File.ReadAllLines(exhibitsFile);
                for (int i = 1; i < lines.Length; i++)
                {
                    var parts = lines[i].Split(',');
                    if (parts.Length >= 3)
                    {
                        Exhibits.Add(new Exhibit(
                            int.Parse(parts[0]),
                            parts[1],
                            parts[2]
                        ));
                    }
                }
            }

            if (File.Exists(visitorsFile))
            {
                var lines = File.ReadAllLines(visitorsFile);
                for (int i = 1; i < lines.Length; i++)
                {
                    var parts = lines[i].Split(',');
                    if (parts.Length >= 4)
                    {
                        Visitors.Add(new Visitor(
                            int.Parse(parts[0]),
                            parts[1],
                            int.Parse(parts[2]),
                            parts[3]
                        ));
                    }
                }
            }

            if (File.Exists(ticketsFile))
            {
                var lines = File.ReadAllLines(ticketsFile);
                for (int i = 1; i < lines.Length; i++)
                {
                    var parts = lines[i].Split(',');
                    if (parts.Length >= 5)
                    {
                        Tickets.Add(new Ticket(
                            int.Parse(parts[0]),
                            int.Parse(parts[1]),
                            int.Parse(parts[2]),
                            DateTime.Parse(parts[3]),
                            decimal.Parse(parts[4])
                        ));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("ошибка загрузки данных: " + ex.Message);
        }
    }

    public void SaveData(string exhibitsFile, string visitorsFile, string ticketsFile)
    {
        try
        {
            using (var writer = new StreamWriter(exhibitsFile))
            {
                writer.WriteLine("ID,Название,Эпоха");
                foreach (var exhibit in Exhibits)
                {
                    writer.WriteLine(string.Format("{0},{1},{2}", exhibit.ID, exhibit.Name, exhibit.Era));
                }
            }

            using (var writer = new StreamWriter(visitorsFile))
            {
                writer.WriteLine("ID,Имя,Возраст,Пол");
                foreach (var visitor in Visitors)
                {
                    writer.WriteLine(string.Format("{0},{1},{2},{3}", visitor.ID, visitor.Name, visitor.Age, visitor.Gender));
                }
            }

            using (var writer = new StreamWriter(ticketsFile))
            {
                writer.WriteLine("ID,ID_экспоната,ID_посетителя,Дата,Стоимость");
                foreach (var ticket in Tickets)
                {
                    writer.WriteLine(string.Format("{0},{1},{2},{3},{4}",
                        ticket.ID, ticket.ExhibitID, ticket.VisitorID, ticket.Date.ToString("yyyy-MM-dd"), ticket.Cost));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("ошибка сохранения данных: " + ex.Message);
        }
    }
    public void ViewAllData()
    {
        Console.WriteLine("\n=== экспонаты ===");
        foreach (var exhibit in Exhibits)
        {
            Console.WriteLine(exhibit);
        }

        Console.WriteLine("\n=== посетители ===");
        foreach (var visitor in Visitors)
        {
            Console.WriteLine(visitor);
        }

        Console.WriteLine("\n=== билеты ===");
        foreach (var ticket in Tickets)
        {
            Console.WriteLine(ticket);
        }
    }

    public void AddExhibit(int id, string name, string era)
    {
        Exhibits.Add(new Exhibit(id, name, era));
    }

    public void AddVisitor(int id, string name, int age, string gender)
    {
        Visitors.Add(new Visitor(id, name, age, gender));
    }

    public void AddTicket(int id, int exhibitId, int visitorId, DateTime date, decimal cost)
    {
        Tickets.Add(new Ticket(id, exhibitId, visitorId, date, cost));
    }

    public bool DeleteExhibit(int id)
    {
        var exhibit = Exhibits.FirstOrDefault(e => e.ID == id);
        if (exhibit != null)
        {
            Exhibits.Remove(exhibit);
            return true;
        }
        return false;
    }

    public bool DeleteVisitor(int id)
    {
        var visitor = Visitors.FirstOrDefault(v => v.ID == id);
        if (visitor != null)
        {
            Visitors.Remove(visitor);
            return true;
        }
        return false;
    }

    public bool DeleteTicket(int id)
    {
        var ticket = Tickets.FirstOrDefault(t => t.ID == id);
        if (ticket != null)
        {
            Tickets.Remove(ticket);
            return true;
        }
        return false;
    }

    public List<Exhibit> ExhibitsEra(string era)
    {
        var result = from exhibit in Exhibits
                     where exhibit.Era.Equals(era, StringComparison.OrdinalIgnoreCase)
                     select exhibit;
        return result.ToList();
    }

    public decimal Revenue(string era, int year, int month)
    {
        var result = from ticket in Tickets
                     join exhibit in Exhibits on ticket.ExhibitID equals exhibit.ID
                     where exhibit.Era.Equals(era, StringComparison.OrdinalIgnoreCase)
                     && ticket.Date.Year == year
                     && ticket.Date.Month == month
                     select ticket.Cost;
        return result.Sum();
    }

    public List<string> VisitorsEra(string era)
    {
        var result = from ticket in Tickets
                     join exhibit in Exhibits on ticket.ExhibitID equals exhibit.ID
                     join visitor in Visitors on ticket.VisitorID equals visitor.ID
                     where exhibit.Era.Equals(era, StringComparison.OrdinalIgnoreCase)
                     select visitor.Name;
        return result.Distinct().ToList();
    }

    public int VisitorsCountEra(string era)
    {
        var result = from ticket in Tickets
                     join exhibit in Exhibits on ticket.ExhibitID equals exhibit.ID
                     join visitor in Visitors on ticket.VisitorID equals visitor.ID
                     where exhibit.Era.Equals(era, StringComparison.OrdinalIgnoreCase)
                     select visitor.ID;
        return result.Distinct().Count();
    }
}