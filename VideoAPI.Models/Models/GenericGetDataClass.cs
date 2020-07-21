using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace VideoAPI.Models.Models {
    public static class GenericGetDataClass<TModel> where TModel : class {

        public static async Task<TModel> GetAllData(string actionPath) // API GET метод
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                TModel model;

                var response = await client.GetAsync(actionPath);

                if (response.IsSuccessStatusCode) {
                    model = await response.Content.ReadAsAsync<TModel>();
                    return model;
                }
                // Костыль
                return null;
            }
        }

        public static async Task<bool> EditData(string actionPath, TModel editedModel)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PutAsJsonAsync(actionPath, editedModel);

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
        }

        public static async Task<bool> AddData(string actionPath, TModel addedModel)
        {
            using (HttpClient client = new HttpClient()) {
                client.BaseAddress = new Uri("https://localhost:5001/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync(actionPath, addedModel);

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
        }

        public static async Task<bool> DeleteData(string actionPath)
        {
            using (HttpClient client = new HttpClient()) {
                client.BaseAddress = new Uri("https://localhost:5001/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.DeleteAsync(actionPath);

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
        }
    }
}
