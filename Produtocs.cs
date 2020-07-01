using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aula29_POO
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }

        private const string PATH = "Database/produto.csv";

        public Produto()
        {
            
            string pasta = PATH.Split('/')[0];

            if(!Directory.Exists(pasta)){
                Directory.CreateDirectory(pasta);
            }
            

            if(!File.Exists(PATH))
            {
                File.Create(PATH).Close();
            }
        }

        // <summary>
        /// Cadastra um produto
        /// </summary>
        /// <param name="prod">Objeto Produto</param>
        public void Cadastrar(Produto prod)
        {
            var linha = new string[] { PrepararLinha(prod) };
            File.AppendAllLines(PATH, linha);
        }

         /// <summary>
         /// Lê o CSV
         /// </summary>
         /// <returns></returns>
         public List<Produto> Ler(){

         //Criar lista de Produtos
         List<Produto> produtos = new List<Produto>();

         // Transformas as linhas em array de strings
         string[] linhas = File.ReadAllLines(PATH);

         //Varremos o array de strings
         foreach(var linha in linhas){
         //Quebramos cada linha em partes
         string[] dados = linha.Split(";");

         //Tratamos os dadso e adicionamos em um novo produto
          Produto prod = new Produto();
          prod.Codigo = Int32.Parse(Separar(dados[0]));
          prod.Nome = Separar(dados[1]);
          prod.Preco = float.Parse(Separar(dados[2]));

          produtos.Add(prod);

         }

          produtos = produtos.OrderBy(y => y.Nome).ToList();

          return produtos;
        }

          /// <summary>
          /// Remove uma ou mais linhas que contém um termo
          /// </summary>
          /// <param name="_termo"></param>
          public void Remover(string _termo){
          //Criamos uma lista que servirá como uma espécie de backup par as linhas do CSV

          List<string> linhas = new List<string>();
          
          //Utilizamos a biblioteca StreamReader para ler nosso CSV

          using(StreamReader arquivo = new StreamReader(PATH)){
 
          //Criou uma variável e colocou num laço para ler todas as linhas do .csv, e não ler apenas uma linha
          string linha;
          while((linha = arquivo.ReadLine())!=null){

          linhas.Add(linha);
            }
          }

          // Removemos as linhas que tiverem o termo passado como argumento
          linhas.RemoveAll(l => l.Contains(_termo));

          // Reescrevos nosso .csv do zero
          using(StreamWriter output = new StreamWriter(PATH)){
              
              foreach(string line in linhas){
                  output.Write(line + "\n");
              }
          }
        }  

          public List<Produto> Filtrar(string _nome){
          return Ler().FindAll(x => x.Nome == _nome);
          }
          /// <summary>
          /// Separa os dados
          /// </summary>
          /// <param name="_coluna"></param>
          /// <returns></returns>
          private string Separar(string _coluna){
          return _coluna.Split("=")[1];
        }

          /// <summary>
          /// Retorna texto
          /// </summary>
          /// <param name="p"></param>
          /// <returns></returns>


        private string PrepararLinha(Produto p)
        {
            return $"codigo={p.Codigo};nome={p.Nome};preco={p.Preco}";
        }

    }
}