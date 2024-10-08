# Temporal User Flow

## 1. Авторизация
- Пользователь (владелец животного или администратор гостиницы) открывает сайт/приложение.
- Вводит свои данные (логин и пароль).
- Запрос отправляется через **Frontend** в микросервис **Authentication**, который проверяет данные через **Postgres**.
- Если авторизация успешна, пользователь получает JWT-токен для дальнейших действий.

## 2. Просмотр номерного фонда гостиниц
- Авторизованный пользователь (владелец животного) переходит к разделу с гостиницами.
- Запрос на получение списка гостиниц отправляется через **Frontend** к **Gateway API**, который взаимодействует с микросервисом **Backend** для получения данных из базы **Postgres**.
- Пользователь видит доступные номера, подробности о номерах, включая фотографии (фотографии хранятся в **Files**).

## 3. Бронирование номера для животного
- Пользователь выбирает номер и начинает процесс бронирования.
- Запрос на создание бронирования отправляется через **Frontend** в **Backend**, который обрабатывает и сохраняет данные в **Postgres**.
- После успешного бронирования пользователь получает подтверждение с деталями.

## 4. Чат с работниками гостиницы
- Владелец животного может задать вопросы через чат.
- Сообщения отправляются через **Frontend** к микросервису **Messages**, который использует **Redis** для хранения и синхронизации сообщений.
- Работник гостиницы отвечает на сообщения в режиме реального времени.

## 5. Отзывы, комментарии и рейтинги
- После завершения услуги пользователь может оставить отзыв о гостинице.
- Отзыв отправляется через **Frontend** в микросервис **Reviews**, который сохраняет отзывы и рейтинги в **MongoDB**.
- Пользователь также может оставлять комментарии на страницах гостиницы.

## 6. Загрузка фотографий животных
- Владелец или работник гостиницы загружает фотографии животных для бронирования или профиля.
- Фотографии отправляются через **Frontend** в микросервис **Files** и сохраняются в **Minio**.
- Фотографии отображаются на страницах гостиницы и в профиле животного.

## 7. Уведомления
- Владелец получает уведомления о новых бронированиях, напоминаниях и предложениях оставить отзыв через микросервис **Notifications**.
- Уведомления синхронизируются через **RabbitMQ** и могут быть отправлены по различным каналам (email, push-уведомления).

## 8. Завершение работы
- Пользователь завершает сессию, закрыв профиль, что завершает процесс авторизации и защиты данных.