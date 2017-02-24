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

    [Fact]
    public void GetAll_DatabaseEmptyAtFirst_NoClient()
    {
      //Arrange, Act
      int output = Client.GetAll().Count;

      //Assert
      Assert.Equal(0, output);
    }

    [Fact]
    public void Save_OneInstanceofClients_SavesToDatabase()
    {
      //Arrange
      DateTime testDate = new DateTime(2016,4,30);
      Client firstClient = new Client("Wendy","curly",testDate,1);
      firstClient.Save();

      //Act
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{firstClient};

      //Assert
      Assert.Equal(testList, result);
    }

    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
