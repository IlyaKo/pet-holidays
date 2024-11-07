FROM node:18 AS build

WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . .  

RUN npm run build

FROM nginx:latest

COPY --from=build /app/build /usr/share/nginx/html

# Копируем файл конфигурации Nginx 
 COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

# Запуск Nginx
CMD ["nginx", "-g", "daemon off;"]