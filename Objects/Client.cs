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

    public void SetId(int newId)
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

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.GetId() == newClient.GetId());
        bool nameEquality = (this.GetName() == newClient.GetName());
        bool requestEquality = (this.GetRequest() == newClient.GetRequest());
        bool dateEquality = (this.GetDate() == newClient.GetDate());
        bool stylistIdEquality = (this.GetStylistId() == newClient.GetStylistId());
        return (idEquality && nameEquality && requestEquality && dateEquality && stylistIdEquality);
      }
    }

      public static List<Client> GetAll()
       {
         List<Client> allClients = new List<Client>{};

         SqlConnection conn = DB.Connection();
         conn.Open();

         SqlCommand cmd = new SqlCommand("SELECT * FROM clients ORDER BY cast([date] as datetime) asc;", conn);
         SqlDataReader rdr = cmd.ExecuteReader();

         while(rdr.Read())
         {
           int id = rdr.GetInt32(0);
           string name = rdr.GetString(1);
           string request = rdr.GetString(2);
           DateTime date = rdr.GetDateTime(3);
           int stylistId = rdr.GetInt32(4);
           Client newClient = new Client(name, request, date, stylistId, id);
           allClients.Add(newClient);
         }

         if (rdr != null)
         {
           rdr.Close();
         }
         if (conn != null)
         {
           conn.Close();
         }

         return allClients;
       }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
