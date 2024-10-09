using Microsoft.OpenApi.Models;

namespace Store.Web.Extensions
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    { Title = "Store Api",
                      Version = "v1" ,
                      Contact =new OpenApiContact
                        {
                            Name="Johan",
                            Email="Johan@gmail.co",
                            Url=new Uri("https://twitter.com")
                        }
                        });
                var securityScheme = new OpenApiSecurityScheme
                {
                    Description= "Jwt Authorization header using the Bearer scheme. Example :\"Authorization: Bearer{token}\"",
                    Name= "Authorization",
                    In=ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme="bearer",
                    Reference=new OpenApiReference
                    {
                        Id="bearer",
                        Type=ReferenceType.SecurityScheme
                    }
                  };
                options.AddSecurityDefinition("bearer",securityScheme);
                var securityRequriments = new OpenApiSecurityRequirement
                {
                    {securityScheme , new [] { "bearer"} }
                };
                options.AddSecurityRequirement(securityRequriments);
            });
            return services;
        }

    }
}
