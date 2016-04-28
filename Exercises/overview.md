# Exercise Overview


## Introduction

The exercises are used to show different aspects of Microsoft Azure and Visual Studio that are relevant for DevOps. The goal is to have a consistent sample that attendees can build step by step. The sample is simple so that everybody can follow. However, it contains some requirements and implementation details that are typical for real-world web applications.


## Goal of the Sample

The goal of the sample is to implement a simple Web API that could be used in an online book shop. The Web API should return a list of available books with ID, title, description and price.

The sample does not require a backend storage. Therefore, the books are generated at runtime. The number of returned books and their titles should be random. The following screenshot shows a typical result of the Web API:

![Web API result](img/postman.png)

The generation of the book titles is done by randomly combining three text parts:

* Book titles start with phrases like *The, A, A great, One hell of a*,...
* Then phrases like *Red, Blue, awesome, good, horrible*,... follow
* At the end, phrases like *Street, Road, Car, House*,... are appended

The sample also contains a simple Web UI that consumes the Web API mentioned above:

![Web UI](img/angular-web-ui.png)


## Session Structure

* The exercises are built for a **two-days workshop**.
* Every day consists of **four sessions**.
* You can use one exercise per session. However, if you audience is especially initerested in certain topics, you can spend more time on certain exercises and keep the other ones shorter.

