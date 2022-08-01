# notes

olá! por aqui vou deixar o que eu fui anotando conforme o desenvolvimento  
isso envolve pensamentos, decisões técnicas, tradeoffs e tudo mais.
fiquem a vontade pra perguntar.

## decisoes técnicas
- código e commits em inglês, docs e eventuais comentários (serão removidos até o final) em pt-br    
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

- fiz primeiro deploy, amanhã faço os testes. terça validações e quarta troco o banco. será?

- próximos passos, em ordem: validações, refactoring de tudo (visando legibilidade e reaproveitamento), mudar a infra do banco (da app e de testes).
depois, se der tempo, front e melhorias de infra (pensando aqui se vale a pena colocar isso no azure pra fazer algo mais 'requintado').
btw apesar de gostar mt de TDD, nao estou utilizando. primeiro mudo a impl., depois o teste. =(

decidi por nao utilizar interfaces pros validators e foi bom pq poupou tempo nos testes sem ter q fazer mock. bom tradeoff 
(e ainda deu pra brincar com abstract class)
os testes de unidade ajudaram bastante a achar pequenos bugs.

- error handler no personservice, testes unitários dos servicos (fazer de person e revisar de barbeque) => postergar,  teste unitário do dto (=>) done, 
revisar namings, strings e geralzao (pq o indent=2 n funciona mais?). depois voltar pra => legibilidade e reaproveitamento), mudar a infra do banco

- naming review
barbeque => barbecue?
(deixar o banco shared entre os testes está os tornando intermitentes) => resolvido
queria mt colocar sqlserver/postgres mas daí precisaria de um aws/azure free tier. entre sqlite e inmemory, tanto faz, inmemory pela praticidade.
acho que vou dar o teste como done hoje (sexta), sábado faço o front e domingo reviso os testes do back.
fazer readme e notes.md no domingo também (feito)
redirect de /  pra /swagger (feito)
spaces de 4 pra 2, fazer pelo vscode

- acabou que nao tive sabado ou domingo pra mexer no teste.  
o tempo que eu tive foi: de 20/07 a 31/07, 11 dias. desses 11 dias, eu só programei em 4 de fato (e no período da noite), devido a trabalho corrido e compromissos pessoais.
queria ter entregado mais mas hoje (01/08), vou fazer uns ajustes finais e enviar.
vou ficar devendo coisas como: migrations, alguns endpoints extras, infra em sqlserver, infra em provider famoso (aws/azure) com pipeline, fazer um front em vanilla js ou react/next, enfim...  
ah, e acabei de notar que os testes de unidade de serviço não foram finalizados. sorry : (  
espero que gostem.  

## outros

Colocar um valor sugerido por usuário de contribuição (valor com e sem bebida inclusa); - ?  
- há um valor de comida e um valor de bebida na hora de adicionar uma pessoa no churrasco  
- a pessoa a ser adicionada deve escolher se vai pagar somente comida, somente bebida ou ambos (e quanto)  
- o valor sugerido pode ser algo hard-coded (fixo) (ou pode ser uma média...)  
pela simplicidade:  
nao precisamos do toggle, e podemos ter as duas formas de 'prediction' do valor caso não informado 

## code review

prediction de valores? desfeito
haspaid pra person? nao sei se precisa. adicionar? se sobrar tempo
teste pro dtos => fazer
pegar todos os churrascos? tela de login? se não achar nada pra fazer hoje(qui) e amanhã, faço.
esperam o front? se houver tempo suficiente, faço no final de semana. e em vanilla js.
