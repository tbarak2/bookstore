using BookStore.Interfaces;
using BookStore.Model;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BookStore.Repositroies
{
    class CsvBookRepository : IBookRepository
    {
        private readonly ConnectionDriver _config;
        private string _file;
        private string _datetime;
        public CsvBookRepository(IOptions<ConnectionDriver> connectionDriver)
        {
            _config = connectionDriver?.Value ?? throw new ArgumentNullException(nameof(connectionDriver));
        }
        public void Connect()
        {
            var connectionString = _config.DefaultConnection;
            _datetime = DateTime.Now.ToString("ddmmyyyyHm");
            _file =$"{ connectionString}{ _datetime}.csv";
        }

         public int Insert(Book book)
        {
            try
            {
                var books = new List<Book>();
                books.Add(book);
                var csvconfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
                StreamWriter writer;

                if (File.Exists(@_file))
                {
                    csvconfiguration.HasHeaderRecord = false;
                    writer = new StreamWriter(File.Open(@_file, FileMode.Append));
                }
                else
                {
                    writer = new StreamWriter(@_file);
                }

                using (var csv = new CsvWriter(writer, csvconfiguration))
                {
                    csv.WriteRecords(books);
                }
                int lastLine = File.ReadLines(_file).Count();
                writer.Close();

                return lastLine;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
