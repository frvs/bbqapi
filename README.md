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
	 
- nao vou trabalhar com branches nesse repo. pra nao acharem que tenho maus costumes:
nao gosto de master/main/prod + development + staging (multibranching) (muito trabalho, git flow � passado)
gosto de uma branch pra prod + branches de desenvolvimento =) (github flow � massa)

- talvez eu tenha adicionado um pouco de overengineering nos Dtos, fazendo eles calcularem coisas pro front que
nao precisavam MAS: eu sigo a ideia que o front tem que ser o mais r�pido poss�vel e o back tem que entregar as 
coisas o mais mastigado poss�vel. mais pra frente eu volto nessa decis�o e reviso
(aqui me refiro as props q s�o lambdas =>)

# requisitos
Incluir um novo churrasco com data, descri��o e observa��es adicionais;  
Adicionar e remover participantes (colocando o seu valor de contribui��o);  
Visualizar os detalhes do churrasco, total de participantes e valor arrecadado;  

Colocar um valor sugerido por usu�rio de contribui��o (valor com e sem bebida inclusa); - ?  
- h� um valor de comida e um valor de bebida na hora de adicionar uma pessoa no churrasco  
- a pessoa a ser adicionada deve escolher se vai pagar somente comida, somente bebida ou ambos (e quanto)  
- o valor sugerido pode ser algo hard-coded (fixo) (ou pode ser uma m�dia...)  
pela simplicidade:  
nao precisamos do toggle, e podemos ter as duas formas de 'prediction' do valor caso n�o informado  

# models 

bbq { date: datetime, description: string, notes: string?, persons: list<person> }  
person { name: string, value: decimal, foodValue: decimal, drinksValue: decimal }  

# endpoints

```
GET api/barbeques/{id}  
POST api/barbeques  
```
```
POST api/barbeques/{id}/persons  
DELETE api/barbeques/{barbequeId}/persons/{personId}  
```