namespace L2.GameStore.ProcessOrder.Domain;

public class Dimensao
{
    [JsonPropertyName("altura")]
    public decimal Altura { get; set; }

    [JsonPropertyName("largura")]
    public decimal Largura { get; set; }

    [JsonPropertyName("comprimento")]
    public decimal Comprimento { get; set; }
}
