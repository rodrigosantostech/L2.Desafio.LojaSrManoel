# Desafio de Empacotamento de Itens

## Descrição

Este repositório contém uma solução desenvolvida para o desafio de empacotamento de itens em caixas, utilizando **.NET 8**, **C# 12** e **Docker**. A estratégia adotada foi a utilização de **Minimal API** para resolver o problema de forma direta e objetiva, sem a necessidade de camadas extras como negócios ou infraestrutura.

## Tecnologias Utilizadas

- **.NET 8**
- **C# 12**
- **Docker**

## Pacotes Utilizados

- **XUnit**: Para testes unitários.
- **Fluent Assertions**: Para garantir uma sintaxe fluida nos testes e facilitar a leitura.

## Estrutura do Projeto

A estratégia adotada foi a de uma **API mínima**, onde a lógica de programação é o foco. A aplicação busca otimizar o uso de caixas, ordenando os itens por volume e dimensões, garantindo que o empacotamento seja o mais eficiente possível.

### Lógica de Resolução

1. **Ordenação por Volume**: O primeiro passo é ordenar os itens pelo volume, para selecionar a caixa para o item que ocupa mais espaço.
2. **Testar Itens**: Cada item é testado para verificar se cabe em uma caixa já em uso, considerando tanto as dimensões quanto o volume.
3. **Verificar Caixas Disponíveis**: Se o item não couber na caixa atual ou se for a primeira caixa, o sistema verifica se há uma caixa disponível que comporte o item.

### Fluxo de Empacotamento

- Ordena os itens por volume.
- Tenta alocar o item em uma caixa já utilizada, verificando as dimensões e o volume.
- Se não couber em nenhuma caixa existente, busca uma nova caixa disponível.

## Problemas Identificados no Desafio

- **Pedido_Id = 6**: O volume total do pedido é de 64.600 cm³. O JSON de saída sugere dividir o pedido em duas caixas, mas é possível otimizar e alocar tudo na **caixa 3**, que possui o volume de **240.000 cm³**. Nenhum item ultrapassa as dimensões da caixa 3.
  
- **Pedido_Id = 4**: O item **Teclado Mecânico** é enviado com dimensões **4*45*15 cm** no input, mas a caixa 1 tem dimensões **30*40*80 cm**. O sistema alocou o item na caixa 1 no JSON de saída, mas ele não cabe. A solução correta seria alocar o item na **caixa 2**.

## Como Rodar o Projeto

### Requisitos

- **.NET 8**
- **Docker**

### Passos para Rodar Localmente

1. Clone o repositório:

    ```bash
    git clone https://github.com/seu-usuario/desafio-empacotamento.git
    cd desafio-empacotamento
    ```

2. Restaurar as dependências do projeto:

    ```bash
    dotnet restore
    ```

3. Rodar o projeto:

    ```bash
    dotnet run
    ```

### Rodar os Testes

1. Para rodar os testes unitários, use o comando:

    ```bash
    dotnet test
    ```

## Conclusão

A estratégia de **Minimal API** foi escolhida para atender de forma simples e eficaz ao desafio. A solução pode ser expandida conforme necessário, mas atende bem ao problema proposto de forma enxuta e otimizada.
