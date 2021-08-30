using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PizzeriaApplication.ICommands.ICommandsItem;
using PizzeriaApplication.ICommands.ICommandsItemType;
using PizzeriaApplication.ICommands.ICommandsOrder;
using PizzeriaApplication.ICommands.ICommandsTable;
using PizzeriaApplication.ICommands.ICommandsAttendant;
using PizzeriaCommands.ItemCommands;
using PizzeriaCommands.ItemTypeCommands;
using PizzeriaCommands.OrderCommands;
using PizzeriaCommands.TableCommands;
using PizzeriaCommands.AttendantCommands;

namespace PizzeriaMVC
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddDbContext<PizzeriaContext>();
            services.AddTransient<IGetItems, GetItems>();
            services.AddTransient<IGetItem, GetItem>();
            services.AddTransient<IGetItemTypes, GetItemTypes>();
            services.AddTransient<IAddItem, AddItem>();
            services.AddTransient<IUpdateItem, UpdateItem>();
            services.AddTransient<IDeleteItem, DeleteItem>();
            services.AddTransient<IGetOrders, GetOrders>();
            services.AddTransient<IGetOrder, GetOrder>();
            services.AddTransient<IGetTables, GetTables>();
            services.AddTransient<IGetAttendants, GetAttendants>();
            services.AddTransient<IAddOrderItem, AddOrderItem>();
            services.AddTransient<IAddOrderAndItem, AddOrderAndItem>();
            services.AddTransient<IAddItemToOrder, AddItemToOrder>();
            services.AddTransient<IChangeTable, ChangeTable>();
            services.AddTransient<ISubtractItemsOrder, SubtractItemsOrder>();
            services.AddTransient<IDeleteOrder, DeleteOrder>();
            services.AddTransient<IChangeStatus, ChangeStatus>();
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
