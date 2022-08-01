# bbqapi
olá! eu sou o lucas, estou fazendo uma api pra empresa trincada mais legal do país.  
vou documentando aqui o pensamento, as decisões e tudo mais que eu achar relevante.  
fiquem à vontade (quem for revisar) pra ler e questionar tudo.  

link do swagger: https://bbqapi.herokuapp.com/swagger/index.html \o/  

## descricao do projeto


## requisitos
- dotnet sdk/runtime --version 6.0.0 ou superior
- editor como visual studio ou vscode para debugging ou review

## como rodar
a aplicação localmente
- ```git clone https://github.com/frvs/bbqapi.git```
- ```cd bbqapi/BarbequeApi```
- ```dotnet restore```
- ```dotnet build```
- ```dotnet run```
- abrir https://localhost:7195/swagger/index.html e fazer suas requests pelo swagger (ou postman/insomnia/curl/etc)

os testes  
- ```git clone https://github.com/frvs/bbqapi.git```
- ```cd bbqapi/BarbequeApi.Tests```
- ```dotnet restore```
- ```dotnet build```
- ```dotnet test```

## requisitos de desenvolvimento
Incluir um novo churrasco com data, descrição e observações adicionais;  
Adicionar e remover participantes (colocando o seu valor de contribuição);  
Visualizar os detalhes do churrasco, total de participantes e valor arrecadado;  
Colocar um valor sugerido por usuário de contribuição (valor com e sem bebida inclusa;

## modelos 

bbq { date: datetime, title: string, notes: string?, persons: list<person> }  
person { name: string, value: decimal, foodValue: decimal, drinksValue: decimal }  

## endpoints

```
GET api/barbeques/{id}  
POST api/barbeques  
```
```
POST api/barbeques/{id}/persons  
DELETE api/barbeques/{barbequeId}/persons/{personId}  
```

## principais commits pra revisar
dica: ```git checkout [commitId]``` para atualizar o código para o estado desejado
- ```734920eb756dea22fbcff1358a0fbdc2e5a0c4d9``` initial commit: criei a estrutura básica de pastas 
- ```f5b248ff32a3d7f49a29b0ba54db9750cc2ff707``` adding services to barbeque controller: lógica básica nos services/repositories 
- ```b4a67495bfe8c02591ff74b052b29bd04a0101c4``` yay! finishing mvp api: terminei o mvp. sem muita coisa mas aqui o básico tava feito
- ```a6657c83b6d62092e8d904a3a47ed208fb6238eb``` fix integration tests for person: nesse ponto os testes de integração estavam feitos
- ```36f7ffd9f934d5b84b6877a3caaebd792c056fd3``` add notification pattern: melhorei as validações e coloquei o notification pattern. aqui todos os testes (unit/integration) tavam bons.

## mais infos de review
eu fiz um diário de bordo em NOTES.md.  
fiquem a vontade pra dar uma lida.