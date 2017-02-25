using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace HairSalon
{
  public class FeedbackTest : IDisposable
  {
    public FeedbackTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void OverrideEquals_TwoSameFeedbacks_Same()
    {//Arrange, Act
      Feedback firstFeedback = new Feedback("nice shampoo", 1);
      Feedback secondFeedback = new Feedback("nice shampoo", 1);

      //Assert
      Assert.Equal(firstFeedback,secondFeedback);
    }

    [Fact]
    public void GetAll_DatabaseEmptyAtFirst_NoFeedbacks()
    {
      //Arrange, Act
      int output = Feedback.GetAll().Count;

      //Assert
      Assert.Equal(0, output);
    }

    [Fact]
    public void Save_OneInstanceofFeedback_SavesToDatabase()
    {
      //Arrange
      Feedback testFeedback = new Feedback("nice shampoo", 1);
      testFeedback.Save();

      //Act
      List<Feedback> result = Feedback.GetAll();
      List<Feedback> testList = new List<Feedback>{testFeedback};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void SaveGetAll_OneInstanceofFeedbacks_AssignIdToInstance()
    {
      //Arrange
      Feedback testFeedback = new Feedback("nice shampoo", 1);

      //Act
      testFeedback.Save();
      Feedback savedFeedback = Feedback.GetAll()[0];

      int result = savedFeedback.GetId();
      int testId = testFeedback.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void GetClientFeedback_OneClient_FeedbacksAboutClients()
    {
      //Act
      DateTime testDate = new DateTime(2017,6,4);
      Client testClient = new Client("Wendy","curly",testDate,1);
      testClient.Save();

      Feedback testFeedback1 = new Feedback("nice shampoo", testClient.GetId());
      testFeedback1.Save();
      Feedback testFeedback2 = new Feedback("more gentle", testClient.GetId());
      testFeedback2.Save();

      //Arrange
      List<Feedback> output = testClient.GetClientFeedback();
      List<Feedback> verify = new List<Feedback>{testFeedback1,testFeedback2};

      //Act
      Assert.Equal(output,verify);
    }

    public void Dispose()
    {
      Feedback.DeleteAll();
    }
  }
}
