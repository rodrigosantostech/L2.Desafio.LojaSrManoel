using FluentAssertions;
using L2.GameStore.ProcessOrder.Domain;
using L2.GameStore.ProcessOrder.Services;

namespace L2.GameStore.ProcessOrder.Tests;
public class EmpacotamentoServiceTests
{
    private readonly EmpacotamentoService _service;

    public EmpacotamentoServiceTests()
    {
        _service = new EmpacotamentoService();
    }

    [Fact]
    public void Empacotar_DeveEmpacotarProdutosEmUmaCaixa()
    {
        // Arrange
        var pedido = new Pedido
        {
            PedidosId = 1,
            Produtos =
            [
                new Produto
                {
                    ProdutoId = "PS5",
                    Dimensoes = new Dimensao { Altura = 20, Largura = 10, Comprimento = 30 }
                },
                new Produto
                {
                    ProdutoId = "Volante",
                    Dimensoes = new Dimensao { Altura = 15, Largura = 25, Comprimento = 10 }
                }
            ]
        };

        // Act
        var resultado = _service.Empacotar(pedido);

        // Assert
        resultado.Caixas.Should().HaveCount(1);
        resultado.Caixas[0].Produtos.Should().Contain(new List<string> { "PS5", "Volante" });
        resultado.Caixas[0].CaixaId.Should().Be("Caixa 1");
    }

    [Fact]
    public void Empacotar_DeveEmpacotarPriorizandoOComMaiorVolumePrimeiro()
    {
        // Arrange
        var pedido = new Pedido
        {
            PedidosId = 6,
            Produtos =
        [
            new Produto
            {
                ProdutoId = "Notebook",
                Dimensoes = new Dimensao { Altura = 2, Largura = 35, Comprimento = 25 }
            },
            new Produto
            {
                ProdutoId = "Webcam",
                Dimensoes = new Dimensao { Altura = 7, Largura = 10, Comprimento = 5 }
            },
            new Produto
            {
                ProdutoId = "Monitor",
                Dimensoes = new Dimensao { Altura = 50, Largura = 60, Comprimento = 20 }
            },
            new Produto
            {
                ProdutoId = "Microfone",
                Dimensoes = new Dimensao { Altura = 25, Largura = 10, Comprimento = 10 }
            }
        ]
        };

        // Act
        var resultado = _service.Empacotar(pedido);

        // Assert
        resultado.Caixas.Should().HaveCount(1);

        resultado.Caixas[0].Produtos.Should().Contain(["Monitor", "Webcam", "Notebook", "Microfone"]);
        resultado.Caixas[0].CaixaId.Should().Be("Caixa 3");
    }

    [Fact]
    public void Empacotar_DeveEmpacotarEmMultiplasCaixas()
    {
        // Arrange
        var pedido = new Pedido
        {
            PedidosId = 6,
            Produtos =
        [
            new Produto
            {
                ProdutoId = "Mesa Gamer",
                Dimensoes = new Dimensao { Altura = 70, Largura = 50, Comprimento = 40 }
            },
            new Produto
            {
                ProdutoId = "Cadeira Gamer",
                Dimensoes = new Dimensao { Altura = 80, Largura = 50, Comprimento = 40 }
            },
            new Produto
            {
                ProdutoId = "Monitor",
                Dimensoes = new Dimensao { Altura = 50, Largura = 60, Comprimento = 20 }
            },
            new Produto
            {
                ProdutoId = "Mouse",
                Dimensoes = new Dimensao { Altura = 5, Largura = 5, Comprimento = 5 }
            }
        ]
        };

        // Act
        var resultado = _service.Empacotar(pedido);

        // Assert
        resultado.Caixas.Should().HaveCount(2);

        resultado.Caixas[1].Produtos.Should().Contain(["Monitor"]);
        resultado.Caixas[1].CaixaId.Should().Be("Caixa 3");


        resultado.Caixas[0].Produtos.Should().Contain(["Cadeira Gamer", "Mesa Gamer", "Mouse"]);
        resultado.Caixas[0].CaixaId.Should().Be("Caixa 2");
    }

    [Fact]
    public void Empacotar_DeveRetornarObservacaoSeProdutoNaoCabeEmNenhumaCaixa()
    {
        // Arrange
        var pedido = new Pedido
        {
            PedidosId = 5,
            Produtos =
        [
            new Produto
            {
                ProdutoId = "Cadeira Gamer",
                Dimensoes = new Dimensao { Altura = 200, Largura = 100, Comprimento = 100 }
            }
        ]
        };

        // Act
        var resultado = _service.Empacotar(pedido);

        // Assert
        resultado.Caixas.Should().HaveCount(1);
        resultado.Caixas[0].CaixaId.Should().BeNull();
        resultado.Caixas[0].Produtos.Should().Contain("Cadeira Gamer");
        resultado.Caixas[0].Observacao.Should().Be("Produto Cadeira Gamer não cabe em nenhuma caixa disponível.");
    }

    [Fact]
    public void Empacotar_DeveEmpacotarAlgunsProdutosENaoEmpacotarOutros()
    {
        // Arrange
        var pedido = new Pedido
        {
            PedidosId = 7,
            Produtos =
        [
            new Produto
            {
                ProdutoId = "Monitor",
                Dimensoes = new Dimensao { Altura = 50, Largura = 50, Comprimento = 30 }
            },
            new Produto
            {
                ProdutoId = "Cadeira Gamer",
                Dimensoes = new Dimensao { Altura = 200, Largura = 100, Comprimento = 100 }
            }
        ]
        };

        // Act
        var resultado = _service.Empacotar(pedido);

        // Assert
        resultado.Caixas.Should().HaveCount(2);

        resultado.Caixas[1].Produtos.Should().Contain("Monitor");
        resultado.Caixas[1].CaixaId.Should().Be("Caixa 2");

        resultado.Caixas[0].Produtos.Should().Contain("Cadeira Gamer");
        resultado.Caixas[0].CaixaId.Should().BeNull();
        resultado.Caixas[0].Observacao.Should().Be("Produto Cadeira Gamer não cabe em nenhuma caixa disponível.");
    }
}
