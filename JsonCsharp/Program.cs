// See https://aka.ms/new-console-template for more information
using JsonCsharp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

//Cria uma variável filePath para armazenar o caminho completo do arquivo JSON.
//A função Path.Combine combina os diretórios "Json" e o nome do arquivo "Authors.json" formando o caminho completo.

string filePath = Path.Combine("Json", "Authors.json");

//Lê o conteúdo do arquivo JSON especificado pelo caminho filePath e armazena o texto JSON na variável jsonText.

string jsonText = File.ReadAllText(filePath);

//Deserializa o texto JSON em um objeto do tipo AuthorListContainer.
//A biblioteca JsonConvert.DeserializeObject converte o JSON em um objeto C#.
//O operador ?? (null-coalescing) garante a criação de um objeto vazio se a desserialização falhar.

AuthorListContainer authorList = JsonConvert.DeserializeObject<AuthorListContainer>(jsonText) ?? new AuthorListContainer();

//Procura o primeiro autor com o nome "Machado de Assis" na lista de autores dentro de authorList.
//A função FirstOrDefault retorna o primeiro elemento que satisfaz a condição, ou null se nenhum for encontrado.

var author = authorList.Authors.FirstOrDefault(s => s.Name == "Machado de Assis");

//Inicia um loop foreach para percorrer a lista de livros do autor encontrado (author.Books).
//Escreve no console o nome de cada livro (book.Name) durante a execução do loop.

foreach (var book in author?.Books ?? Enumerable.Empty<Book>())
{
    Console.WriteLine(book.Name);
}

//Interrompe a execução do programa.
Debugger.Break();
