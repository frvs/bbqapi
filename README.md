# bbqapi
ol�! eu sou o lucas, estou fazendo uma api pra empresa trincada mais legal do pa�s.  
vou documentando aqui o pensamento, as decis�es e tudo mais que eu achar relevante.  
fiquem � vontade (quem for revisar) pra ler e questionar tudo.  

link do swagger: https://bbqapi.herokuapp.com/swagger/index.html \o/  

## descricao do projeto


## requisitos
- dotnet sdk/runtime --version 6.0.0 ou superior
- editor como visual studio ou vscode para debugging ou review

## como rodar
a aplica��o localmente
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
Incluir um novo churrasco com data, descri��o e observa��es adicionais;  
Adicionar e remover participantes (colocando o seu valor de contribui��o);  
Visualizar os detalhes do churrasco, total de participantes e valor arrecadado;  
Colocar um valor sugerido por usu�rio de contribui��o (valor com e sem bebida inclusa;

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
dica: ```git checkout [commitId]``` para atualizar o c�digo para o estado desejado
- ```734920eb756dea22fbcff1358a0fbdc2e5a0c4d9``` initial commit: criei a estrutura b�sica de pastas 
- ```f5b248ff32a3d7f49a29b0ba54db9750cc2ff707``` adding services to barbeque controller: l�gica b�sica nos services/repositories 
- ```b4a67495bfe8c02591ff74b052b29bd04a0101c4``` yay! finishing mvp api: terminei o mvp. sem muita coisa mas aqui o b�sico tava feito
- ```a6657c83b6d62092e8d904a3a47ed208fb6238eb``` fix integration tests for person: nesse ponto os testes de integra��o estavam feitos
- ```36f7ffd9f934d5b84b6877a3caaebd792c056fd3``` add notification pattern: melhorei as valida��es e coloquei o notification pattern. aqui todos os testes (unit/integration) tavam bons.

## mais infos de review
eu fiz um di�rio de bordo em NOTES.md.  
fiquem a vontade pra dar uma lida.