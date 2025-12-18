# 🏗️ Dotnet CQRS - Architecture Reference MVP

> **O Gabarito Arquitetural.**
> Uma implementação canônica de referência para arquiteturas de backend modernas baseadas em DDD, CQRS e Vertical Slices.

---

## 📖 Sobre o Projeto

Este repositório atua como um **MVP (Minimum Viable Product)** desenhado para servir de referência técnica ("Gabarito") para o desenvolvimento de aplicações escaláveis em .NET.

O projeto foi desenvolvido deliberadamente como um **cenário isolado e controlado**, livre de débitos técnicos ou complexidades de negócio específicas, para demonstrar padrões arquitetônicos em sua forma mais pura. O objetivo é reduzir a carga cognitiva na tomada de decisão técnica, guiando o desenvolvedor para a implementação correta através do próprio design do código.

## 🏗️ Pilares Arquiteturais

O projeto valida estruturalmente os seguintes conceitos:

* **Domain-Driven Design (DDD):** Foco no "Coração do Software". O Domínio é rico, autossuficiente e agnóstico à persistência.
* **CQRS (Command Query Responsibility Segregation):** Separação estrita entre operações de escrita (Commands/Side Effects) e leitura (Queries/Read-Only).
* **Vertical Slice Architecture:** Organização por funcionalidade (*features*) em vez de camadas técnicas horizontais, maximizando a coesão e facilitando a manutenção.

## 🎯 Decisões de Design e Destaques

Para garantir profundidade arquitetural, foram priorizados fluxos críticos de domínio que demonstram as capacidades da arquitetura:

### 1. Domínio Rico e Value Objects
A arquitetura demonstra a implementação de **Value Objects** (como `PasswordHash` e `Email`) para encapsular regras de validação intrínsecas. O design induz a "fazer a coisa certa": métodos semânticos blindam as regras de negócio, exigindo que o desenvolvedor respeite a integridade centralizada no Domínio em vez de dispersar validações nos serviços.

### 2. Padronização de Performance em Leitura
Todos os fluxos de leitura (`Queries`) implementam sistematicamente o `.AsNoTracking()`. O foco é evitar o overhead do ORM (*Change Tracker*) em operações que não exigem persistência, garantindo consultas de alta performance por padrão.

### 3. Foco Estrutural
Integrações periféricas e dependências externas foram omitidas para manter o foco estritamente na validação da estrutura central, fluxo de dados (Pipeline) e organização das camadas.

## 🚀 Como Executar

1.  Clone o repositório:
    ```bash
    git clone https://github.com/ThiagoMSM/dotnet-cqrs.git
    ```
2. Crie o arquivo `appsettings.json` utilizando o `appsettingsExample.json` como base e configure sua string de conexão apontando para uma instância **MySQL**
3. Execute a API (As migrações de banco de dados serão aplicadas automaticamente na inicialização).

## 📂 Estrutura do Repositório

* **`src/backend/Domain`**: O núcleo da aplicação. Entidades, Value Objects e Regras de Negócio Puras.
* **`src/backend/Application`**: Casos de uso organizados em Vertical Slices, CQRS, Regras de Aplicação e Contratos.
* **`src/backend/Infrastructure`**: Implementação de persistência, EF Core e Serviços.
* **`src/backend/ApiExemplo`**: Entry point e Controllers "magros".

---
*Este projeto serve como material de estudo e referência para desenvolvedores .NET interessados em arquitetura de software.*
