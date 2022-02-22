# banking.operation-notification-consumer

Banking Operation Solution - Notification Consumer

[![.NET](https://github.com/EdsonCaliman/banking.operation-notification-consumer/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/EdsonCaliman/banking.operation-client-api/actions/workflows/dotnet.yml)
[![Coverage Status](https://coveralls.io/repos/github/EdsonCaliman/banking.operation-notification-consumer/badge.svg?branch=main)](https://coveralls.io/github/EdsonCaliman/banking.operation-client-api?branch=main)

This project is a part of a Banking Operation solution, with DDD and microservices architecture, using .Net Core.

![BankingOperations (1)](https://user-images.githubusercontent.com/19686147/133843637-85277ee1-9748-4456-befa-4b2265e3ebec.jpg)

Using a docker-compose configuration the components will be connected so that together they work as a solution.

This component will be responsible for notifications, It uses a RabbitMq for consume data.

![image](https://user-images.githubusercontent.com/19686147/155213575-d08095bf-a5bc-49b2-84ff-e7f115c2fbe6.png)

# How to run

With a docker already installed run:

docker-compose up -d
