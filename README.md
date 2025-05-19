# Funda assignment
<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
  </ol>
</details>

## About The Project

![alt text](https://images.ctfassets.net/alcjc9drsa23/6F74ADPFMYsmjSob3NZqMI/a57b61bee1fbcaf0834e0b94d36e46c5/Fallback-image-funda.jpg?fm=webp)

This repository was made with the sole purpose of making the Funda assessment. 

## Getting Started

Below you will find the locally needed software in order to run this solution correctly.

### Requirements

Make sure you have installed on your machine:
* .NET 9 SDK
* Docker

### Installation
1. Clone this repository
```sh
git clone https://github.com/bakfietsje/fundaAssignment.git
```
2. Create a copy of appsettings.json and name it appsettings.local.json and replace the apikey with one provided by Funda.

### Usage
1. Run the docker image using this command
```sh
docker-compose build     
```
2. Run the web api in detached mode in order to access it interactively with the console later
```sh
docker-compose up -d webapi   
```
3. Run the console application
 ```sh
docker-compose run --rm consoleapp
```
4. You should now be able to interact with the console through the terminal




## Acknowledgments

As requested, AI has been used for the formatting of this readme and spelling mistakes and better readability. Further more, due to limited experience with console applications. AI has helped me create the config part of the program.cs. 
