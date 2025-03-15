
# CryptoPriceAggregator

CryptoPriceAggregator is a .NET-based application that aggregates cryptocurrency prices from multiple providers.

## Prerequisites

Ensure you have the following tools installed:

1. **Docker**: [Install Docker](https://www.docker.com/get-started).
2. **Docker Compose**: [Install Docker Compose](https://docs.docker.com/compose/install/).

Verify installation by running:

```bash
docker --version
docker-compose --version
```

## Running the Application

### 1. Clone the Repository

Clone the repo to your local machine:

```bash
git clone https://github.com/ZhiKaanZhi/CryptoPriceAggregator.git
cd CryptoPriceAggregator
```

### 2. Build and Start the Application

Run the following command to build and start the application:

```bash
docker build -t crypto-price-api .
docker run -p 5000:80 crypto-price-api
```

This will expose the app on port `5000`.

### 3. Access the Application

Once running, access it at:

```
http://localhost:5000/swagger
```

Or you can run a curl command:

```bash
curl -v http://localhost:5000/api/prices/2025-03-15T10:00:00
```

Or you can access it directly from your browser:
```
http://localhost:5000/api/prices/2023-01-01T00:00:00
```

### 4. Stop the Application
Run the following commands to stop the application:
```bash
docker ps  # Find CONTAINER ID
docker stop <CONTAINER_ID>
```