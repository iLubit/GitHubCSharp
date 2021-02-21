using System;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GitHubCSharpLib.Models;
using Newtonsoft.Json;

namespace GitHubCSharpLib
{
    public class GitHubCSharp
    {

      public GitHubCSharp(){}

      public string TriggerEvent(string EventType, string Owner, string Repo, string Token)
      {
        var Dispatch = new Dispatches();
        Dispatch.event_type = EventType;
        var json = JsonConvert.SerializeObject(Dispatch);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        
        HttpClient client= new HttpClient();
        client.BaseAddress = new Uri("https://api.github.com");
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("iLubit-GitHubCSharp", "1.0"));
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        var response = client.PostAsync($"/repos/{Owner}/{Repo}/dispatches", data ).Result;
        Console.WriteLine(client.DefaultRequestHeaders);
        Console.WriteLine(json);
        Console.WriteLine(response);
        return "ok";
      } 
    }
}
