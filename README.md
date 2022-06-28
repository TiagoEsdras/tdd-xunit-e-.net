# TDD com xUnit
## _TDD na prática_

Esse projeto consiste na construção de uma API utilizando TDD, os princípios SOLID e boas práticas de programação, conhecimentos adquiridos durante o estudo do curso [TDD com xUnit para C# .Net Core | UDEMY](https://www.udemy.com/course/automatizando-testes-para-sua-aplicacao).

## Resumo

TDD vem do acrônimo em inglês Test Driven Development (em português: Desenvolvimento Orientado a Testes). Esse conceito foi apresentado por Kent Beck introduzindo a técnica de codificar sistemas a partir da escrita de testes unitários, e hoje, é amplamente utilizado nos times de desenvolvimento. 
A ideia consiste no ciclo que funciona da seguinte forma:

1°- É escrito o teste de uma funcionalidade a qual será implementada seguindo as regras de negócio. E ao executar o teste, o resultado deve ser uma falha, pois a implementação do código da funcionalidade ainda não foi realizada.

2°- Após a falha do teste, a implementação da funcionalidade é realizada, e ao rodar o teste novamente, resultado deve ser sucesso.

3°- Com o sucesso do teste, é feita a refatoração do código a fim de aplicar as boas práticas de programação.

Esse ciclo deve ser repetido em todo o desenvolvimento de um sistema para ter como resultado uma aplicação que funcione corretamente, garantido pela boa cobertura de teste unitários. Além disso, o TDD traz como benefícios a geração de códigos menos complexos que vão seguir exatamente a regra do negócio,  a fácil manutenibilidade do software com um código limpo e a oportunidade de corrigir falhas antes mesmo de chegar no usuário final.

## O Projeto

Na API criada temos 3 entidades de domínio: Aluno, Curso e Matrícula.

Para cada uma delas, existem os endpoints de Cadastro, Obtenção da lista, Obtenção por id, Atualização dos dados e a Exclusão (CRUD).

### Aluno

```
{
  "id": guid,
  "nome": string,
  "cpf": string,
  "email": string,
  "publicoAlvo": string
}
```

#### Regra de negócio

* Nome não pode ser uma string vazia ou nulo;
* CPF não pode ser uma string vazia ou nulo;
* CPF deve ter valor válido de acordo com as regras da Receita Federal;
* Email não pode ser uma string vazia ou nulo;
* Email deve ter valor válido de acordo com a classe `MailAddress` do System.Net.Mail;
* PublicoAlvo deve ter um valor válido de acordo com o enum `PublicoAlvoEnum` - "Estudante", "Universitario", "Empregado" ou "Empreendedor";
* Não deve ser possível cadastrar um aluno com CPF já existente no banco de dados;
* Deve ser possível cadastrar um Aluno;
* Deve ser possível buscar lista de Alunos;
* Deve ser possível buscar Aluno por Id;
* Deve ser possível alterar somente o Nome do Aluno informando o Id do Aluno a ser alterado;
* Deve ser possivel deletar um Aluno informando o Id do Aluno a ser deletado;
* Deve passar um Id válido nos endpoints de Alunos: Get, Put e Delete;

### Curso

```
{
  "id": guid,
  "nome": string,
  "descricao": string,
  "cargaHoraria": decimal,
  "publicoAlvo": string,
  "valor": decimal
}
```

#### Regra de negócio

* Nome não pode ser uma string vazia ou nulo;
* Descricao não pode ser uma string vazia ou nulo;
* CargaHoraria não pode ser menor ou igual a zero;
* Valor não pode ser menor ou igual a zero;
* PublicoAlvo deve ter um valor válido de acordo com o enum `PublicoAlvoEnum` - "Estudante", "Universitario", "Empregado" ou "Empreendedor";
* Não deve ser possível cadastrar um Curso com Nome já existente no banco de dados;
* Deve ser possível cadastrar um Curso;
* Deve ser possível buscar lista de Cursos;
* Deve ser possível buscar Curso por Id;
* Deve ser possível alterar somente Nome, Descricao, CargaHoraria e Valor do Curso informando o Id do Curso a ser alterado;
* Deve ser possivel deletar um Curso informando o Id do Curso a ser deletado;
* Deve passar um Id válido nos endpoints de Cursos: Get, Put e Delete;

### Matrícula

```
{
  "id": guid,
  "alunoId": guid,
  "cursoId": guid,
  "valorPago": decimal
  "existeDesconto": bool
}
```

#### Regra de negócio

* AlunoId deve ser um id válido;
* CursoId deve ser um id válido;
* ValorPago não pode ser menor ou igual que zero;
* ValorPago não pode ser maior que o valor do Curso;
* ExisteDesconto não é informado na request na criação da matrícula.
* ExisteDesconto deve ser o resultado da seguinte regra: ValorPago < ValorDoCurso ? verdadeiro : falso;
* Deve ser validado que o PublicoAlvo do Curso e o PublicoAlvo do Aluno são iguais;
* Não deve ser possível matricular um Aluno em um Curso que ele já esteja matriculado;
* Deve ser possível criar uma Matricula;
* Deve ser possível buscar lista de Matriculas;
* Deve ser possível buscar Matricula por Id;
* Deve ser possível alterar somente ValorPago da Matricula informando o Id do Matricula a ser alterada;
* Deve ser possivel deletar uma Matricula informando o Id do Matricula a ser deletada;
* Deve passar um Id válido nos endpoints de Matriculas: Get, Put e Delete;

## Tecnologias Utilizadas

Aqui estão as tecnologias utlilizadas no projeto:

* C#
* .NET 5.0
* AspNet WebAPI
* Entity Framework ("in memory persistence")
* xUnit
* Moq
* ExpectedObjects
* Bogus

## REQUISITOS NECESSÁRIOS

Para rodar esse projeto você precisará de:

* Visual Studio 
* SDK .NET - Versão 5.0.4
* Windows 

## COMO USAR
Rode os seguintes comandos:

 ```
 git@github.com:TiagoEsdras/tdd-xunit-e-.net.git
```
```
 cd tdd-xunit-e-.net/CursoOnline/
```
 ```
 dotnet build
```
```
cd src/CursoOnline.API
```
```
dotnet watch run
```

## Authors
 
[@TiagoEsdras](https://github.com/TiagoEsdras)
 
