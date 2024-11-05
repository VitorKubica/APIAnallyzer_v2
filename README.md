# Bem vindos à Anallyzer_v2 🚀

APIAnallyzer_v2 é uma API RESTful em .NET que gerencia campanhas de marketing e prevê seu sucesso com base em métricas de desempenho. Utilizando MongoDB como banco de dados e ML.NET para machine learning, a API oferece funcionalidades avançadas de predição de campanhas, integrações para validação de dados e práticas sólidas de desenvolvimento, como Clean Code e princípios SOLID.

## Funcionalidades Principais

1. **Gerenciamento de Campanhas e Relatórios**
   - CRUD completo para campanhas de marketing.
   - CRUD completo para relatórios de campanhas com métricas como cliques, aberturas de e-mails, envios e leads gerados nos últimos 7 e 30 dias.

2. **Predição de Sucesso de Campanha**
   - Machine learning integrado para previsão do sucesso de campanhas com base nas métricas fornecidas.
   - Utilização do ML.NET para treinamento e predição de modelos com base em dados históricos.

3. **Validação de Dados**
   - Integração com um serviço externo de validação de e-mail.
   - Validação de formato de CNPJ.

## Requisitos Atendidos

### 1. Integração com Serviço Externo

Foi implementada uma integração com a API externa **Mails.so** para validação de e-mails. Esse serviço verifica a deliverabilidade dos e-mails registrados em campanhas, garantindo que apenas endereços válidos e ativos sejam considerados.

- A integração foi implementada no `ValidationService`, que faz uma chamada HTTP para a API de validação de e-mail.
- O serviço retorna `true` para e-mails válidos e `false` para inválidos, rejeitando registros de e-mails não confirmados.

### 2. Testes Unitários, de Integração e de Sistema

Foram implementados testes com o **xUnit** e **Moq** para garantir a robustez da API. Os testes cobrem:

- **Testes Unitários**: Verificação das funções principais de cada serviço (ex.: validação de CNPJ e e-mail, CRUD de campanhas).
- **Testes de Integração**: Testes sobre a integração com o MongoDB para garantir o armazenamento e recuperação corretos dos dados.
- **Testes de Sistema**: Testes de ponta a ponta que verificam o funcionamento completo dos endpoints, desde a criação de uma campanha até sua atualização e exclusão.

Para rodar os testes, utilize o comando:
```bash
dotnet test
```
Configuração do MongoDB: Atualize a string de conexão do MongoDB em appsettings.json:

```json
"ConnectionStrings": {
  "DbConnection": "mongodb+srv://usuario:senha@cluster.mongodb.net/AnallyzerDB"
}
```
### Endpoints

#### Campaigns (`/api/campaigns`)

Gerencia campanhas de marketing com funcionalidades de criação, leitura, atualização e exclusão.

- **`GET /api/campaigns`**  
  Retorna uma lista de todas as campanhas cadastradas.
  - **Resposta**: Status 200 e uma lista de campanhas em formato JSON.

- **`GET /api/campaigns/{id}`**  
  Retorna os detalhes de uma campanha específica pelo seu ID.
  - **Parâmetros**: `id` - ID da campanha.
  - **Resposta**: Status 200 e os detalhes da campanha ou 404 se não encontrada.

- **`POST /api/campaigns`**  
  Cria uma nova campanha de marketing.
  - **Body**: JSON com os detalhes da campanha (nome, descrição, empresa, CNPJ, e-mail, data de início, data prevista de término e status).
  - **Resposta**: Status 201 e a campanha criada ou 400 se os dados forem inválidos.

- **`PUT /api/campaigns/{id}`**  
  Atualiza os detalhes de uma campanha existente pelo seu ID.
  - **Parâmetros**: `id` - ID da campanha.
  - **Body**: JSON com os campos da campanha a serem atualizados.
  - **Resposta**: Status 200 e os detalhes da campanha atualizada, 404 se não encontrada, ou 400/409 em caso de erro de validação.

- **`DELETE /api/campaigns/{id}`**  
  Exclui uma campanha específica pelo seu ID.
  - **Parâmetros**: `id` - ID da campanha.
  - **Resposta**: Status 204 para exclusão bem-sucedida ou 404 se a campanha não for encontrada.

#### CampaignReports (`/api/campaignreport`)

Gerencia relatórios de campanhas, incluindo métricas de desempenho nos últimos 7 e 30 dias.

- **`GET /api/campaignreport`**  
  Retorna todos os relatórios de campanhas.
  - **Resposta**: Status 200 e uma lista de relatórios de campanha em JSON.

- **`GET /api/campaignreport/{id}`**  
  Retorna um relatório de campanha específico pelo seu ID.
  - **Parâmetros**: `id` - ID do relatório de campanha.
  - **Resposta**: Status 200 e os detalhes do relatório ou 404 se não encontrado.

- **`POST /api/campaignreport`**  
  Cria novos relatórios de campanha em massa.
  - **Body**: JSON com uma lista de relatórios de campanha (contendo métricas como cliques, aberturas de e-mails, envios e leads para 7 e 30 dias).
  - **Resposta**: Status 201 e a lista de relatórios criados ou 400 para dados inválidos.

- **`PUT /api/campaignreport/{id}`**  
  Atualiza um relatório de campanha existente pelo seu ID.
  - **Parâmetros**: `id` - ID do relatório.
  - **Body**: JSON com os campos a serem atualizados.
  - **Resposta**: Status 204 para atualização bem-sucedida, 404 se não encontrado.

- **`DELETE /api/campaignreport/{id}`**  
  Exclui um relatório específico pelo seu ID.
  - **Parâmetros**: `id` - ID do relatório de campanha.
  - **Resposta**: Status 204 para exclusão bem-sucedida ou 404 se o relatório não for encontrado.

#### Predição de Campanhas (`/api/campaignprediction/predict`)

Prediz o sucesso de uma campanha de marketing com base nas métricas fornecidas.

- **`POST /api/campaignprediction/predict`**  
  Recebe as métricas de desempenho de uma campanha e retorna uma previsão de sucesso.
  - **Body**: JSON com métricas do relatório de campanha (cliques, aberturas de e-mails, envios e leads para 7 e 30 dias).
  - **Resposta**: Status 200 e um valor booleano indicando `true` para campanhas previstas como bem-sucedidas e `false` para campanhas com baixo desempenho.

## 🤝 Integrantes
<table>
  <tr>
    <td align="center">
      <a href="https://github.com/nichol6s">
        <img src="https://avatars.githubusercontent.com/u/105325313?v=4" width="115px;" alt="Foto do Nicholas no GitHub"/><br>
        <sub>
          <strong>Nicholas Santos</strong>
        </sub>
      </a>
    </td>
    <td align="center">
      <a href="https://github.com/VitorKubica">
        <img src="https://avatars.githubusercontent.com/u/107961081?v=4" width="115px;" alt="Foto do Vitor no GitHub"/><br>
        <sub>
          <strong>Vitor Kubica</strong>
        </sub>
      </a>
    </td>
    <td align="center">
      <a href="https://github.com/DuduViolante">
        <img src="https://avatars.githubusercontent.com/u/126472870?v=4" width="115px;" alt="Foto do Violante no GitHub"/><br>
        <sub>
          <strong>Eduardo Violante</strong>
        </sub>
      </a>
    </td>
    <td align="center">
      <a href="https://github.com/pedrocpacheco">
        <img src="https://avatars.githubusercontent.com/u/112909829?v=4" width="115px;" alt="Foto do Pedro no Github"/><br>
        <sub>
          <strong>Pedro Pacheco</strong>
        </sub>
      </a>
    </td>
    <td align="center">
        <a href="https://github.com/biasvestka">
        <img src="https://avatars.githubusercontent.com/u/126726456?v=4" width="115px;" alt="Foto da Beatriz GitHub"/><br>
        <sub>
            <strong>Beatriz Svestka</strong>
        </sub>
      </a>
    </td>
  </tr>
</table>
