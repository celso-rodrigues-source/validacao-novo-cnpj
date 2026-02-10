
## Mudanças

Com o objetivo de se obter o mínimo de impacto, foi mantido o número atual de posições, que é 14. As oito primeiras posições que representam a raiz agora aceitarão, além de números, letras. O mesmo ocorre para as quatro próximas posições que identificam a ordem do estabelecimento (se é matriz ou filial). Já as duas últimas posições, que representam os dígitos verificadores, permanecerão apenas como numéricos. Em resumo, somente os dígitos verificadores permanecerão como números. Uma forma de manter o mesmo esquema de validação do formato numérico utilizado até então.

<img src="validacao-novo-cnpj/img/img03.png" alt="Solução do CNPJ alfanumérico descrita na Nota COCAD/SUARA/RFB nº 49 de 14 de maio de 2024" />

Essa proposta permitiu manter a mesma regra para a validação do CNPJ. Basicamente, o cálculo é feito, inicialmente, multiplicando cada posição do CNPJ pelo seu multiplicador pré-estabelecido. Para calcular o primeiro dígito verificador, multiplicamos cada posição/valor pelo seu multiplicador, respectivamente, conforme a tabela abaixo. Feito isso, o valor de cada uma destas multiplicações deverá ser somado. Com a soma, precisaremos calcular o módulo, que nada mais é do que o resto da soma total dividido por 11. Tomemos por exemplo o CNPJ do LinkedIn Brasil, **13.638.767/0001-92**.

<img src="validacao-novo-cnpj/img/img05.png" alt="Calculando o primeiro dígito verificador do CNPJ" />

Observe que, para calcular o primeiro dígito, pegamos as 12 primeiras posições somente. Após obter o resultado do módulo, que no exemplo acima é 2, precisamos verificar se tal valor é menor ou igual a 1 (um), e caso positivo, o valor do dígito será 0 (zero). Do contrário, deveremos subtrair o resto de 11. No exemplo, o resultado foi 9. Já para calcular o segundo dígito verificador, a regra é a mesma, com a diferença de que acrescentamos também o primeiro dígito verificador para efetuar as somas:

<img src="validacao-novo-cnpj/img/img06.png" alt="Calculando o segundo dígito verificador do CNPJ" />

É bom frisar que, apesar de um número de CNPJ ser válido, isso não significa que ele esteja cadastrado na base de dados da Receita Federal. Agora veremos como fica essa questão da validação para o CNPJ alfanumérico, e um exemplo prático feito utilizando a linguagem de programação C# da Microsoft.

<br />

## Validação do novo CNPJ em C#

Conforme informado na *Nota Técnica conjunta COCAD/SUARA/RFB nº 49 de 14 de maio de 2024*, o cálculo para a validação do CNPJ continua sendo pelo módulo 11. Porém, para que fossem incluídas as letras no número de inscrição do CNPJ, foi necessário alterar a maneira como se calcula o dígito verificador. A partir de agora, é necessário substituir os valores numéricos e alfanuméricos pelo valor decimal correspondente na tabela de código ASCII. De posse de tal valor, é necessário subtrair dele o valor 48. Por exemplo, na tabela abaixo temos a letra "E", cujo valor decimal é 69, logo 69 - 48 = 21, então E = 21 será o valor utilizado nas multiplicações.

<img src="validacao-novo-cnpj/img/img04.png" alt="Valores decimais correspondentes para letras e números na tabela ASCII" />

No exemplo abaixo, utilizamos o CNPJ fictício **33.EMA.SAM/E007-81** para verificar se o mesmo é válido. Observe que existe um passo antes de partirmos para a multiplicação. Primeiramente, recuperamos os valores decimais correspondentes na tabela ASCII e subtraímos de cada um deles o valor 48. É com o resultado dessa subtração que efetuamos a multiplicação por cada um dos valores pré-definidos.

<img src="validacao-novo-cnpj/img/img07.png" alt="Calculando o primeiro dígito verificador do CNPJ alfanumérico" />

Note na forma como é feito o cálculo do segundo dígito verificador. No primeiro momento, recuperamos o respectivo valor decimal da letra ou número na tabela ASCII, e deste valor subtraímos o valor 48. Lembrando que é necessário incluir no cálculo o primeiro dígito verificador também. No segundo passo, efetuamos a multiplicação pelos valores pré-estabelecidos, depois efetuamos a soma. Por fim, entra o cálculo do módulo e a mesma regra que já existia anteriormente.

<img src="validacao-novo-cnpj/img/img08.png" alt="Calculando o segundo dígito verificador do CNPJ alfanumérico" />

Partindo para a prática, criaremos um exemplo de uma classe em C# responsável por validar um CNPJ no padrão alfanumérico. Abaixo foi implementada uma classe estática juntamente com um método responsável pela validação. Na linha 16, "limpamos" a máscara do CNPJ caso a mesma tenha sido informada. Depois verificamos se o mesmo possui 14 posições na linha 20. Nas linhas 26 e 27 estamos criando os *arrays* com os valores pré-definidos responsáveis pela multiplicação de cada um dos dígitos do CNPJ. Já na linha 30, definimos as posições do CNPJ utilizadas no cálculo do primeiro dígito verificador, e, na linha 31, as posições utilizadas para calcular o segundo dígito verificador.

<img src="validacao-novo-cnpj/img/img09.png" alt="Criação da classe de validação" />

Seguindo o código-fonte, na linha 37 estamos "varrendo" o nosso *array* que possui os 12 valores pré-definidos para o cálculo do primeiro dígito, e, ao mesmo tempo, recuperando a respectiva posição no número do CNPJ. A linha 39 recupera o valor conforme a tabela ASCII e subtrai o valor 48, multiplicando logo em seguida pelo valor pré-definido. Tudo isso já armazenando na nossa variável *soma*. Na linha 42, recuperamos o resto da divisão da soma total por 11, e na linha 44, definimos qual é o primeiro dígito de fato. Seguindo pela linha 48, temos os mesmos procedimentos, mas agora para o segundo dígito verificador. Por fim, na linha 58, retornamos verdadeiro caso os dígitos calculados coincidam com os do CNPJ informado.

<img src="validacao-novo-cnpj/img/img10.png" alt="Finalização da classe de validação" />

Por fim, testamos a nossa classe de validação com alguns exemplos:

<img src="validacao-novo-cnpj/img/img11.png" alt="Testando a classe de validação" />

<br />

## Instalação

`git clone https://github.com/celso-rodrigues-source/validacao-novo-cnpj`

<br />

## Referências

*Nota Técnica Conjunta COCAD/SUARA/RFB n° 49/2024:* <https://www.seprorgs.org.br/admin/assets/upload/noticias_anexo/6039179747.pdf>

*Algoritmo para Validação do CNPJ:* <https://www.macoratti.net/alg_cnpj.htm>

*Criando o Projeto C# e as classes para validação:* <https://www.macoratti.net/11/09/c_val1.htm>

*Como validar o novo formato do CNPJ do Brasil:* <https://welyab.dev/2024/06/29/como-validar-o-novo-formato-do-cnpj-do-brasil>


