# TV Shows API

## Before you start

Few words before you start looking at the assignment.

This assignment was done from scratch. The scraper application and the API are both new. I wanted to keep it fair and I've attempted to do it in the **3 hours** time limit.

In a real-world scenario, I'd have done somethings differently. For example:

* I'd have added a valid certificate (from [https://letsencrypt.org/](https://letsencrypt.org/) for example)
* I'd have added proper security to the database
* I'd have added a cache in between the application and the database
* The scraping application would use a messaging system. The idea would be to separate the read (of the Maze API) and the write (to "our API"). In case of a huge amount of data, the current scenario wouldn't be advisable.

Of course, if you have any questions, I'll be very happy to answer them.

## Pre-requisites

We need to install:

- [Docker](https://www.docker.com/get-started)
- [dotnet tools](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools)
- [Cake Build](https://cakebuild.net)
  - `dotnet tool install Cake.Tool --version 0.33.0 -g`

## How to run

1. First thing we need to do is to clone this repository:

```bash
git clone https://github.com/solvingpuzzles/tv-shows-info-api.git
```

2. Second, we need to bring up the infrastructure defined in the `docker-compose.yml` file:

```bash
cd tv-shows-info-api/TvShowsApi
docker-compose up --build -d
```

3. After it's finished, both the database and the API are up and running. Verify that you can talk with the API (give it a couple of seconds, in order to make sure the API is up and running):

```bash
curl localhost:5080/api/shows
[]
```

4. It returns an empty list because we didn't scrape the Maze API. We need to import the TV Shows information, and to do that we need to:

```bash
# from the point where we were
cd ../TvShowsScraper
dotnet cake
dotnet app/Host.dll
```

5. After all of these steps are complete, we should be able to query our API:

```bash
# for all results
curl localhost:5080/api/shows
```

6. You can also use [Postman](https://www.getpostman.com/downloads/) to query the API.

