using App2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BrecketWonsDiary
{
    class API
    {
        //CONST REST
        private const string REST_BASE_URL = "https://agnieszkawua.herokuapp.com";

        //GET
        public async Task getArticles(MainViewModel DataContext)
        {
            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync(REST_BASE_URL);
                var items = await result.Content.ReadAsStringAsync();
                DataContext.ItemsCollection = JsonConvert.DeserializeObject<ObservableCollection<Article>>(items);
            }
        }

        //GET notes 
        public async Task getUserArticles(MainViewModel DataContext)
        {
            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync(REST_BASE_URL + "/books/" + DataContext.OwnerId);
                var items = await result.Content.ReadAsStringAsync();
                DataContext.ItemsCollection = JsonConvert.DeserializeObject<ObservableCollection<Article>>(items);
                DataContext.AllArticles = true;
            }
        }

        //GET books
        public async Task getBooks(MainViewModel DataContext)
        {
            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync(REST_BASE_URL + "/books");
                var items = await result.Content.ReadAsStringAsync();
                DataContext.ItemsCollection = JsonConvert.DeserializeObject<ObservableCollection<Article>>(items);
                DataContext.AllArticles = true;
            }
        }

        //POST
        public async void postArticle(Article article, MainViewModel DataContext)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(REST_BASE_URL + "/books/" + DataContext.OwnerId);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, DataContext.OwnerId);
                request.Content = new StringContent(article.Serialize(), Encoding.UTF8, "application/json");
                await client.SendAsync(request);
            }
        }

        //DELETE one note
        public async Task deleteArticle(Article article, MainViewModel DataContext)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(REST_BASE_URL + "/");

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "books/"+DataContext.OwnerId + "/" + article.id);
                await client.SendAsync(request);
            }
        }

        //DELETE BOOK
        public async Task deleteBook(Book book, MainViewModel DataContext)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(REST_BASE_URL + "/");
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "books/"+ book.name);
                 await client.SendAsync(request);
            }
        }
    }
}
