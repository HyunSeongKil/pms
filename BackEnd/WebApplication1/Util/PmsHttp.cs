using Blazored.SessionStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using static WebApplication1.Util.Const;

namespace WebApplication1.Util
{
    public class PmsHttp
    {
        public async static Task<HttpRequestMessage> CreateGetRequestMessage(string uri, ISessionStorageService sessionStorage)
        {
            HttpRequestMessage requestMessage = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
            };


            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(TOKEN, await sessionStorage.GetItemAsync<string>(TOKEN));


            //
            return requestMessage;
        }


        /// <summary>
        /// header에 token값 추가하여 get 요청
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="uri"></param>
        /// <param name="sessionStorage"></param>
        /// <returns></returns>
        public async static Task<T> GetAsync<T>(HttpClient httpClient, string uri, ISessionStorageService sessionStorage)
        {
            HttpRequestMessage requestMessage = new()
            {
                Method = new HttpMethod("GET"),
                RequestUri = new Uri(uri),
                
            };


            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(TOKEN, await sessionStorage.GetItemAsync<string>(TOKEN));

            var response = await httpClient.SendAsync(requestMessage);

            return await response.Content.ReadFromJsonAsync<T>();
        }



        /// <summary>
        /// header에 token값 추가하여 post 요청
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="uri"></param>
        /// <param name="sessionStorage"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async static Task<HttpResponseMessage> PostAsync<T>(HttpClient httpClient, string uri, ISessionStorageService sessionStorage, T data)
        {
            HttpRequestMessage requestMessage = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(uri),
                Content = JsonContent.Create<T>(data, default, new System.Text.Json.JsonSerializerOptions()
                {
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                }),
            };

            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(TOKEN, await sessionStorage.GetItemAsync<string>(TOKEN));
            requestMessage.Content.Headers.TryAddWithoutValidation("x-custom-header", "value");

            return await httpClient.SendAsync(requestMessage);

        }




        /// <summary>
        /// header에 token값 추가하여 put 요청
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="uri"></param>
        /// <param name="sessionStorage"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async static Task<HttpResponseMessage> PutAsync<T>(HttpClient httpClient, string uri, ISessionStorageService sessionStorage, T data)
        {
            HttpRequestMessage requestMessage = new()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(uri),
                Content = JsonContent.Create<T>(data, default, new System.Text.Json.JsonSerializerOptions()
                {
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                }),
            };

            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(TOKEN, await sessionStorage.GetItemAsync<string>(TOKEN));
            requestMessage.Content.Headers.TryAddWithoutValidation("x-custom-header", "value");

            return await httpClient.SendAsync(requestMessage);

        }
    }
}
