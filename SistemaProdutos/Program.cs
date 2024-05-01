using Microsoft.EntityFrameworkCore;
using SistemaProdutos.Data;
using SistemaProdutos.Repositorios;
using SistemaProdutos.Repositorios.Interfaces;
using Microsoft.AspNetCore.Builder;


namespace SistemaProdutos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // banco SQL Server
            //builder.Services.AddEntityFrameworkSqlServer().
            //    AddDbContext<SistemaProdutosDBContext>(
            //        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
            //    );

            //banco MySql WorkBench
            var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionStrings");
            builder.Services.AddDbContext<SistemaProdutosDBContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                //app.UseSwaggerUI();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");

                    // Desabilitar a validação de certificados
                    c.ConfigObject.ValidatorUrl = null;
                    c.ConfigObject.AdditionalItems["validatorUrl"] = null;
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // foram adicionadas essas duas linhas pois estava dando problema // Only the invariant culture is supported in globalization-invariant mode. See https://aka.ms/GlobalizationInvariantMode for more information. (Parameter 'name') en - us is an invalid culture identifier.
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;


            app.MapControllers();

            app.Run();
        }
    }
}
