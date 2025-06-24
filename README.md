# Projeto - Processamento de Aluguel de Veículos 🚗 (Em Desenvolvimento 🚧)

Este projeto é uma solução em nuvem utilizando serviços do Microsoft Azure, com foco no **processamento e gestão de solicitações de aluguel de veículos**.

---

## Tecnologias Utilizadas

- **Azure Functions (.NET 8 - Worker Isolado)**  
Processamento de mensagens e integração com serviços externos.

- **Azure Container Apps**  
Hospedagem de microsserviços backend, expostos via API, utilizando imagens Docker.

- **Azure Container Registry (ACR)**  
Gerenciamento e armazenamento das imagens Docker utilizadas no projeto.

- **SQL Server (Azure SQL Database)**  
Persistência de dados relacionais, como cadastro de clientes e registros de aluguel.

- **Azure Service Bus**  
Gerenciamento de mensagens assíncronas entre os componentes da solução.

- **Cosmos DB**  
Armazenamento NoSQL para logs de eventos e histórico de processamento.

---

## Status do Projeto

🚧 **Projeto em desenvolvimento**

As integrações finais, testes e ajustes de performance ainda estão em andamento.

---

## Próximos Passos

- Finalizar integração entre Azure Function e SQL Server  
- Configurar pipelines de CI/CD para o Container App e Functions  
- Implementar monitoramento com Application Insights  

---

> _Este repositório será atualizado conforme as próximas entregas de desenvolvimento._
