namespace L2.GameStore.ProcessOrder.ViewModel;

public class CaixaResponse
{
    [JsonPropertyName("caixa_id")]
    public string CaixaId { get; set; } = string.Empty;
    [JsonPropertyName("produtos")]
    public List<string> Produtos { get; set; } = [];
    [JsonPropertyName("observacao")]
    public string Observacao { get; set; } = default!;
}
