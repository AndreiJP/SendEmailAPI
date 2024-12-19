## Introduzione

La classe `EmailService` implementa l'interfaccia `IEmailService` e fornisce una funzionalità per inviare email. Utilizza il client SMTP per gestire la connessione e l'invio. Questo servizio è configurato tramite il file di configurazione dell'applicazione e supporta l'invio di email in modo asincrono.

## Prerequisiti

Assicurati di avere i seguenti prerequisiti prima di utilizzare questo progetto:

- **.NET 6.0 o superiore**
- **Un server SMTP**: Devi avere accesso a un server SMTP configurato e le credenziali per l'autenticazione.
- **Configurazione**: Un file di configurazione (ad es. `appsettings.json`) per specificare le impostazioni del server SMTP.

## Configurazione

Per utilizzare `EmailService`, è necessario configurare le seguenti impostazioni nel file di configurazione della tua applicazione (`appsettings.json` o un file equivalente):

```json
{
  "EmailAccount": {
    "Server": "smtp.tuo-server.com",
    "Port": "587",
    "Email": "tuo-username",
    "Password": "tua-password"
  }
}
