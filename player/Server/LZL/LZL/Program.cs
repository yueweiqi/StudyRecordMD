
using LZL.ProfileExtend;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Internal;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;

namespace LZL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region IOC
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region mongodb
            // 配置MongoDB连接
            var mongoConnectionString = builder.Configuration.GetValue<string>("MongoConnection:ConnectionString");
            var mongoDatabaseName = builder.Configuration.GetValue<string>("MongoConnection:Database");
            var mongoClient = new MongoClient(mongoConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseName);
            builder.Services.AddSingleton(mongoDatabase);
            #endregion

            #region AutoMapper
            builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(CustomAutoMapperProfile)));
            #endregion

            #endregion
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {   // 应用请求跨域
                    builder.SetIsOriginAllowed(_ => true).AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            var app = builder.Build();

            #region 中间件
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("any");
            #region 配置静态文件访问
            var resourcePath = Path.Combine(AppContext.BaseDirectory, "Resource");
            if(!Directory.Exists(resourcePath))
                Directory.CreateDirectory(resourcePath);
            app.UseStaticFiles(new StaticFileOptions()
            {

                FileProvider = new PhysicalFileProvider(resourcePath),
                RequestPath = "/Resource"
            });
            //app.UseStaticFiles(new StaticFileOptions() { 
               
            //     FileProvider=new PhysicalFileProvider(resourcePath+ "/Team/Avater"),
            //     RequestPath= "/Resource/Team/Avater"
            //});
            #endregion
            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
