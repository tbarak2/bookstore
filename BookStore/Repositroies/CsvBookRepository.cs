using BookStore.Interfaces;
using BookStore.Model;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositroies
{
    class CsvBookRepository : IBookRepository
    {
        //private readonly IConfiguration _config;
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

        private void CreateFile(string connectionString)
        {
            var proprties = typeof(Book).GetProperties();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var prop in proprties)
            {
                stringBuilder.Append(prop.Name);
                stringBuilder.Append(",");                
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            var lines = new List<string>();
            lines.Add(stringBuilder.ToString());
            File.AppendAllLines(connectionString, lines);
            _file = connectionString;
        }

        public int Insert(Book book)
        {
            var books = new List<Book>();
            books.Add(book);
            var csvconfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
            StreamWriter writer;
            if (File.Exists(@_file))
            {
                csvconfiguration.HasHeaderRecord = false;
                writer =  new StreamWriter(File.Open(@_file, FileMode.Append));
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

        //public int Insert(Book book)
        //{
        //    if (File.Exists(@_file))
        //    {
        //        try
        //        {
        //            var proprties = typeof(Book).GetProperties();
        //            StringBuilder stringBuilder = new StringBuilder();
        //            foreach (var prop in proprties)
        //            {
        //                stringBuilder.Append(prop.GetValue(book,null));
        //                stringBuilder.Append(",");
        //            }
        //            stringBuilder.Remove(stringBuilder.Length - 1, 1);
        //            var lines = new List<string>();
        //            lines.Add(stringBuilder.ToString());
        //            //File.AppendAllText(_file, stringBuilder.ToString());
        //            File.AppendAllLines(_file, lines);
        //            int lastLine = File.ReadLines(_file).Count();
        //            return lastLine;
        //        }
        //        catch(Exception ex)
        //        {
        //            throw;
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception("There is a problem with the connection");
        //    }

        //}
    }
}
