Autenticação/Autorização: Não necessita desenvolver, mas explique como você faria a autenticação
e autorização para proteger os endpoints e garantir que apenas usuários autorizados possam acessá-los.
R: Para autenticação eu utilizaria jwt, com validação de tokens.

Documentação: Não precisa fazer, mas explique brevemente o que usaria para criar a documentação
clara para a API, incluindo descrições de endpoints, parâmetros, respostas e exemplos de uso.
R: A documentação é a que já estou utilizando, inclusive, SWAGGER, por ser nativa e autoimplementada.

Segurança: Não necessita fazer, mas explique o que você faria para que sejam cumpridos os 2
requisitos abaixo:

Como você implementaria medidas de segurança, como proteção contra-ataques de injeção de
SQL ou XSS;
R: Sql Injection estou utilizando Entity Framework, que elimina completamente esta ameaça; já XSS
eu poderia utilizar o a biblioteca AntiXSS.

Como você implementaria um tratamento de erro apropriado para lidar com falhas na
comunicação com o banco de dados.
R: Capturando o erro (SqlException somente) com try-catch, dando o devido direcionamento às excessões.

OBS: Os testes unitários foram extremamentes pontuais, mostrando de uma forma 'demonstrativa' de uso. A 
cobertura poderia (deveria) ser maior, mas o intuito do teste é mostrar o máximo das habilidades e 
conhecimentos possíveis. Aproveitei o tempo para demonstrar algumas particularidades não requisitadas 
porém, se fizeram essenciais ao projeto.
