## eVendas API


### Introdu��o

Este projeto foi desenvolvido como parte do programa Acelera��o Global Dev da Digital Innovation One e Avanade.  
Com o objetivo de aplicar as melhores praticas de desenvolvimento de software com conceitos de:  

    SOLID;  
    Design Patterns;  
    Arquitetura de Eventos;  
    Aplica��es utilizando Azure Service Bus.  

### Sobre o Projeto

Esta solu��o apresenta duas aplica��es web independentes, que simulam parcialmente aplica��es REST conectadas via Azure Service Bus.  
As aplica��es s�o respons�veis pelo gerenciamento de estoque e vendas de um e-commerce.

* **Controle de Estoque**  
   Realiza o cadastro, edi��o e exclus�o de produtos da base de dados.
   Possui os m�todos: 

       GET, POST, PUT, DELETE.

* **Controle de Vendas**  
   Realiza apenas a venda de produtos.
   Possui os m�todos:

       GET, PUT.

### Iniciando a aplica��o

As principais depend�ncias do projeto s�o:

    Entity Framework;  
    Azure Service Bus;  
    Data SQLite.  

**Banco de Dados**

O SQLite foi escolhido como banco de dados da aplica��o por ser suficientemente flex�vel e leve.  
Altera��es no banco podem ser feitas nos respectivos Repository Context de cada aplica��o, alterando o atributo *dbContextOptionsBuilder*

```CSharp
namespace ControleEstoque.Repository
{
   public class RepositoryContext : DbContext
   {
       public DbSet<Product> Produtos { get; set; }

       protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
       {
           dbContextOptionsBuilder.UseSqlite("Data Source=DBStorage.db");
       }
   }
}
```

As aplica��es ja contem um `Migration` inicial, para criar o arquivo do banco de dados execute para a respectiva aplica��o:

    Terminal no diret�rio raiz da aplica��o /> dotnet ef database update  
    Terminal do gerenciador de pacotes /> Update-DataBase

**Azure Service Bus**

Foi adotado um �nico t�pico para as duas aplica��es e foram criadas 2 subscri��es as quais foram atribu�das filtros
de correla��o para propriedades personalizadas nos metadados da mensagem. 

  ***controleEstroque**: substrico a `sales` (itens que foram vendidos, filtro `sale`)*   
  ***controleVendas**: subscrito a `storage` (itens criados e modificados, `filtros: storage, update e delete`)* 

![Model](docs/ModelService.png)

**AppSettings e Endpoints**

A *connection string*, *nome do t�pico* e *subscri��o* podem ser carregados diretamente no arquivo appsettings.json

```JSON
   "ServiceBus":
    {
       "ConnectionString": "Endpoint=sb://...",
       "EntityPath": "Nome do T�pico",
       "Subscription": "Nome da Subscri��o"
    }
```

O Swagger foi implementado na aplica��o sendo acess�vel a partir da URL  
    
    https://localhost:{porta}/index.html
 
As aplica��es podem ser acessadas a partir da URL

    https://localhost:{porta}/api/product

`portas: 44388 (controle de estoque), 44364 (controle de vendas)`

### Requisi��es

As requisi��es sobre o endpoint do controle de estoque devem ser realizadas segundo as orienta��es a seguir:


* O m�todo GET tem o par�metro Id opcional (se vazio requisita todos os produtos eleg�veis na base de dados);
* O par�metro Id � obrigat�rio nos m�todos PUT e DELETE
* Os m�todos POST e PUT devem conter no corpo da requisi��o o text/json com os atributos:

```JSON
    {
      "id": 0,    
      "codigo": "string",
      "nome": "string",
      "preco": 0,
      "quantidade": 0
    }
```
�*id desnecess�rio no m�todo post*


H� apenas duas requisi��es poss�veis para o servi�o de vendas.

 * O m�todo GET tem o par�metro Id opcional (se vazio requisita todos os produtos eleg�veis na base de dados);
 * O m�todo PUT representa a a��o de venda de um produto e recebe, o par�metro Id � obrigat�rio e recebe no corpo da requisi��o o text/json com os atributos:

```JSON
    {
      "id": 0,
      "quantidade":0
    }
```

#### Refer�ncias


[Documenta��o Azure](https://docs.microsoft.com/pt-br/azure/service-bus-messaging/)



#### Agradecimento 

Agradecimentos a Digital Innovation One e ao grupo de profissionais Seniores e Arquitetos da Avanade pela ciclo de palestras.