# ApiBloomFull

> API exemplo construída com ASP.NET Core, Entity Framework Core, FluentValidation e testes unitários com xUnit.

## 📁 Estrutura da Solution

Solution Full
├─ Bloom.Api
├─ Bloom.Data
├─ Bloom.Negocio
└─ Bloom.Tests

## 🚀 Como rodar

### Pré-requisitos
- [.NET SDK 8.0](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB)


### Configurar connection string
Edite Bloom.Api/appsettings.Development.json:

json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BloomDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}

### Aplicar migrations

Update-Database ou dotnet ef database update

🚀 Decisões tomadas

### DI nativa

Utilizado Microsoft.Extensions.DependencyInjection:
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();

### Persistência EF Core (Validação com Coluna Computada)

Coluna computada Nome_Normalizado = UPPER([Nome]).

Índice único (CategoriaId, Nome_Normalizado) para garantir unicidade case-insensitive.

### Validações

FluentValidation na entidade Produto:

Nome obrigatório (2 a 100 caracteres).

PrecoUnitario >= 0.

### Tratamento de Mensagens

INotificador para mensagens de negócio e notificar toda a aplicação com as mensagens tratadas.

### Testes com xUnit

Decidi usar o xUnit pela facilidade, desde o .NET Core, a Microsoft sugere o uso de xUnit nos exemplos oficiais e na própria documentação.

