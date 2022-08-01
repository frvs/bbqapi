# notes

ol�! por aqui vou deixar o que eu fui anotando conforme o desenvolvimento  
isso envolve pensamentos, decis�es t�cnicas, tradeoffs e tudo mais.
fiquem a vontade pra perguntar.

## decisoes t�cnicas
- c�digo e commits em ingl�s, docs e eventuais coment�rios (ser�o removidos at� o final) em pt-br    
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
nao gosto de master/main/prod + development + staging (multibranching) (muito trabalho, git flow � chato)
gosto de uma branch pra prod + branches de desenvolvimento =) (github flow � massa)

- talvez eu tenha adicionado um pouco de overengineering nos Dtos, fazendo eles calcularem coisas pro front que
nao precisavam MAS: eu sigo a ideia que o front tem que ser o mais r�pido poss�vel e o back tem que entregar as 
coisas o mais mastigado poss�vel (pelo seu poder de performance). mais pra frente eu volto nessa decis�o e reviso
(aqui me refiro as props q s�o lambdas =>)

- nota mental: minimal api � horr�vel.

- � engra�ado que o c�digo de desafio t�cnicos � sempre muito diferente de c�digo da vida real.
sobre a divis�o de pastas, pra desafios t�cnicos eu preferiria fazer tudo na controller, mas opto por services/repositories por passar uma impress�o melhor.
em projetos reais, eu prefiro queries/commands nos reposit�rios (se necess�rio), pois acho a organiza��o melhor (e escala melhor) =)
... tamb�m, eu sempre fico em d�vida: abusar de libs pra ir mais r�pido ou fazer tudo na m�o em desafios pq eu nao preciso de libs? 
aqui, vou come�ar fazendo as coisas na m�o (translators e validators).

- nota: estou *tentando* deixar os commits organizados. o hist�rico poder� ser acompanhado por l�.

- estou optando pelo banco inmemory por enquanto. talvez depois eu coloque sqlserver num container ou algo assim.
- efcore faz os relacionamentos 1:n automagicamente, ef6 precisa do onconfiguring

- yay! terminei o mvp. nesse momento eu optei por n�o tratar os erros adequadamente, s� fazer funcionar.
agora nas validacoes eu posso usar o fluent, flunt ou ir na m�o.
pros testes, primeiro integra��o e depois unidade.
al�m disso, est� no roadmap deploy (heroku ou azure) e trocar o banco de inmemory pra sqlite ou sqlserver

- fiz primeiro deploy, amanh� fa�o os testes. ter�a valida��es e quarta troco o banco. ser�?

- pr�ximos passos, em ordem: valida��es, refactoring de tudo (visando legibilidade e reaproveitamento), mudar a infra do banco (da app e de testes).
depois, se der tempo, front e melhorias de infra (pensando aqui se vale a pena colocar isso no azure pra fazer algo mais 'requintado').
btw apesar de gostar mt de TDD, nao estou utilizando. primeiro mudo a impl., depois o teste. =(

decidi por nao utilizar interfaces pros validators e foi bom pq poupou tempo nos testes sem ter q fazer mock. bom tradeoff 
(e ainda deu pra brincar com abstract class)
os testes de unidade ajudaram bastante a achar pequenos bugs.

- error handler no personservice, testes unit�rios dos servicos (fazer de person e revisar de barbeque) => postergar,  teste unit�rio do dto (=>) done, 
revisar namings, strings e geralzao (pq o indent=2 n funciona mais?). depois voltar pra => legibilidade e reaproveitamento), mudar a infra do banco

- naming review
barbeque => barbecue?
(deixar o banco shared entre os testes est� os tornando intermitentes) => resolvido
queria mt colocar sqlserver/postgres mas da� precisaria de um aws/azure free tier. entre sqlite e inmemory, tanto faz, inmemory pela praticidade.
acho que vou dar o teste como done hoje (sexta), s�bado fa�o o front e domingo reviso os testes do back.
fazer readme e notes.md no domingo tamb�m (feito)
redirect de /  pra /swagger (feito)
spaces de 4 pra 2, fazer pelo vscode

- acabou que nao tive sabado ou domingo pra mexer no teste.  
o tempo que eu tive foi: de 20/07 a 31/07, 11 dias. desses 11 dias, eu s� programei em 4 de fato (e no per�odo da noite), devido a trabalho corrido e compromissos pessoais.
queria ter entregado mais mas hoje (01/08), vou fazer uns ajustes finais e enviar.
vou ficar devendo coisas como: migrations, alguns endpoints extras, infra em sqlserver, infra em provider famoso (aws/azure) com pipeline, fazer um front em vanilla js ou react/next, enfim...  
ah, e acabei de notar que os testes de unidade de servi�o n�o foram finalizados. sorry : (  
espero que gostem.  

## outros

Colocar um valor sugerido por usu�rio de contribui��o (valor com e sem bebida inclusa); - ?  
- h� um valor de comida e um valor de bebida na hora de adicionar uma pessoa no churrasco  
- a pessoa a ser adicionada deve escolher se vai pagar somente comida, somente bebida ou ambos (e quanto)  
- o valor sugerido pode ser algo hard-coded (fixo) (ou pode ser uma m�dia...)  
pela simplicidade:  
nao precisamos do toggle, e podemos ter as duas formas de 'prediction' do valor caso n�o informado 

## code review

prediction de valores? desfeito
haspaid pra person? nao sei se precisa. adicionar? se sobrar tempo
teste pro dtos => fazer
pegar todos os churrascos? tela de login? se n�o achar nada pra fazer hoje(qui) e amanh�, fa�o.
esperam o front? se houver tempo suficiente, fa�o no final de semana. e em vanilla js.
