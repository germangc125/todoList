using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TodoList.Models;

namespace TodoList.services
{
    public class WebAPIService
    {
        //string urlBase = "http://localhost:8080/api/";
        // string urlBase = "http://127.0.0.1:8080/api/";
        string urlBase = "http://10.0.2.2:8080/api/"; // Special alias to your host loopback interface (i.e., 127.0.0.1 on your development machine) https://developer.android.com/studio/run/emulator-networking

        #region Fields 

        System.Net.Http.HttpClient client;

        #endregion

        #region Properties 

        public ObservableCollection<TodoItem> Items
        {
            get; private set;
        }

        public string WebAPIUrl
        {
            get; private set;
        }

        #endregion

        #region Constructor
        public WebAPIService()
        {
            client = new System.Net.Http.HttpClient();
        }

        #endregion

        #region Methods
        public async System.Threading.Tasks.Task<ObservableCollection<TodoItem>> GetListItems()
        {
            WebAPIUrl = urlBase + "Todo";
            var uri = new Uri(WebAPIUrl);
            try
            {
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<ObservableCollection<TodoItem>>(content);
                    return Items;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }


        public async System.Threading.Tasks.Task<string> SaveTodoItem(TodoItem Item)
        {
            WebAPIUrl = urlBase + "Todo/Create/";
            var uri = new Uri(WebAPIUrl);

            var data = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("Name", Item.Name),
                    new KeyValuePair<string, string> ("IsComplete", Item.IsComplete.ToString()),
                }
            );

            try
            {
                var response = await client.PostAsync(uri, data);
                return "Se ha guardado correctamente";
            }
            catch (Exception ex)
            {
                return "Se ha presentado un error:  " + ex.Message;
            }
        }

        public async System.Threading.Tasks.Task<string> Update(TodoItem Item)
        {
            WebAPIUrl = urlBase + "Todo";  // + Item.Key;
            var uri = new Uri(WebAPIUrl);

            var httpContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("Name", Item.Name),
                    new KeyValuePair<string, string> ("IsComplete", Item.IsComplete.ToString()),
                        new KeyValuePair<string, string> ("Key", Item.Key),
                }
            );

            try
            {
                var response = await client.PutAsync(uri, httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    return "Existe un problema con eliminar la actividad.";
                }
                else
                {
                    return "Se ha actualizado correctamente";
                }
              
            }
            catch (Exception ex)
            {
                return "Se ha presentado un error:  " + ex.Message;
            }
        }



        public async System.Threading.Tasks.Task<string> Delete(string key)
        {
            WebAPIUrl = urlBase + "Todo/" + key + "/";
            var uri = new Uri(WebAPIUrl);

            try
            {
                var response = await client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return "Se ha eliminado correctamente";
                }
                else
                {
                    return "Existe un problema con eliminar la actividad.";

                }
            }
            catch (Exception ex)
            {
                return "Se ha presentado un error al eliminar:  " + ex.Message;
            }
        }

        #endregion
    }
}
