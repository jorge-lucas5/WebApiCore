## 💻 Sobre o projeto

<h2 align="center">
  &nbsp; Projeto final do curso "REST com ASP.NET Core WebAPI" da plataforma <a href="https://desenvolvedor.io/inicio">desenvolvedor.io</a> &nbsp;
</h2>


## Índice

- [Sobre](#-sobre)
- [Tecnologia utilizada](#-tecnologia-utilizada)
- [Funcionalidades](#-funcionalidades)
- [Recursos utilizados](#-recursos-utilizados)
- [Para clonar o projeto](#-para-clonar-o-projeto)

---


## 🔖 Sobre

Web Api desenvolvida como projeto de estudo de ASP.NET Core, com a finalidade de gerenciar fornecedores, produtos econtrolar o nível de permissão (através das claims) que o usuário pode ter dentro da aplicação
---

## 🚀 Tecnologia utilizada

- [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet)

---

## 📌 Recursos utilizados

- Injeção de Dependência
- Entity Framework Core
- Segurança com o ASPNET Identity
- Migrations
- Filtro customizado para altorização baseada em Claims
- Proteção de dados com User Secrets
- Acesso ao banco de dados via Repositórios
- Validação de utilizando fluent validation
- Mapeamento de entidades em ViewModels com Automapper
- Roteamento inteligente
- Tratamento de erros
- Versionamento de API
- Json Web Token (JWT)
- Documentação com o Swagger
- Monitoramento da API com elmah.io

---

## 🎯 Funcionalidades

Funcionalidades disponíveis na API

- **Cadastro e login de Usuários**

- **Visualizar, Cadastrar, Editar e ExcluirFornecedores:**
De acordo com as permissões do usuário

- **Vizualizar, Cadastrar, Editar e Excluir Produtos:**
De acordo com as permissões do usuário

---

## ⚙ Para clonar o projeto

```bash
  # clonar o projeto
  $ https://github.com/jorgelucasac/WebApiCore.git

  # acessar a pasta do projeto
  $ cd WebApiCore

  # para restaurar todas as dependências
  $ dotnet restore

```

---
