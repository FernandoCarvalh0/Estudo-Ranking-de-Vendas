using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Runtime.Serialization;
//lista dos vendedores
var vendedores = new List<Vendedor>
{
    new Vendedor {Id = 1 , Nome = "Caio"},

    new Vendedor {Id = 2 , Nome = "Fernando"},

    new Vendedor {Id = 3 , Nome = "João"},

    new Vendedor {Id = 4 , Nome = "Juliana"},

    new Vendedor {Id = 5 , Nome = "André"},

    new Vendedor {Id = 6 , Nome = "Joaquim"},

    new Vendedor {Id = 7 , Nome = "Luiz"},

    new Vendedor {Id = 8 , Nome = "Isabela"},

    new Vendedor {Id = 9 , Nome = "Thiago"},

    new Vendedor {Id = 10 , Nome = "Vinicius"}

};

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
 
    {
        for (int q = 0; q < quantidade; q++)
        {

            var produtoAleatorio = produtos[random.Next(produtos.Count)];
            valorTotal += produtoAleatorio.Preco;
        }
    }
  
    var venda = new Venda
    {
        IdVendedor = vendedorAleatorio.Id,
        NomeVendedor = vendedorAleatorio.Nome,
        Data = DateTime.Now,
        Valor = valorTotal,
        RankVendedor = vendedorAleatorio.Rank
    };

    listaDeVendas.Add(venda);
}

//chance do vendedor estar sem vendas
for (int i = 0; i < 5; i++)
{
    var vendedorAleatorio = vendedores[random.Next(vendedores.Count)];
    var valorTotal = 0m;

    var venda = new Venda
    {
        IdVendedor = vendedorAleatorio.Id,
        NomeVendedor = vendedorAleatorio.Nome,
        Data = DateTime.Now,
        Valor = valorTotal,
        
    };

    listaDeVendas.Add(venda);
}

//soma

var soma = listaDeVendas
    .GroupBy(p => p.NomeVendedor)
    .Select(g => new Venda
    {
        NomeVendedor = g.Key,
        Valor = g.Sum(p => p.Valor),
        RankVendedor = g.Key
    })
    .ToList();

//Sistema do Ranking
foreach (var item in soma)
{
    

    if (item.Valor > 2000)
    {
        item.RankVendedor = "Ouro";
    }
    if (item.Valor < 2000)
    {
        item.RankVendedor = "Prata";
    }
    if (item.Valor < 1000)
    {
        item.RankVendedor = "Bronze";
    }
    if (item.Valor < 500)
    {
        item.RankVendedor = "Ferro";
    }
}


Console.WriteLine("  ---------");
Console.WriteLine("|| Vendas ||");
Console.WriteLine("  ---------");
Console.WriteLine("");

foreach (var item in soma.OrderByDescending(p => p.Valor))
{
    Console.WriteLine($"Vendedor: {item.NomeVendedor}");
    Console.WriteLine($"Valor da Venda R$: {item.Valor:F2}");
    Console.WriteLine($"Rank: {item.RankVendedor}");

    Console.WriteLine("");
}

Console.WriteLine("");
Console.WriteLine("  ----------||-----------");
Console.WriteLine("|| Vendedores sem Vendas ||");
Console.WriteLine("  ----------||-----------");
Console.WriteLine("");
foreach (var item in soma)
{
    if (item.Valor == 0)
    {
        Console.WriteLine($"{item.NomeVendedor}");
    }
}

Console.ReadKey();
public class Vendedor
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public string Rank { get; set; }
}

public class Venda
{
    public int Id { get; set; }
    public int IdVendedor { get; set; }

    public string NomeVendedor { get; set; }
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }

    public string RankVendedor { get; set; }

}
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }

        public decimal Preco { get; set; }
    }

