# Backend do projeto-base

Esta pasta contém uma Web API de atividades formativas. Ela foi preparada para fornecer dados ao front-end durante a prova prática.

A API possui os modelos `AtividadeFormativa`, `Curso`, `Workshop` e `IInscrevivel`, além de Repository, Singleton, CORS e Swagger.

## Execução

Dentro desta pasta, execute:

```bash
dotnet restore --configfile NuGet.config
dotnet run
```

Os pacotes necessários do Swagger estão na pasta `packages`, portanto não é necessário acessar o NuGet durante a prova.

O Swagger estará disponível em:

```text
http://localhost:5224/swagger
```

## Endpoints disponíveis

```text
GET    /api/Atividades
GET    /api/Atividades/{id}
POST   /api/Atividades/curso
POST   /api/Atividades/workshop
PUT    /api/Atividades/curso/{id}
PUT    /api/Atividades/workshop/{id}
DELETE /api/Atividades/{id}
GET    /api/Atividades/{id}/descricao
POST   /api/Atividades/{id}/inscrever
```

## Exemplos de dados

### Curso

```json
{
  "titulo": "JavaScript para Interfaces",
  "ano": 2026,
  "professor": "Nome do professor",
  "cargaHoraria": 40,
  "vagas": 30
}
```

### Workshop

```json
{
  "titulo": "Boas práticas de Git",
  "ano": 2026,
  "instrutor": "Nome do instrutor",
  "local": "Laboratório 01",
  "duracaoHoras": 4
}
```
