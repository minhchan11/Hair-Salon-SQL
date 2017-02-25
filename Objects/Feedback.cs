using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon
{
  public class Feedback
  {
    private int _id;
    private string _preference;
    private int _clientId;

    public Feedback(string Preference, int ClientId, int Id = 0)
    {
      _id = Id;
      _preference = Preference;
      _clientId = ClientId;
    }

    public override bool Equals(System.Object otherFeedback)
    {
      if (!(otherFeedback is Feedback))
      {
        return false;
      }
      else
      {
        Feedback newFeedback = (Feedback) otherFeedback;
        bool idEquality = (this.GetId() == newFeedback.GetId());
        bool preferenceEquality = (this.GetPreference() == newFeedback.GetPreference());
        bool clientIdEquality = (this.GetClientId() == newFeedback.GetClientId());
        return (idEquality && preferenceEquality && clientIdEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public string GetPreference()
    {
      return _preference;
    }
    public void SetPreference(string newPreference)
    {
      _preference = newPreference;
    }

    public int GetClientId()
    {
      return _clientId;
    }
    public void SetClientId(int newClientId)
    {
      _clientId = newClientId;
    }

    public static List<Feedback> GetAll()
     {
       List<Feedback> allFeedbacks = new List<Feedback>{};

       SqlConnection conn = DB.Connection();
       conn.Open();

       SqlCommand cmd = new SqlCommand("SELECT * FROM feedbacks;", conn);
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

      public void Save()
      {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("INSERT INTO feedbacks (preference, client_id) OUTPUT INSERTED.id VALUES (@Preference, @ClientId);", conn);

        SqlParameter preferenceParameter = new SqlParameter();
        preferenceParameter.ParameterName = "@Preference";
        preferenceParameter.Value = this.GetPreference();

        SqlParameter clientIdParameter = new SqlParameter();
        clientIdParameter.ParameterName = "@ClientId";
        clientIdParameter.Value = this.GetClientId();

        cmd.Parameters.Add(preferenceParameter);
        cmd.Parameters.Add(clientIdParameter);
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

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM feedbacks;", conn);
      cmd.ExecuteNonQuery();
    }

  }
}
