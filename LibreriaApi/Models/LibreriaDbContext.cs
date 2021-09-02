using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibreriaApi.Models {
    public class LibreriaDbContext : DbContext {
        private const string RUTA_XML = "App_Data/xml/";

        public LibreriaDbContext(DbContextOptions<LibreriaDbContext> options) : base(options) {

        }


        public List<Dictionary<String, Object>> SqlList(String queryName, Array parameters, Dictionary<String, Object> replaceParam, IWebHostEnvironment _env) {

            string query = null;
            string[] filePaths = Directory.GetFiles(Path.Combine(_env.ContentRootPath, RUTA_XML));
            foreach (string file in filePaths) {
                XElement xml = XElement.Load(file);
                if (xml.Descendants("query").FirstOrDefault(x => x.Attribute("name").Value == queryName) != null) {
                    query = xml.Descendants("query").FirstOrDefault(x => x.Attribute("name").Value == queryName).Value;
                    break;
                }
            }

            base.Database.GetDbConnection().Open();
            var cmd = base.Database.GetDbConnection().CreateCommand();

            if (replaceParam != null) {
                foreach (var entry in replaceParam) {
                    query = query.Replace(entry.Key, entry.Value.ToString());
                }
            }

            cmd.CommandText = query;
            if (parameters != null) {
                cmd.Parameters.AddRange(parameters);
            }

            var reader = cmd.ExecuteReader();
            List<Dictionary<String, Object>> items = new List<Dictionary<String, Object>>();
            while (reader.Read()) {
                Dictionary<String, Object> item = new Dictionary<String, Object>();
                for (int i = 0; i < reader.FieldCount; i++)
                    if (!item.ContainsKey(reader.GetName(i))) item.Add(reader.GetName(i), reader[i]);

                items.Add(item);
            }

            cmd.Parameters.Clear();
            base.Database.GetDbConnection().Close();
            return items;
        }




        public Dictionary<string, object> SqlObject(string queryName, Array parameters, Dictionary<String, Object> replaceParam, IWebHostEnvironment _env) {
            string query = null;
            string[] filePaths = Directory.GetFiles(Path.Combine(_env.ContentRootPath, RUTA_XML));
            foreach (string file in filePaths) {
                XElement xml = XElement.Load(file);
                if (xml.Descendants("query").FirstOrDefault(x => x.Attribute("name").Value == queryName) != null) {
                    query = xml.Descendants("query").FirstOrDefault(x => x.Attribute("name").Value == queryName).Value;
                }
            }
            base.Database.GetDbConnection().Open();
            var cmd = base.Database.GetDbConnection().CreateCommand();

            if (replaceParam != null) {
                foreach (var entry in replaceParam) {
                    query = query.Replace(entry.Key, entry.Value.ToString());
                }
            }

            cmd.CommandText = query;
            if (parameters != null) {
                cmd.Parameters.AddRange(parameters);
            }

            var reader = cmd.ExecuteReader();
            Dictionary<String, Object> item = new Dictionary<String, Object>();
            while (reader.Read()) {
                for (int i = 0; i < reader.FieldCount; i++)
                    if (!item.ContainsValue(reader[i])) item.Add(reader[i].ToString(), reader[i]);
            }

            cmd.Parameters.Clear();
            base.Database.GetDbConnection().Close();
            return item;
        }




        public int SqlExec(string queryName, Array parameters, IWebHostEnvironment _env) {
            string query = null;
            string[] filePaths = Directory.GetFiles(Path.Combine(_env.ContentRootPath, RUTA_XML));
            foreach (string file in filePaths) {
                XElement xml = XElement.Load(file);
                if (xml.Descendants("query").FirstOrDefault(x => x.Attribute("name").Value == queryName) != null) {
                    query = xml.Descendants("query").FirstOrDefault(x => x.Attribute("name").Value == queryName).Value;
                }
            }
            base.Database.GetDbConnection().Open();
            var cmd = base.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = query;
            if (parameters != null) {
                cmd.Parameters.AddRange(parameters);
            }

            int result = cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();
            base.Database.GetDbConnection().Close();
            return result;
        }



        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Libro> Libro { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
    }
}
