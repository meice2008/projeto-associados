# projeto-associados
Cadastra empresas e associados. Associa empresas a associados. 

1.Criar cadastro de associados com os seguintes campos:
1.1. Id (int, auto incrementável)
1.2 Nome (varchar, 200, não nulo)
1.2 Cpf (varchar, 11, não nulo)
1.3 Data de Nascimento(DateTime)

2. Criar cadastro de empresas com os seguintes campos:
2.1. Id(int, auto incrementável)
2.2. Nome(varchar,200, não nulo)
2.3. CNPJ (varchar,14, não nulo)

3. Criar o Relacionamento N pra N entre associado e empresa.

4. Cada cadastro citado acima(itens 1 e 2) deve possuir as apis de inclusão, alteração, consulta por filtros retornando uma lista como resultado, consulta por id retornando o objeto como resultado, e exclusão, tendo como parâmetro o id do mesmo.

5. Os campos CPF e CNPJ devem ser únicos.

6. Na inclusão do associado deve ser possível adicionar 1 ou mais empresas.

7. Na inclusão da empresa deve ser possível adicionar 1 ou mais associados.

8. Na alteração de associado deve ser possível, além de alterar os dados cadastrais do mesmo, adicionar ou remover uma empresa vinculado a ele.

9. Na alteração da empresa deve ser possível, além de alterar os dados cadastrais da mesma, adicionar ou remover um associado vinculado a ela.

10. Na exclusão de associado, remover as empresas do relacionamento sem exclui-las.

11. Na exclusão da empresa, remover os associados do relacionamento sem excluí-los.

12. Os filtros das consultas serão os campos de cada tabela, não sendo necessário colocar o filtro por relacionamento.
