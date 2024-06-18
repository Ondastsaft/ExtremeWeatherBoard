using Azure.Core.Serialization;
using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;
using Newtonsoft.Json;
namespace ExtremeWeatherBoard.DAL
{
    public class CategoryAPIService : IApiService
    {
        protected Uri BaseAdress { get; }
        public CategoryAPIService()
        {
            BaseAdress = new Uri("https://localhost:44353/api/Category/");
        }
        public async Task<List<Category>?>? GetObjects()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = BaseAdress;
                HttpResponseMessage response = await client.GetAsync("GetAll");
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    try
                    {
                        List<Category>? retrievedObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Category>>(responseString);
                        if (retrievedObjects != null && retrievedObjects.Count > 0)
                        {
                            return retrievedObjects;
                        }
                    }
                    catch (Exception e) { Console.WriteLine(e.Message); }
                }
                return null;
            }
        }
    }
}
