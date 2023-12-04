using DesafioTecnico.Business;
using DesafioTecnico.Business.Commons;
using DesafioTecnico.WebAPI.Commons;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Desafio Técnico", Version = "v1" });
    options.IgnoreObsoleteActions();
    options.IgnoreObsoleteProperties();
    options.OrderActionsBy(c => c.HttpMethod);

    var documentacoes = Directory.GetFiles(AppContext.BaseDirectory, "DOC_*.xml", searchOption: SearchOption.TopDirectoryOnly);
    foreach (var documentacao in documentacoes)
        options.IncludeXmlComments(documentacao, includeControllerXmlComments: true);
});
builder.Services.RegistrarIoC();
builder.Services.AddScoped<IAutenticacaoProvider, AutenticacaoProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<AutenticacaoMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthorization();
app.MapControllers();
app.Run();
