using Microsoft.VisualBasic;
using System;
using System.Runtime.Serialization;
//lista dos vendedores
var vendedores = new List<Vendedor>
{
    new Vendedor {Id = 1 , Nome = "Caio"},

    new Vendedor {Id = 2 , Nome = "Fernando"},

    new Vendedor {Id = 3 , Nome = "João"},

    new Vendedor {Id = 4 , Nome = "Juliana"},

    new Vendedor {Id = 5 , Nome = "Vinicius"}

};

/*var produtos = new[] { "Teclado", "Mouse", "Monitor", "Caixa de Som", "HeadSet" };
for (int i = 0; i < 5; i++)
{
    var indice = Random.Shared.Next(produtos.Length);
    Console.WriteLine($"Nome Aleátório: {produtos[indice]}");
}
*/
//lista dos produtos
var produtos = new List<Produto>
{
    new Produto {Id = 1 , Nome = "Teclado", Preco = 120m, Quantidade = 12 },

    new Produto {Id = 2 , Nome = "Mouse", Preco = 100m, Quantidade = 8},

    new Produto {Id = 3 , Nome = "Monitor", Preco = 500m, Quantidade = 7},

    new Produto {Id = 4 , Nome = "Caixa de Som", Preco = 130m, Quantidade = 10},

    new Produto {Id = 5 , Nome = "HeadSet", Preco = 200m, Quantidade = 5}

};
var random = new Random();
//"estoque" (uma ideia não descartada)

var listaDeVendas = new List<Venda>();

//gerador de vendas
for (int i = 0; i < 10; i++)
{

    var vendedorAleatorio = vendedores[random.Next(vendedores.Count)];
    var quantidade = random.Next(1, 5);
    var valorTotal = 0m;

    for (int q = 0; q < quantidade; q++)
    {

        var produtoAleatorio = produtos[random.Next(produtos.Count)];
        valorTotal += produtoAleatorio.Preco;
    }


    var venda = new Venda
    {
        IdVendedor = vendedorAleatorio.Id,
        NomeVendedor = vendedorAleatorio.Nome,
        Data = DateTime.Now,
        Valor = valorTotal
    };

    listaDeVendas.Add(venda);
}
//soma

var soma = listaDeVendas
    .GroupBy(p => p.NomeVendedor)
    .Select(g => new
    {
        Nome = g.Key,
        Total = g.Sum(p => p.Valor)
    })
    .ToList();

foreach (var item in soma.OrderByDescending(p => p.Total))
{
    if (item.Total > 2000)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
    }
    if (item.Total < 2000)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
    }
    if (item.Total < 1000)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
    }
    if (item.Total < 500)
    {
        Console.ResetColor();
    }
    Console.Write($"Vendedor: {item.Nome} ||");
    Console.Write($" Valor da Venda R$: {item.Total:F2}");
    Console.WriteLine("");

    Console.ResetColor();
}

//Sistema do Ranking


Console.ReadKey();
public class Vendedor
{
    public int Id { get; set; }
    public string Nome { get; set; }
}

public class Venda
{
    public int Id { get; set; }
    public int IdVendedor { get; set; }

    public string NomeVendedor { get; set; }
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
}

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Quantidade { get; set; }

    public decimal Preco { get; set; }
}

