FROM node:10.15.0-alpine

WORKDIR /app

ADD EventClient /app

RUN npm install --production

EXPOSE 8088

CMD ["node", "index.js"]
