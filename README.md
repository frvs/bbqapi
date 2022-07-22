# bbqapi
ol�! eu sou o lucas, estou fazendo uma api pra empresa trincada mais legal do pa�s.
vou documentando aqui o pensamento, as decis�es e tudo mais que eu achar relevante.
fiquem � vontade (quem for revisar) pra ler e questionar tudo.

# decisoes t�cnicas
- c�digo em ingl�s, docs e eventuais coment�rios (ser�o removidos at� o final) em pt-br, commits em ingl�s
-- n�o sei se quem vai revisar sabe/prefere em ingl�s a doc, por exemplo. eu preferia em ingl�s.

- comecei pelos models, mais especificamente Dtos
- depois (agora), vou come�ar a doc e o tracking pelo Git
- ordem das entregas desejadas:
	* mvp sem testes, sem valida��es, sem infra, em csharp
	* mvp com testes, sem valida��es, sem infra, em csharp
	* mvp com testes, com valida��es, sem infra, em csharp
	* mvp com testes, com valida��es, com infra, em csharp
	* mvp sem testes, sem valida��es, sem infra, em fsharp
	... acho que j� deu pra perceber o fluxo das inten��es


# requisitos
Incluir um novo churrasco com data, descri��o e observa��es adicionais;
Adicionar e remover participantes (colocando o seu valor de contribui��o);
Visualizar os detalhes do churrasco, total de participantes e valor arrecadado;

Colocar um valor sugerido por usu�rio de contribui��o (valor com e sem bebida inclusa); - ?

# models 

bbq { date: datetime, description: string, notes: string?, persons: list<person> }
person { name: string, value: decimal }

# endpoints

GET api/barbeques/{id}
POST api/barbeques

POST api/barbeques/{id}/persons
DELETE api/barbeques/{id}/persons


valor default com e sem bebida pra pessoa que est� se cadastrando pra um churrasco...