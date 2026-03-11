## Drkb.Notification

Сервис уведомлений для экосистемы Drkb, реализованный как ASP.NET Core Web API с поддержкой SignalR, JWT‑аутентификации и хранения данных в базе через Entity Framework Core.

### Основные возможности

- **Получение непрочитанных уведомлений адресованных пользователю** для текущего авторизованного пользователя.
- **Подсчёт количества непрочитанных уведомлений адресованных пользователю**.
- **Пометка конкретного сообщения как прочитанного**.
- **Массовая пометка всех сообщений как прочитанных**.
- **Push‑уведомления через SignalR‑хаб** для доставки уведомлений в режиме реального времени.
- **Интеграция с брокером сообщений (RabbitMQ)** для получения событий о новых уведомлениях.

### Архитектура и проекты

Решение разделено на несколько проектов по слоям:

- **`Drkb.Notification`** – ASP.NET Core Web API:
  - `Program.cs` — точка входа, регистрация DI, логирования, CORS, SignalR, Swagger.
  - `Controllers/NotificationController.cs` — REST‑контроллер для работы с уведомлениями.
  - `Hubs/NotificationHub.cs`, `Hubs/SignalRNotificationDispatcher.cs` — SignalR‑хабы и диспетчер уведомлений.

- **`Drkb.Notification.Application`** – слой application/use case:
  - Команды: `CreateMessage`, `MarkMessageByIdAsRead`, `MarkAllMessagesAsRead`.
  - Запросы: `GetUnreadMessages`, `GetUnreadCount`.
  - Использует **MediatR** для обработки команд и запросов.

- **`Drkb.Notification.Domain`** – доменная модель:
  - Сущности и доменная логика уведомлений.

- **`Drkb.Notification.Infrastructure`** – инфраструктурный слой:
  - Подключение к БД через **EF Core** и `Npgsql` (PostgreSQL).
  - Реализация data provider’ов и query‑объектов.
  - Подключение брокера сообщений (**RabbitMQ**).
  - Конфигурация логирования через **Serilog** (включая sink в Seq).
  - Интеграция Swagger.

- **`Drkb.Notification.Contract`** – контракты передачи данных (DTO/контракты).

- **`Drkb.Notification.Integration`** – интеграционный слой (обработчики событий, интеграция с другими сервисами).

### Используемые технологии

- **.NET**: .NET 8 (`net8.0`).
- **Web**: ASP.NET Core Web API, SignalR.
- **CQRS / Mediator**: MediatR.
- **Доступ к данным**: Entity Framework Core, Npgsql (PostgreSQL).
- **Аутентификация и JWT**: `Drkb.JwtConfiguration`.
- **Сообщения**: `MessageBroker.RabbitMQ`.
- **Документация API**: Swashbuckle (Swagger / SwaggerUI).
- **Логирование**: Serilog, Serilog.AspNetCore, Serilog.Sinks.Seq.

### HTTP API

Все методы контроллера `NotificationController` доступны по префиксу `api/notifications` и требуют аутентификации (`[Authorize]`).

- **GET `api/notifications`**  
  Возвращает список непрочитанных уведомлений текущего пользователя.
  - **Ответ 200** – `List<GetUnreadMessagesDto>`.
  - **Ошибки** – код и сообщение берутся из `result.StatusCode` и `result.ErrorMessage`.

- **GET `api/notifications/unread-count`**  
  Возвращает объект с количеством непрочитанных уведомлений.
  - **Ответ 200** – `GetUnreadCountDto` (например, поле `Count`).

- **POST `api/notifications/{id:guid}/read`**  
  Помечает конкретное уведомление с указанным `id` как прочитанное.
  - **Ответ 200** – в случае успешного выполнения.

- **POST `api/notifications/read-all`**  
  Помечает все уведомления текущего пользователя как прочитанные.
  - **Ответ 200** – в случае успешного выполнения.

### SignalR‑хаб

- **Маршрут хаба**: `/notification`  
  Регистрируется в `Program.cs` с помощью `app.MapHub<NotificationHub>("/notification");`.

Клиенты могут подписываться на хаб для получения уведомлений в реальном времени (например, при создании нового сообщения через брокер сообщений будет отправлен сигнал подключенным клиентам).

### Конфигурация

Основные настройки находятся в файлах:

- `appsettings.json`
- `appsettings.Development.json`
- `appsettings.Production.json`

Обычно необходимо задать:

- **Подключение к БД** (PostgreSQL) – секция `ConnectionStrings`.
- **Настройки JWT** – параметры для `Drkb.JwtConfiguration`.
- **Параметры RabbitMQ** – хост, логин/пароль, виртуальный хост и т.п.
- **Логирование Serilog/Seq** – URL сервера Seq, минимальный уровень логов.

### Запуск проекта локально

1. **Установить зависимости**:
   - .NET 8 SDK.
   - Локальный PostgreSQL (или доступ к внешнему экземпляру).
   - Локальный RabbitMQ (если требуется интеграция событий).
   - При необходимости – Seq для логирования.

2. **Настроить `appsettings.*.json`**:
   - Указать строку подключения к PostgreSQL.
   - Настроить JWT (issuer, audience, ключ подписи).
   - Настроить параметры RabbitMQ.
   - Настроить параметры Serilog/Seq (по желанию).

3. **Применить миграции БД** (если в проекте есть миграции EF Core). Пример:

```bash
dotnet ef database update -p Drkb.Notification.Infrastructure -s Drkb.Notification
```

4. **Собрать и запустить API**:

```bash
dotnet build
dotnet run --project Drkb.Notification
```

После запуска:

### Типичный сценарий использования

1. Клиент (например, фронтенд) аутентифицируется и получает JWT‑токен.
2. Открывает подключение к SignalR‑хабу `/notification` и подписывается на события уведомлений.
3. При появлении новых уведомлений через брокер сообщений сервис создает записи в БД и отправляет push‑уведомления через SignalR.
4. Клиент периодически запрашивает:
   - список непрочитанных уведомлений – `GET api/notifications`;
   - количество непрочитанных – `GET api/notifications/unread-count`.
5. Пользователь помечает уведомления как прочитанные:
   - одно уведомление – `POST api/notifications/{id}/read`;
   - сразу все – `POST api/notifications/read-all`.

### Стиль и соглашения

- Используется чистое разделение по слоям: Web/API, Application (use case), Domain, Infrastructure, Contracts, Integration.
- В качестве шаблона для команд/запросов принят подход с MediatR: контроллеры тонкие, логика в хэндлерах.
- Для расширения функциональности рекомендуется:
  - добавлять новые сценарии в слое `Application`;
  - расширять/изменять доменную модель в `Domain`;
  - добавлять новые query‑объекты и data provider’ы в `Infrastructure`.

### Дальнейшее развитие

Возможные направления:

- Добавление разных типов уведомлений (email, push в браузер/мобильное приложение, системные сообщения и т.п.).
- Гибкая настройка каналов доставки и стратегий ретраев.
- Расширенные фильтры и пагинация по истории уведомлений.
- Дэшборд администрирования и мониторинга.

