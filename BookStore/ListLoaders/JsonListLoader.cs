using BookStore.Interfaces;
using BookStore.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BookStore.ListLoaders
{
    class JsonListLoader : IListLoader
    {
        private readonly IConfiguration _config;
        private readonly IDaoService _daoService;
        public JsonListLoader(IConfiguration configuration, IDaoService daoService)
        {
            _config = configuration;
            _daoService = daoService;
            _daoService.Connect();
        }
        public List<Book> GetData()
        {

            throw new NotImplementedException();
        }

        public void LoadData()
        {
            var items = LoadFile();
            items.ForEach(i => _daoService.Insert(i));
        }

        private List<Book> LoadFile()
        {
            try
            {
                var file = _config["ListLoader:Name"];
                var ignoreCase = @"\b(" + _config["IgnoreAuthors"] + @")\b";
                var ignoreDays = _config.GetSection("IgnoreDays").Get<List<string>>();
                List<Book> items = new List<Book>();
                if (!File.Exists(file))
                {
                    throw new Exception("The file does not exists");
                }
                using (StreamReader r = new StreamReader(file))
                {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<Book>>(json);
                }
                return items.Where(p =>
                {
                    var authorFirstName = p.Author.Split(',')[1];
                    Console.WriteLine(authorFirstName);
                    if (Regex.Match(authorFirstName.ToLower(), ignoreCase, RegexOptions.Singleline | RegexOptions.IgnoreCase).Success)
                    {
                        return false;
                    }

                    var day = p.PublishDate.DayOfWeek.ToString();
                    if (ignoreDays.Contains(day))
                    {
                        return false;
                    }
                    return true;
                }).OrderBy(p => p.Title).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
