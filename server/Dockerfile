FROM ubuntu:16.04

RUN apt-get update -y
RUN apt-get install -y nodejs npm
RUN ln -s /usr/bin/nodejs /usr/bin/node

RUN mkdir -p /blocker
WORKDIR /blocker

COPY . /blocker/
RUN npm install

EXPOSE 8080

ENTRYPOINT [ "npm", "start" ]

