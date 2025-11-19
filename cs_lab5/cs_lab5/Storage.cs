using System;
using System.Collections.Generic;

public class Exhibit
{
    public int ID
    {
        get;
        set;
    }
    public string Name
    {
        get;
        set;
    }
    public string Era
    { 
        get;
        set;
    }

    public Exhibit(int id, string name, string era)
    {
        ID = id;
        Name = name;
        Era = era;
    }

    public override string ToString()
    {
        return string.Format("ID: {0}, Название: {1}, Эпоха: {2}", ID, Name, Era);
    }
}

public class Visitor
{
    public int ID
    {
        get;
        set;
    }
    public string Name
    {
        get;
        set;
    }
    public int Age
    {
        get;
        set;
    }
    public string Gender
    {
        get;
        set;
    }

    public Visitor(int id, string name, int age, string gender)
    {
        ID = id;
        Name = name;
        Age = age;
        Gender = gender;
    }

    public override string ToString()
    {
        return string.Format("ID: {0}, Имя: {1}, Возраст: {2}, Пол: {3}", ID, Name, Age, Gender);
    }
}

public class Ticket
{
    public int ID
    {
        get;
        set;
    }
    public int ExhibitID
    {
        get;
        set;
    }
    public int VisitorID
    {
        get;
        set;
    }
    public DateTime Date
    {
        get;
        set;
    }
    public decimal Cost
    {
        get;
        set;
    }

    public Ticket(int id, int exhibitId, int visitorId, DateTime date, decimal cost)
    {
        ID = id;
        ExhibitID = exhibitId;
        VisitorID = visitorId;
        Date = date;
        Cost = cost;
    }

    public override string ToString()
    {
        return string.Format("ID: {0}, ID экспоната: {1}, ID посетителя: {2}, Дата: {3}, Стоимость: {4}", ID, ExhibitID, VisitorID, Date.ToString("dd.MM.yyyy"), Cost);
    }
}