namespace L2.GameStore.ProcessOrder.Domain;

public class Produto
{
    [JsonPropertyName("produto_id")]
    public string ProdutoId { get; set; } = string.Empty;
    [JsonPropertyName("dimensoes")]
    public Dimensao Dimensoes { get; set; } = new();
}