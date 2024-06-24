using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace ExtremeWeatherBoard.DAL
{
    public class CategoryApiService
    {
        protected Uri BaseAdress { get; }                
        public CategoryApiService()
        {
            BaseAdress = new Uri("https://localhost:44353/api/Category/");
        }
        public async Task<List<Category>?> GetCategoriesAsync()
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
        public async Task<string> PostCategoryAsync(string title, AdminUserData poster)
        {           
            Category newCategory = new Category() {Title = title, CreatorAdminUser = poster, TimeStamp = DateTime.UtcNow};
            string categoryJson = JsonConvert.SerializeObject(newCategory);
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = BaseAdress;
                using var content = new StringContent(categoryJson, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("AddCategory", content);
                if (response.IsSuccessStatusCode) 
                {
                    return "true";
                }
                else return response.StatusCode.ToString();
            }
        }

    }
}
