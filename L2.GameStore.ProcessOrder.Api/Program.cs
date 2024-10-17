var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<EmpacotamentoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/empacotar", (EmpacotamentoService service, PedidoRequest entradaPedidos) =>
{
    var respostaFinal = new
    {
        Pedidos = entradaPedidos.Pedidos.Select(pedido => service.Empacotar(pedido)).ToList()
    };

    return Results.Ok(respostaFinal);
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();