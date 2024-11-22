# AvaliacaoVibe
 
Este projeto tem como objetivo gerenciar e exportar dados de Placemarks para um arquivo KML, com foco na performance, escalabilidade e boa arquitetura de software. 
O projeto se deu como um desafio proposto pela Vite Tecnologia.

# Princípios SOLID no Projeto
S - Single Responsibility Principle (Princípio da Responsabilidade Única)
Cada classe tem uma única responsabilidade, facilitando a manutenção e entendimento do código. Por exemplo, temos uma classe Utils separada para a geração do arquivo KML, outra para o acesso aos dados e uma para o controller que recebe as requisições HTTP.

O - Open/Closed Principle (Princípio Aberto/Fechado)
O sistema foi desenhado para ser facilmente estendido sem a necessidade de modificar o código existente. Por exemplo, ao adicionar novos Objetos ao KML a necessidade séria de apenas criar os novos Objetos.

L - Liskov Substitution Principle (Princípio da Substituição de Liskov)
As subclasses podem substituir suas classes base sem afetar a funcionalidade do sistema. 

I - Interface Segregation Principle (Princípio da Segregação de Interfaces)
Interfaces foram desenhadas de maneira específica para os diferentes tipos de operações, evitando que classes implementem métodos que não são necessários para a sua funcionalidade. Isso garante que as classes não sejam sobrecarregadas com métodos irrelevantes.

D - Dependency Inversion Principle (Princípio da Inversão de Dependência)
O projeto segue o padrão Injeção de Dependência (DI), garantindo que as dependências sejam passadas para as classes de forma externa. Isso permite maior flexibilidade e facilita os testes, já que podemos substituir implementações sem modificar a lógica principal.

# Foco em Performance
A performance é um dos pilares do projeto. Algumas das principais considerações para garantir eficiência incluem:

Uso de operações assíncronas: A solução utiliza métodos assíncronos para garantir que a aplicação não se torne bloqueante, especialmente ao recuperar dados ou gerar arquivos KML. Isso é crucial quando lidamos com grandes volumes de dados ou operações de I/O (como leitura/escrita de arquivos).

Armazenamento dos dados oriundos do KML me cache: Utilizando o Memory Cache os dados oriundos são armazenados e enquanto a aplicação se manter sem alterações ele irá se manter usando os dados em cache, os dados são carregados na primeira consulta e permeados.

# Endpoints da API
O projeto disponibiliza os seguintes endpoints para interação com os dados de Placemarks:

GET /api/placemark
Recupera todos os Placemarks, com possibilidade de filtragem via parâmetros de query (como Cliente, Situação, Bairro, etc.).

POST /api/placemark/export
Gera e retorna todos os Placemarks em um arquivo KML, com base nos filtros fornecidos.

GET /api/placemark/filters
Retorna os filtros únicos para cada campo, permitindo ao usuário saber os valores possíveis para cada parâmetro.
