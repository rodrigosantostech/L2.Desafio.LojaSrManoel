namespace L2.GameStore.ProcessOrder.ViewModel;

public class PedidoResponse
{
    [JsonPropertyName("pedido_id")]
    public int PedidoId { get; set; }

    [JsonPropertyName("caixas")]
    public List<CaixaResponse> Caixas { get; set; } = [];
}
