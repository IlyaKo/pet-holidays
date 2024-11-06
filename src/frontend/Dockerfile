FROM node:18 AS build

WORKDIR /app

COPY frontend/package*.json ./

RUN npm install

COPY frontend/. .  

RUN npm run build

FROM nginx:latest

COPY --from=build /app/build /usr/share/nginx/html

EXPOSE 80

# Запуск Nginx
CMD ["nginx", "-g", "daemon off;"]