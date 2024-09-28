using Contact37.Infrastructure;
using Contact37.Persistence;
using Contacts37.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// #nota: Aqui s�o adicionados os ConfigureServices das outras camadas
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigurePersistenceServices(builder.Configuration);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// #nota: Aqui � definida a pol�tica de permiss�o de acesso externo (ex:IP) � API. Est� liberando tudo
builder.Services.AddCors(o =>
{
	o.AddPolicy("CorsPolicy",
		builder => builder.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//#dica: Aqui define que ir� usar controle de acesso (usuario/perfil) � API
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();
