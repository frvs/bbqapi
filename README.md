# bbqapi
olá! eu sou o lucas, estou fazendo uma api pra empresa trincada mais legal do país.  
vou documentando aqui o pensamento, as decisões e tudo mais que eu achar relevante.  
fiquem à vontade (quem for revisar) pra ler e questionar tudo.  

link do swagger: https://bbqapi.herokuapp.com/swagger/index.html \o/  

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
	 
- nao vou trabalhar com branches nesse repo. pra nao acharem que tenho maus costumes:
nao gosto de master/main/prod + development + staging (multibranching) (muito trabalho, git flow é chato)
gosto de uma branch pra prod + branches de desenvolvimento =) (github flow é massa)

- talvez eu tenha adicionado um pouco de overengineering nos Dtos, fazendo eles calcularem coisas pro front que
nao precisavam MAS: eu sigo a ideia que o front tem que ser o mais rápido possível e o back tem que entregar as 
coisas o mais mastigado possível (pelo seu poder de performance). mais pra frente eu volto nessa decisão e reviso
(aqui me refiro as props q são lambdas =>)

- nota mental: minimal api é horrível.

- é engraçado que o código de desafio técnicos é sempre muito diferente de código da vida real.
sobre a divisão de pastas, pra desafios técnicos eu preferiria fazer tudo na controller, mas opto por services/repositories por passar uma impressão melhor.
em projetos reais, eu prefiro queries/commands nos repositórios (se necessário), pois acho a organização melhor (e escala melhor) =)
... também, eu sempre fico em dúvida: abusar de libs pra ir mais rápido ou fazer tudo na mão em desafios pq eu nao preciso de libs? 
aqui, vou começar fazendo as coisas na mão (translators e validators).

- nota: estou *tentando* deixar os commits organizados. o histórico poderá ser acompanhado por lá.

- estou optando pelo banco inmemory por enquanto. talvez depois eu coloque sqlserver num container ou algo assim.
- efcore faz os relacionamentos 1:n automagicamente, ef6 precisa do onconfiguring

- yay! terminei o mvp. nesse momento eu optei por não tratar os erros adequadamente, só fazer funcionar.
agora nas validacoes eu posso usar o fluent, flunt ou ir na mão.
pros testes, primeiro integração e depois unidade.
além disso, está no roadmap deploy (heroku ou azure) e trocar o banco de inmemory pra sqlite ou sqlserver

# requisitos
Incluir um novo churrasco com data, descrição e observações adicionais;  
Adicionar e remover participantes (colocando o seu valor de contribuição);  
Visualizar os detalhes do churrasco, total de participantes e valor arrecadado;  

Colocar um valor sugerido por usuário de contribuição (valor com e sem bebida inclusa); - ?  
- há um valor de comida e um valor de bebida na hora de adicionar uma pessoa no churrasco  
- a pessoa a ser adicionada deve escolher se vai pagar somente comida, somente bebida ou ambos (e quanto)  
- o valor sugerido pode ser algo hard-coded (fixo) (ou pode ser uma média...)  
pela simplicidade:  
nao precisamos do toggle, e podemos ter as duas formas de 'prediction' do valor caso não informado  

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