using System;
using System.Collections.Generic;

namespace Aula29_POO
{
    class Program
    {
        static void Main(string[] args)
        {
             Produto p1 = new Produto();
            p1.Codigo = 6;
            p1.Nome = "Guitarra Tagima ";
            p1.Preco = 5500f;

            p1.Cadastrar(p1);
            p1.Remover("Tagima");
            

            List<Produto> lista = new List<Produto>();
            lista = p1.Ler();

            foreach(Produto item in lista){
                Console.WriteLine($"{item.Preco} - {item.Nome}");
            }
        }
    }
}
