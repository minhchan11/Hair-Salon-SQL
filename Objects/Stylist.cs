using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon
{
  public class Stylist
  {
    private int _id;
    private string _name;

    public Stylist(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.GetId() == newStylist.GetId());
        bool nameEquality = (this.GetName() == newStylist.GetName());
        return (idEquality && nameEquality);
      }
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

    public static List<Stylist> GetAll()
     {
       List<Stylist> allStylists = new List<Stylist>{};

       SqlConnection conn = DB.Connection();
       conn.Open();

       SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);
       SqlDataReader rdr = cmd.ExecuteReader();

       while(rdr.Read())
       {
         int stylistId = rdr.GetInt32(0);
         string stylistName = rdr.GetString(1);
         Stylist newStylist = new Stylist(stylistName, stylistId);
         allStylists.Add(newStylist);
       }

       if (rdr != null)
       {
         rdr.Close();
       }
       if (conn != null)
       {
         conn.Close();
       }

       return allStylists;
     }

      public void Save()
      {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("INSERT INTO stylists (name) OUTPUT INSERTED.id VALUES (@StylistName);", conn);


        SqlParameter nameParameter = new SqlParameter();
        nameParameter.ParameterName = "@StylistName";
        nameParameter.Value = this.GetName();
        cmd.Parameters.Add(nameParameter);
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

    public static Stylist Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @StylistId;", conn);

      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@StylistId";
      idParameter.Value = id.ToString();
      cmd.Parameters.Add(idParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string foundName = null;

      while (rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        foundName = rdr.GetString(1);
      }

      Stylist foundStylist = new Stylist(foundName, foundId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundStylist;
    }

    public static List<Stylist> Search(string match)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE name = @StylistName;", conn);

      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@StylistName";
      idParameter.Value = match;
      cmd.Parameters.Add(idParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Stylist> stylistMatches = new List<Stylist>{};

      while (rdr.Read())
      {
        int foundId = rdr.GetInt32(0);
        string foundName = rdr.GetString(1);
        Stylist foundStylist = new Stylist(foundName, foundId);
        stylistMatches.Add(foundStylist);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return stylistMatches;
    }

    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE stylists SET name = @NewName OUTPUT INSERTED.name WHERE id = @StylistId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);


      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.GetId();
      cmd.Parameters.Add(stylistIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        //rdr.GetString(0) = INSERTED.name
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

    public List<Client> GetClient()
    {
      List<Client> allClients = new List<Client>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE stylist_id =@StylistId ORDER BY cast([date] as datetime) asc;", conn);

      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@StylistId";
      idParameter.Value = this.GetId().ToString();
      cmd.Parameters.Add(idParameter);
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

    public void DeleteThisStylist()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM stylists WHERE id = @StylistId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@StylistId";
      idParameter.Value = this.GetId().ToString();
      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();
    }

    public void DeleteClientsInStylist()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE stylist_id = @StylistId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@StylistId";
      idParameter.Value = this.GetId().ToString();
      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
      cmd.ExecuteNonQuery();
    }

  }
}
