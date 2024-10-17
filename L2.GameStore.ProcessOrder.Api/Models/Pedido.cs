namespace L2.GameStore.ProcessOrder.Domain;

public class Pedido
{
    [JsonPropertyName("pedido_id")]
    public int PedidosId { get; set; }
    [JsonPropertyName("produtos")]
    public List<Produto> Produtos { get; set; } = [];
}
