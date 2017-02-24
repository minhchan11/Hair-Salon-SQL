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

  }
}
