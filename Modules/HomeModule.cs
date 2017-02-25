using Nancy;
using System;
using System.Collections.Generic;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/stylists"] = _ =>{
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };
      Post["/stylists/new"] = _ => {
        string stylistName = Request.Form["stylist"];
        Stylist newStylist = new Stylist(stylistName);
        newStylist.Save();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };
      Get["/clients"] = _ =>{
        var allStylists = Stylist.GetAll();
        var allClients = Client.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("stylistList", allStylists);
        model.Add("clientList", allClients);
        return View["clients.cshtml", model];
      };
      Post["/clients/new"] = _ =>{
        string newName = Request.Form["name"];
        string newRequest = Request.Form["request"];
        DateTime newDate = Request.Form["book-date"];
        int newStylistId = Request.Form["stylist-id"];
        Client newClient = new Client(newName, newRequest, newDate, newStylistId);
        newClient.Save();

        var allStylists = Stylist.GetAll();
        var allClients = Client.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("stylistList", allStylists);
        model.Add("clientList", allClients);
        return View["clients.cshtml", model];
      };
      Get["/stylists/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        var foundStylist = Stylist.Find(parameters.id);
        var foundClients = foundStylist.GetClient();
        model.Add("stylist",foundStylist);
        model.Add("clients",foundClients);
        return View["stylist.cshtml", model];
      };
      Get["/clients/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        var foundClient = Client.Find(parameters.id);
        var foundStylist = Stylist.Find(foundClient.GetStylistId());
        var allStylists = Stylist.GetAll();
        var clientFeedbacks = foundClient.GetClientFeedback();

        model.Add("stylists", allStylists);
        model.Add("stylist", foundStylist);
        model.Add("client", foundClient);
        model.Add("feedbacks", clientFeedbacks);
        return View["client.cshtml", model];
      };
      Delete["/stylists/deleted"] = _ => {
        Stylist.DeleteAll();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };
      Delete["/clients/deleted"] = _ => {
        Client.DeleteAll();
        var allStylists = Stylist.GetAll();
        var allClients = Client.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("stylistList", allStylists);
        model.Add("clientList", allClients);
        return View["clients.cshtml", model];
      };
      Delete["/stylist/{id}/cleared"] = parameters => {
        var foundStylist = Stylist.Find(parameters.id);
        foundStylist.DeleteClientInStylist();
        var foundClients = foundStylist.GetClient();
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("stylist",foundStylist);
        model.Add("clients",foundClients);
        return View["stylist.cshtml", model];
      };
      Delete["/stylist/{id}/deleted"] = parameters => {
        Stylist foundStylist = Stylist.Find(parameters.id);
        foundStylist.DeleteThisStylist();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };
      Delete["/client/{id}/deleted"] = parameters => {
        Client foundClient = Client.Find(parameters.id);
        foundClient.DeleteThisClient();
        var allStylists = Stylist.GetAll();
        var allClients = Client.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("stylistList", allStylists);
        model.Add("clientList", allClients);
        return View["clients.cshtml", model];
      };
      Patch["stylist/edit/{id}"] = parameters => {
        Stylist foundStylist = Stylist.Find(parameters.id);
        foundStylist.Update(Request.Form["stylist-name"]);
        Dictionary<string, object> model = new Dictionary<string, object>{};
        var updatedStylist = Stylist.Find(parameters.id);
        var foundClients = foundStylist.GetClient();
        model.Add("stylist",updatedStylist);
        model.Add("clients",foundClients);
        return View["stylist.cshtml", model];
      };
      Patch["client/edit/{id}"] = parameters => {
        Client foundClient = Client.Find(parameters.id);
        foundClient.UpdateName(Request.Form["client-name"]);
        foundClient.UpdateDate(Request.Form["client-date"]);
        foundClient.UpdateStylistId(Request.Form["client-stylist-id"]);
        foundClient.UpdateRequest(Request.Form["client-request"]);
        Dictionary<string, object> model = new Dictionary<string, object>{};
        var updatedClient = Client.Find(parameters.id);
        var foundStylist = Stylist.Find(foundClient.GetStylistId());
        var allStylists = Stylist.GetAll();
        var clientFeedbacks = foundClient.GetClientFeedback();

        model.Add("stylists", allStylists);
        model.Add("stylist", foundStylist);
        model.Add("client", updatedClient);
        model.Add("feedbacks", clientFeedbacks);
        return View["client.cshtml", model];
      };
      Post["/stylists/search"] = _ => {
        string searchTerm = Request.Form["search-stylist"];
        List<Stylist> stylistMatches = Stylist.Search(searchTerm);
        return View["stylist_search.cshtml", stylistMatches];
      };
      Post["/clients/search"] = _ => {
        string searchTerm = Request.Form["search-client"];
        List<Client> clientMatches = Client.Search(searchTerm);
        return View["client_search.cshtml", clientMatches];
      };
      Post["/client/feedback/{id}"] = parameters => {
        Client foundClient = Client.Find(parameters.id);
        string preference = Request.Form["feedback"];
        int clientId = Request.Form["clientId"];
        Feedback newFeedback = new Feedback(preference, clientId);
        newFeedback.Save();

        Dictionary<string, object> model = new Dictionary<string, object>{};
        var clientFeedbacks = foundClient.GetClientFeedback();
        var foundStylist = Stylist.Find(foundClient.GetStylistId());
        var allStylists = Stylist.GetAll();
        model.Add("feedbacks", clientFeedbacks);
        model.Add("client", foundClient);
        model.Add("stylist", foundStylist);
        model.Add("stylists", allStylists);

        return View["client.cshtml", model];
      };
    }
  }
}
