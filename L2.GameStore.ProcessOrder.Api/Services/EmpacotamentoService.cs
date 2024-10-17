namespace L2.GameStore.ProcessOrder.Services;
public class EmpacotamentoService
{
    private readonly List<Caixa> caixasDisponiveis;

    public EmpacotamentoService()
    {
        caixasDisponiveis =
        [
            new Caixa { Nome = "Caixa 1", Altura = 30, Largura = 40, Comprimento = 80 },
            new Caixa { Nome = "Caixa 2", Altura = 80, Largura = 50, Comprimento = 40 },
            new Caixa { Nome = "Caixa 3", Altura = 50, Largura = 80, Comprimento = 60 }
        ];
    }

    public PedidoResponse Empacotar(Pedido pedido)
    {
        var resultadoPedido = new PedidoResponse
        {
            PedidoId = pedido.PedidosId,
            Caixas = []
        };

        var produtosOrdenados = pedido.Produtos
                .OrderByDescending(p => CalcularVolume(p.Dimensoes))
                .ToList();

        foreach (var produto in produtosOrdenados)
        {
            bool produtoAlocado = false;

            foreach (var caixaResposta in resultadoPedido.Caixas)
            {
                var caixa = caixasDisponiveis.FirstOrDefault(c => c.Nome == caixaResposta.CaixaId);
                if (caixa != null && CabeNaCaixa(caixaResposta, caixa, produto))
                {
                    caixaResposta.Produtos.Add(produto.ProdutoId);
                    produtoAlocado = true;
                    break;
                }
            }

            if (!produtoAlocado)
            {
                var novaCaixa = AcharCaixaDisponivel(produto.Dimensoes);
                if (novaCaixa != null)
                {
                    var novaCaixaResposta = new CaixaResponse
                    {
                        CaixaId = novaCaixa.Nome,
                        Produtos = [produto.ProdutoId]
                    };
                    resultadoPedido.Caixas.Add(novaCaixaResposta);
                }
                else
                {
                    var caixaNaoAlocada = new CaixaResponse
                    {
                        CaixaId = null!,
                        Produtos = [produto.ProdutoId],
                        Observacao = $"Produto {produto.ProdutoId} não cabe em nenhuma caixa disponível."
                    };
                    resultadoPedido.Caixas.Add(caixaNaoAlocada);
                }
            }
        }

        return resultadoPedido;
    }

    private decimal CalcularVolume(Dimensao dimensoes)
    {
        return dimensoes.Altura * dimensoes.Largura * dimensoes.Comprimento;
    }

    private bool CabeNaCaixa(CaixaResponse caixaResponse, Caixa caixa, Produto produto)
    {
        if (produto.Dimensoes.Altura > caixa.Altura ||
            produto.Dimensoes.Largura > caixa.Largura ||
            produto.Dimensoes.Comprimento > caixa.Comprimento)
        {
            return false;
        }

        decimal volumeProduto = CalcularVolume(produto.Dimensoes);
        decimal volumeCaixa = caixa.Altura * caixa.Largura * caixa.Comprimento;

        decimal volumeOcupado = volumeCaixa - caixaResponse.Produtos.Sum(pId =>
        {
            var produtoExistente = caixasDisponiveis.FirstOrDefault(c => c.Nome == caixaResponse.CaixaId);
            if (produtoExistente != null)
            {
                var produtoDimensoes = produto.Dimensoes;
                return CalcularVolume(produtoDimensoes);
            }
            return 0;
        });

        decimal volumeRestante = volumeCaixa - volumeOcupado;

        return volumeProduto <= volumeRestante;
    }

    private Caixa AcharCaixaDisponivel(Dimensao dimensoes)
    {
        foreach (var caixa in caixasDisponiveis.OrderBy(c => c.Altura * c.Largura * c.Comprimento))
        {
            if (dimensoes.Altura <= caixa.Altura && dimensoes.Largura <= caixa.Largura && dimensoes.Comprimento <= caixa.Comprimento)
            {
                return caixa;
            }
        }

        return null!;
    }
}