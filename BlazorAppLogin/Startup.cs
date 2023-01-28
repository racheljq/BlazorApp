using Microsoft.AspNetCore.Builder;
using System.Configuration;

namespace BlazorAppLogin
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //// ...     
            //services.AddXafWebApi(Configuration, options =>
            //{
            //    // Use options.BusinessObject<YourBusinessObject>() 
            //    // to make the Business Object available in the Web API and
            //    // generate the GET, POST, PUT, and DELETE HTTP methods for it.
            //});
            //// in XPO applications, uncomment the following line
            //// .AddXpoServices(); 
            //services.AddControllers().AddOData((options, serviceProvider) => {
            //    options
            //        .AddRouteComponents("api/odata", new EdmModelBuilder(serviceProvider).GetEdmModel())
            //        .EnableQueryFeatures(100);
            //});
            //services.AddSwaggerGen(c => {
            //    c.EnableAnnotations();
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "MySolution API",
            //        Version = "v1",
            //        Description = @"Use AddXafWebApi(Configuration, options) in the MySolution.Blazor.Server\Startup.cs file to make Business Objects available in the Web API."
            //    });
            //});
        }
    }
}
