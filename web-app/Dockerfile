
FROM node:alpine 
WORKDIR /app
COPY package.json ./
COPY package-lock.json ./
RUN npm i
COPY . ./
RUN npm run build
RUN npm install -g serve
CMD ["serve", "-s", "build"]