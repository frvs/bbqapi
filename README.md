# bbqapi
olá! eu sou o lucas, estou fazendo uma api pra empresa trincada mais legal do país.
vou documentando aqui o pensamento, as decisões e tudo mais que eu achar relevante.
fiquem à vontade (quem for revisar) pra ler e questionar tudo.

# decisoes técnicas
- código em inglês, docs e eventuais comentários (serão removidos até o final) em pt-br, commits em inglês
-- não sei se quem vai revisar sabe/prefere em inglês a doc, por exemplo. eu preferia em inglês.

- comecei pelos models, mais especificamente Dtos
- depois (agora), vou começar a doc e o tracking pelo Git
- ordem das entregas desejadas:
	* mvp sem testes, sem validações, sem infra, em csharp
	* mvp com testes, sem validações, sem infra, em csharp
	* mvp com testes, com validações, sem infra, em csharp
	* mvp com testes, com validações, com infra, em csharp
	* mvp sem testes, sem validações, sem infra, em fsharp
	... acho que já deu pra perceber o fluxo das intenções


# requisitos
Incluir um novo churrasco com data, descrição e observações adicionais;
Adicionar e remover participantes (colocando o seu valor de contribuição);
Visualizar os detalhes do churrasco, total de participantes e valor arrecadado;

Colocar um valor sugerido por usuário de contribuição (valor com e sem bebida inclusa); - ?

# models 

bbq { date: datetime, description: string, notes: string?, persons: list<person> }
person { name: string, value: decimal }

# endpoints

GET api/barbeques/{id}
POST api/barbeques

POST api/barbeques/{id}/persons
DELETE api/barbeques/{id}/persons


valor default com e sem bebida pra pessoa que está se cadastrando pra um churrasco...