#!/bin/bash

ACTIONS=${*:-"build run"}

echo $ACTIONS

if [[ $ACTIONS =~ build ]];
then
    docker rmi -f blocker
    docker build -t blocker .
fi

if [[ $ACTIONS =~ run ]];
then
    docker rm -f blocker blocker-mongo
    docker run --name blocker-mongo -d mongo
    docker run --link blocker-mongo:mongo -d -v $(pwd):/blocker -p 0.0.0.0:8080:8080 -e DEBUG=express:* --name blocker blocker --name blocker-mongo
fi
