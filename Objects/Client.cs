using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon
{
  public class Client
  {
    private int _id;
    private string _name;
    private string _request;
    private DateTime _date;
    private int _stylistId;

    public Client(string Name, string Request, DateTime Date, int StylistId, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _request = Request;
      _date = Date;
      _stylistId = StylistId;
    }

    public int GetId()
    {
      return _id;
    }

    public int SetId(int newId)
    {
      _id = newId;
    }

    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }

    public string GetRequest()
    {
      return _request;
    }
    public void SetRequest(string newRequest)
    {
      _request = newRequest;
    }

    public DateTime GetDate()
    {
      return _date;
    }
    public void SetDate(DateTime newDate)
    {
      _date = newDate;
    }

    public int GetStylistId()
    {
      return _stylistId;
    }
    public void SetStylistId(int newStylistId)
    {
      _stylistId = newStylistId;
    }


  }
}
