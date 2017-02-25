using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace HairSalon
{
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void OverrideEquals_TwoSameStylists_Same()
    {
      //Arrange, Act
      Stylist firstStylist = new Stylist("John");
      Stylist secondStylist = new Stylist("John");

      //Assert
      Assert.Equal(firstStylist, secondStylist);
    }

    [Fact]
    public void GetAll_DatabaseEmptyAtFirst_NoStylists()
    {
      //Arrange,Act
      int result = Stylist.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Save_OneInstanceofCusine_SavesToDatabase()
    {
      //Arrange
      Stylist testStylist = new Stylist("John");
      testStylist.Save();

      //Act
      List<Stylist> output = Stylist.GetAll();
      List<Stylist> verify = new List<Stylist>{testStylist};

      //Assert
      Assert.Equal(output,verify);
    }

    [Fact]
    public void SaveGetAll_OneInstanceofStylist_AssignIdToInstance()
    {
      //Arrange
      Stylist testStylist = new Stylist("John");
      testStylist.Save();
      Stylist savedStylist = Stylist.GetAll()[0];

      //Act
      int output = savedStylist.GetId();
      int verify = testStylist.GetId();

      //Assert
      Assert.Equal(output,verify);
    }

    [Fact]
    public void Find_StylistClass_FoundStylist()
    {
      //Arrange
      Stylist testStylist = new Stylist("John");
      testStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      //Assert
      Assert.Equal(testStylist, foundStylist);
    }

    [Fact]
    public void Search_StylistName_StylistMatches()
    {
      //Arrange
      Stylist firstStylist = new Stylist("John");
      firstStylist.Save();
      Stylist secondStylist = new Stylist("Kim");
      secondStylist.Save();

      //Act
      List<Stylist> stylistMatches = Stylist.Search("John");

      List<Stylist> expectedStylists = new List<Stylist>{firstStylist};

      //Assert
      Assert.Equal(stylistMatches, expectedStylists);
    }

    [Fact]
    public void Search_StylistName_StylistDoesntMatch()
    {
      //Arrange
      Stylist firstStylist = new Stylist("John");
      firstStylist.Save();
      Stylist secondStylist = new Stylist("Kim");
      secondStylist.Save();

      //Act
      List<Stylist> stylistMatches = Stylist.Search("Johnrere");

      List<Stylist> expectedStylists = new List<Stylist>{};

      //Assert
      Assert.Equal(stylistMatches, expectedStylists);
    }

    [Fact]
    public void Search_StylistNameCapitalization_StylistMatch()
    {
      //Arrange
      Stylist firstStylist = new Stylist("John");
      firstStylist.Save();
      Stylist secondStylist = new Stylist("Kim");
      secondStylist.Save();

      //Act
      List<Stylist> stylistMatches = Stylist.Search("JoHN");

      List<Stylist> expectedStylists = new List<Stylist>{firstStylist};

      //Assert
      Assert.Equal(stylistMatches, expectedStylists);
    }

    [Fact]
    public void GetClient_TwoClientsSameClass_RetrievedClients()
    {
      //Arrange
      Stylist testStylist = new Stylist("John");
      testStylist.Save();

      DateTime testDate = new DateTime(2016,4,30);
      Client firstClient = new Client("Wendy","curly",testDate,testStylist.GetId());
      firstClient.Save();
      Client secondClient = new Client("Lana","straight",testDate,testStylist.GetId());
      secondClient.Save();

      //Act
      List<Client> output = testStylist.GetClient();
      List<Client> verify = new List<Client>{firstClient,secondClient};

      //Assert
      Assert.Equal(output,verify);
    }

    [Fact]
    public void DeleteThisStylist_OneStylist_StylistDeleted()
    {//Arrange
      Stylist firstStylist = new Stylist("Kim");
      firstStylist.Save();
      Stylist secondStylist = new Stylist("chinese");
      secondStylist.Save();
      firstStylist.DeleteThisStylist();
      List<Stylist> outputList = Stylist.GetAll();

      //Act
      List<Stylist> verifyList = new List<Stylist>{secondStylist};

      //Assert
      Assert.Equal(outputList,verifyList);
    }

    [Fact]
    public void DeleteClientsInStylist_OneStylist_StylistEmpty()
    {//Arrange
      Stylist testStylist = new Stylist("Western");
      testStylist.Save();

      DateTime testDate = new DateTime(2016,4,30);
      Client firstClient = new Client("Wendy","curly",testDate,testStylist.GetId());
      firstClient.Save();
      Client secondClient = new Client("Lana","straight",testDate,testStylist.GetId());
      secondClient.Save();

      testStylist.DeleteClientsInStylist();
      List<Client> outputList = testStylist.GetClient();

      //Act
      List<Client> verifyList = new List<Client>{};

      //Assert
      Assert.Equal(outputList, verifyList);
    }

    [Fact]
    public void Update_OneStylist_NewStylistName()
    {
      string original = "John";
      Stylist newStylist = new Stylist(original);
      newStylist.Save();
      string newString = "Jack";
      newStylist.Update(newString);
      string newStylistName = newStylist.GetName();

      Assert.Equal(newString, newStylistName);
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();
      Feedback.DeleteAll();
    }
  }
}
