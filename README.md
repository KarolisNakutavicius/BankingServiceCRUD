# BankingServiceCRUD

CRUD for banking tracking banking account and its statements

After cloning/downloading:

cd BankingService

docker build -t name .

docker run -p 8080:80 name


Some sample JSON data for POST method

curl -X POST "https://localhost:44325/BankAccounts" -H "accept: text/plain" -H "Content-Type: application/json" -d "{\"clientID\":0,\"accountName\":\"Cerol\",\"statements\":[{\"statementID\":0,\"operationType\":\"Expense\",\"amount\":100}]}"

https://localhost:8080/BankAccounts

Body - 
  {
    "clientID": 0,
    "accountName": "Cerol",
    "statements": [
      {
        "statementID": 0,
        "operationType": "Expense",
        "amount": 100
      }
    ]
  }
