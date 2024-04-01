using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Micro_Instagram.Models;
using Newtonsoft.Json;

namespace Micro_Instagram.Services
{
    public class ApiService
    {
        private HttpClient client = new HttpClient();
        private const string url = "https://jsonplaceholder.typicode.com/photos";
        public async Task<List<Photo>> GetPhotosAsync()
        {
            try
            {
                var jsonResponse = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<Photo>>(jsonResponse);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return new List<Photo>();
            }
        }

        public async Task<bool> DeletePhotoAsync(int photoId)
        {
            try
            {
                var response = await client.DeleteAsync($"{url}/{photoId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred during deletion: {e.Message}");
                return false;
            }
        }

        
        public async Task<bool> UpdatePhotoAsync(Photo photo)
        {
            var json = JsonConvert.SerializeObject(photo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PutAsync($"{url}/{photo.Id}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred during updating: {e.Message}");
                return false;
            }
        }
    }
}
