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

    //Object constructor
    public Client(string Name, string Request, DateTime Date, int StylistId, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _request = Request;
      _date = Date;
      _stylistId = StylistId;
    }


    //Getters and Setters for attributes
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

    //Override Equals for SQL
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

    //Save new instance to SQL
     public void Save()
     {
       SqlConnection conn = DB.Connection();
       conn.Open();

       SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, request, date, stylist_id) OUTPUT INSERTED.id VALUES (@ClientName, @ClientRequest, @ClientDate, @ClientStylistId);", conn);

       SqlParameter nameParameter = new SqlParameter();
       nameParameter.ParameterName = "@ClientName";
       nameParameter.Value = this.GetName();

       SqlParameter requestParameter = new SqlParameter();
       requestParameter.ParameterName = "@ClientRequest";
       requestParameter.Value = this.GetRequest();

       SqlParameter dateParameter = new SqlParameter();
       dateParameter.ParameterName = "@ClientDate";
       dateParameter.Value = this.GetDate();

       SqlParameter stylistIdParameter = new SqlParameter();
       stylistIdParameter.ParameterName = "@ClientStylistId";
       stylistIdParameter.Value = this.GetStylistId();

       cmd.Parameters.Add(nameParameter);
       cmd.Parameters.Add(requestParameter);
       cmd.Parameters.Add(dateParameter);
       cmd.Parameters.Add(stylistIdParameter);

       SqlDataReader rdr = cmd.ExecuteReader();

       while(rdr.Read())
       {
         this._id = rdr.GetInt32(0);
       }

       if (rdr != null)
       {
         rdr.Close();
       }
       if (conn != null)
       {
         conn.Close();
       }
     }

    //Output list from SQL
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


    //Find instance from SQL
    public static Client Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @ClientId;", conn);

      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@ClientId";
      idParameter.Value = id.ToString();
      cmd.Parameters.Add(idParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string foundName = null;
      string foundRequest = null;
      DateTime foundDate = new DateTime();
      int foundStylistId = 0;

      while (rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        foundName = rdr.GetString(1);
        foundRequest = rdr.GetString(2);
        foundDate = rdr.GetDateTime(3);
        foundStylistId = rdr.GetInt32(4);
      }

      Client foundClient = new Client(foundName,foundRequest,foundDate,foundStylistId, foundId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundClient;
    }

    public static List<Client> Search(string match)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE name = @ClientName;", conn);

      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@ClientName";
      idParameter.Value = match;
      cmd.Parameters.Add(idParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Client> clientMatches = new List<Client>{};

      while (rdr.Read())
      {
        int foundId = rdr.GetInt32(0);
        string foundName = rdr.GetString(1);
        string foundRequest = rdr.GetString(2);
        DateTime foundDate = rdr.GetDateTime(3);
        int foundStylistId = rdr.GetInt32(4);
        Client foundClient = new Client(foundName,foundRequest,foundDate,foundStylistId, foundId);
        clientMatches.Add(foundClient);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return clientMatches;
    }

      public List<Feedback> GetClientFeedback()
      {
        List<Feedback> allFeedbacks = new List<Feedback>{};

        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM feedbacks WHERE client_id = @ClientId;", conn);

        SqlParameter clientIdParameter = new SqlParameter();
        clientIdParameter.ParameterName = "@ClientId";
        clientIdParameter.Value = this.GetId().ToString();
        cmd.Parameters.Add(clientIdParameter);

        SqlDataReader rdr = cmd.ExecuteReader();

        while(rdr.Read())
        {
          int id = rdr.GetInt32(0);
          string preference = rdr.GetString(1);
          int clientId = rdr.GetInt32(2);
          Feedback newFeedback = new Feedback(preference, clientId, id);
          allFeedbacks.Add(newFeedback);
        }

        if (rdr != null)
        {
          rdr.Close();
        }
        if (conn != null)
        {
          conn.Close();
        }
        return allFeedbacks;
      }

    //Update name method for each client
    public void UpdateName(string newName)
    {
      if (newName != "")
      {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE clients SET name = @NewName OUTPUT INSERTED.name WHERE id=@ClientId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value= newName;
      cmd.Parameters.Add(newNameParameter);

      SqlParameter ClientIdParameter = new SqlParameter();
      ClientIdParameter.ParameterName = "@ClientId";
      ClientIdParameter.Value= this.GetId();
      cmd.Parameters.Add(ClientIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
      }
    }

    //Update request method for each client
    public void UpdateRequest(string newRequest)
    {
      if (newRequest != "")
      {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE clients SET request = @NewRequest OUTPUT INSERTED.request WHERE id=@ClientId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewRequest";
      newNameParameter.Value = newRequest;
      cmd.Parameters.Add(newNameParameter);

      SqlParameter ClientIdParameter = new SqlParameter();
      ClientIdParameter.ParameterName = "@ClientId";
      ClientIdParameter.Value= this.GetId();
      cmd.Parameters.Add(ClientIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._request = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
      }
    }

    //Update request date for each client
    public void UpdateDate(DateTime newDate)
    {
      DateTime defaultDate = new DateTime(1800,1,1);
      if (newDate != defaultDate)
      {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("UPDATE clients SET date = @NewDate OUTPUT INSERTED.date WHERE id=@ClientId;", conn);

        SqlParameter newDateParameter = new SqlParameter();
        newDateParameter.ParameterName = "@NewDate";
        newDateParameter.Value= newDate;
        cmd.Parameters.Add(newDateParameter);

        SqlParameter ClientIdParameter = new SqlParameter();
        ClientIdParameter.ParameterName = "@ClientId";
        ClientIdParameter.Value= this.GetId();
        cmd.Parameters.Add(ClientIdParameter);
        SqlDataReader rdr = cmd.ExecuteReader();

        while(rdr.Read())
        {
          this._date = rdr.GetDateTime(0);
        }

        if (rdr != null)
        {
          rdr.Close();
        }

        if (conn != null)
        {
          conn.Close();
        }
      }
    }

    //Update stylist method for each client
    public void UpdateStylistId(int newStylistId)
    {
      if (newStylistId != 0)
      {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("UPDATE clients SET stylist_id = @NewStylistId OUTPUT INSERTED.stylist_id WHERE id=@ClientId;", conn);

        SqlParameter newStylistIdParameter = new SqlParameter();
        newStylistIdParameter.ParameterName = "@NewStylistId";
        newStylistIdParameter.Value= newStylistId.ToString();
        cmd.Parameters.Add(newStylistIdParameter);

        SqlParameter ClientIdParameter = new SqlParameter();
        ClientIdParameter.ParameterName = "@ClientId";
        ClientIdParameter.Value= this.GetId();
        cmd.Parameters.Add(ClientIdParameter);
        SqlDataReader rdr = cmd.ExecuteReader();

        while(rdr.Read())
        {
          this._stylistId = rdr.GetInt32(0);
        }

        if (rdr != null)
        {
          rdr.Close();
        }

        if (conn != null)
        {
          conn.Close();
        }
      }
    }


    //Delete method for each client
    public void DeleteThisClient()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id = @ClientId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@ClientId";
      idParameter.Value = this.GetId().ToString();
      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();
    }

    //Delete method for all clients
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
