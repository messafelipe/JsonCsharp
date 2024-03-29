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

//Procura por autores com o nome "Clarice Lispector" dentro do objeto JSON e,
//se encontrados, retorna os livros associados a essa autora.

var books = JObject.Parse(jsonText)["authors"]?
                    .Where(x => (string?)x["name"] == "Clarice Lispector")
                    .Select(s => s["books"]);

//Cria uma nova instância de JArray, que será usada para armazenar os livros
//da "Clarice Lispector".

var clariceBooks = new JArray();

//Itera sobre os livros obtidos. Se a variável books for nula, IEnumerable<JToken>()
//é usado como uma coleção vazia.

foreach (var b in books ?? [])
{
    if (b is JArray authorBooks)
    {
        //Mescla os livros da autora.
        clariceBooks.Merge(authorBooks);
    }
}

//Converte os livros de "Clarice Lispector" em formato JSON para uma string.

var clariceJson = clariceBooks.ToString();

//Imprime a string JSON no console.

Console.WriteLine(clariceJson);

Debugger.Break();

//Criando um novo autor.

Author newAuthor = new()
{
    Id = authorList.Authors.Max(s => s.Id) + 1,
    Name = "Guimarães Rosa",
    Type = "Autor",
    Books = []
};

//Adicionando um novo autor.

authorList.Authors.Add(newAuthor);

// Serializa a lista atualizada de autores de volta para JSON.

string updatedJson = JsonConvert.SerializeObject(authorList, Formatting.Indented);

// Cria um novo arquivo JSON na pasta padrão do sistema.

string newFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NewAuthors.json");

// Escreve o JSON serializado no novo arquivo

File.WriteAllText(newFilePath, updatedJson);

Console.WriteLine($"Novo arquivo JSON criado em: {newFilePath}");