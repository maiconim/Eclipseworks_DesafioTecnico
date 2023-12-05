# DesafioTecnico
Esta solução faz parte de um desafio técnico proposto pela Eclipseworks como pré-requisito de avaliação do currículo.

## Tecnologias utilizadas
* Entity Framework
* Migrator
* Mediator

## Arquiteturas empregadas
* Repositórios
* Serviços
* Domain Driven Design
* Arquitetura hexagonal

## Framework
Este projeto foi desenvolvido usando Net Core 7.

## Docker
A solução está apta a ser executada via Docker ou Http.

Para iniciar em Docker, use a sequencia de comandos a seguir no Prompt de Comando em modo administrativo na pasta rais da solução:

'docker build -t maicon-milke -f .\DesafioTecnico.WebAPI\Dockerfile .'
'docker create --name maicon-milke maicon-milke'
'docker run -d -p 5000:80/tcp --name maicon-milke maicon-milke'

Acesse no seu browser o endereço http://localhost:5000/swagger/index.html.

## Refinamento

### Autenticação
Necessário identificar o tipo de autenticação para que o código identifique o usuário autorizado a operá-lo.

## Melhorias
Documentação  melhorada das interfaces e classes. 