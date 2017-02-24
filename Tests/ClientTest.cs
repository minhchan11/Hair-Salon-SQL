using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace HairSalon
{
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void OverrideEquals_TwoSameClients_Same()
    {//Arrange, Act
      DateTime testDate = new DateTime(2016,4,30);
      Client firstClient = new Client("Wendy","curly",testDate,1);
      Client secondClient = new Client("Wendy","curly",testDate,1);

      //Assert
      Assert.Equal(firstClient,secondClient);
    }

    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
