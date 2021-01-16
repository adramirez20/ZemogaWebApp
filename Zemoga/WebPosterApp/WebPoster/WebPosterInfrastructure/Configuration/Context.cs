using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using WebPosterDomain.Entities;
using Microsoft.Extensions.Configuration.Json;


namespace WebPosterInfrastructure.Configuration
{
    public class Context : DbContext
    {

        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Poster> Post { set; get; }
        public DbSet<Comment> Comment { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(GetStringConectionConfig());

            base.OnConfiguring(optionsBuilder);
        }


        private string GetStringConectionConfig()
        {

            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            string strCon = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            if (string.IsNullOrEmpty(strCon))
                strCon = "Data Source=localhost;Initial Catalog=PosterDB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
            return strCon;
        }

    }
}
