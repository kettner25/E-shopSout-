using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Soutez.Models;
using System.Xml;
using Microsoft.AspNetCore.Http;

namespace Soutez
{
    public static class Data
    {
        public static string[] data = System.IO.File.ReadAllLines("./Models/data.csv");

        public static List<Polozka> Polozky = NactiPolozky();

        public static string[] VratKategorie()
        {
            List<string> kategorie = new List<string>();

            int index = data[0].Split(";").ToList().IndexOf("category");

            for (int i = 1; i < data.Length; i++)
            {
                string cat = data[i].Split(";")[index];
                if (kategorie.IndexOf(cat) < 0)
                    kategorie.Add(cat);
            }

            return kategorie.ToArray();
        }

        private static List<Polozka> NactiPolozky()
        {
            List<Polozka> polozky = new List<Polozka>();

            List<string> razeni = data[0].Split(";").ToList();

            for (int i = 1; i < data.Length; i++)
            {
                string[] polozka = data[i].Split(";", razeni.Count);

                polozky.Add(new Polozka { ID = Convert.ToInt32(polozka[razeni.IndexOf("id")]),
                    Code = Convert.ToInt32(polozka[razeni.IndexOf("code")]),
                    Dostupnost = Convert.ToInt32(polozka[razeni.IndexOf("availability")]),
                    Akce = (polozka[razeni.IndexOf("action")] == "1"),
                    Poznamka = polozka[razeni.IndexOf("note")],
                    Cena = Convert.ToInt32(polozka[razeni.IndexOf("price")]),
                    Kategorie = polozka[razeni.IndexOf("category")],
                    UrlImg = polozka[razeni.IndexOf("imagename")],
                    Jmeno = polozka[razeni.IndexOf("name")],
                    Popis = polozka[razeni.IndexOf("description")],
                    MaxKeKoupi = polozka[razeni.IndexOf("maximal_quantity")] == "" ? 0 : Convert.ToInt32(polozka[razeni.IndexOf("maximal_quantity")])
                });
            }

            return polozky;
        }
        
    }
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSession(options => {
                options.Cookie.Name = "PoberToOnline";
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromMinutes(250);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
