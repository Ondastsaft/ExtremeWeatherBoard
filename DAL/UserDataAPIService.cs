using ExtremeWeatherBoard.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Text.Json;

namespace ExtremeWeatherBoard.DAL
{
    public class UserDataAPIService
    {

        private static Uri BaseAdress = new Uri("https://localhost:44311/api/");

        public async Task<UserData> GetUser(int id)
        {
            UserData userData = new UserData();
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAdress;
                HttpResponseMessage response = await client.GetAsync($"UserDataController/GetUserData/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string responsestring = await response.Content.ReadAsStringAsync();
                    try
                    {
                        userData = JsonSerializer.Deserialize<UserData>(responsestring);
                        return userData;
                    }
                    catch (Exception ex) { return null; };                    
                }
            }
            return null;
        }
        public async Task<UserData> GetAssociatedUserData(string id)
        {
            UserData userData = new UserData();
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAdress;
                HttpResponseMessage response = await client.GetAsync($"UserDataController/GetAssociatedUserData/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string responsestring = await response.Content.ReadAsStringAsync();
                    try
                    {
                        userData = JsonSerializer.Deserialize<UserData>(responsestring);
                        return userData;
                    }
                    catch (Exception ex) { return null; };
                }
            }
            return null;
        }


    }
}
