
using DatingApp.Data;
using DatingApp.Interfaces;
using DatingApp.Middleware;
using DatingApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DatingApp
{
    public class Program
    {
        public static  void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            //todo add core
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
		
            //add cores
             builder.Services.AddCors();
             //database
            builder.Services.AddDbContext<DataContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                );


            //repositry services
            builder.Services.AddScoped<IUserRepository,UserRepository>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //token services
            #region TokenConfiguration
            builder.Services.AddScoped<ITokenService, TokenService>();
            // token validation
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>

            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            }
            ); 
            #endregion



            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();

            //Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
               {
                   app.UseSwagger();
                   app.UseSwaggerUI();
               }
   




            app.UseHttpsRedirection();
            app.UseCors(builder=> builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();




            app.Run();
        }
    }
}