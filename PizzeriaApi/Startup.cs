using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PizzeriaApplication.ICommands;
using PizzeriaApplication.ICommands.ICommandsItem;
using PizzeriaApplication.ICommands.ICommandsItemType;
using PizzeriaApplication.ICommands.ICommandsAuth;
using PizzeriaApplication.ICommands.ICommandsOrder;
using PizzeriaApplication.ICommands.ICommandsRole;
using PizzeriaApplication.ICommands.ICommandsTable;
using PizzeriaApplication.ICommands.ICommandsAttendant;
using PizzeriaApplication.Interfaces;
using PizzeriaApplication.Login;
using PizzeriaCommands;
using PizzeriaCommands.ItemCommands;
using PizzeriaCommands.ItemTypeCommands;
using PizzeriaCommands.AuthCommands;
using PizzeriaCommands.OrderCommands;
using PizzeriaCommands.RoleCommands;
using PizzeriaCommands.TableCommands;
using PizzeriaCommands.AttendantCommands;
using PizzeriaApi.Email;
using PizzeriaApi.Helpers;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace PizzeriaApi
{
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddDbContext<PizzeriaContext>();
            services.AddTransient<IGetPizzeriaHalls, GetPizzeriaHalls>();
            services.AddTransient<IAddPizzeriaHall, AddPizzeriaHall>();
            services.AddTransient<IGetPizzeriaHall, GetPizzeriaHall>();
            services.AddTransient<IDeletePizzeriaHall, DeletePizzeriaHall>();
            services.AddTransient<IUpdatePizzeriaHall, UpdatePizzeriaHall>();
            services.AddTransient<IGetItemTypes, GetItemTypes>();
            services.AddTransient<IGetItemType, GetItemType>();
            services.AddTransient<IAddItemType, AddItemType>();
            services.AddTransient<IUpdateItemType, UpdateItemType>();
            services.AddTransient<IDeleteItemType, DeleteItemType>();
            services.AddTransient<IGetTable, GetTable>();
            services.AddTransient<IAddTable, AddTable>();
            services.AddTransient<IGetRoles, GetRoles>();
            services.AddTransient<IGetRole, GetRole>();
            services.AddTransient<IAddRole, AddRole>();
            services.AddTransient<IUpdateRole, UpdateRole>();
            services.AddTransient<IDeleteRole, DeleteRole>();
            services.AddTransient<IGetTables, GetTables>();
            services.AddTransient<IDeleteTable, DeleteTable>();
            services.AddTransient<IUpdateTable, UpdateTable>();
            services.AddTransient<IGetAttendant, GetAttendant>();
            services.AddTransient<IGetAttendants, GetAttendants>();
            services.AddTransient<IAddAttendant, AddAttendant>();
            services.AddTransient<IDeleteAttendant, DeleteAttendant>();
            services.AddTransient<IUpdateAttendant, UpdateAttendant>();
            services.AddTransient<IGetItem, GetItem>();
            services.AddTransient<IGetItems, GetItems>();
            services.AddTransient<IDeleteItem, DeleteItem>();
            services.AddTransient<IUpdateItem, UpdateItem>();
            services.AddTransient<IAddItem, AddItem>();
            services.AddTransient<IAddOrderItem, AddOrderItem>();
            services.AddTransient<IGetOrders, GetOrders>();
            services.AddTransient<IAddItemToOrder, AddItemToOrder>();
            services.AddTransient<IAddOrderAndItem, AddOrderAndItem>();
            services.AddTransient<IGetOrder, GetOrder>();
            services.AddTransient<IChangeStatus, ChangeStatus>();
            services.AddTransient<IChangeTable, ChangeTable>();
            services.AddTransient<ISubtractItemsOrder, SubtractItemsOrder>();
            services.AddTransient<IDeleteOrder, DeleteOrder>();
            services.AddTransient<IGetLoggedUser, GetLoggedUser>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddRazorPages();

            var section = Configuration.GetSection("Email");

            var sender =
                new EmailSender(section["host"], Int32.Parse(section["port"]), section["fromaddress"], section["password"]);

            services.AddSingleton<IEmailSender>(sender);

            var key = Configuration.GetSection("Encryption")["key"];

            var enc = new Encryption(key);
            services.AddSingleton(enc);
          

            services.AddTransient(s => {
                var http = s.GetRequiredService<IHttpContextAccessor>();
                var value = http.HttpContext.Request.Headers["Authorization"].ToString();
                var encryption = s.GetRequiredService<Encryption>();

                try
                {
                    var decodedString = encryption.DecryptString(value);
                    decodedString = decodedString.Substring(0, decodedString.LastIndexOf("}") + 1);
                    var user = JsonConvert.DeserializeObject<LoggedUser>(decodedString);
                    user.IsLogged = true;
                    return user;
                }
                catch (Exception)
                {
                    return new LoggedUser
                    {
                        IsLogged = false
                    };
                }
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("My_Api", new OpenApiInfo { Version = "v1", Description = "My Api" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapRazorPages();
                });
        }
    }
}