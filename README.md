# BankingServiceCRUD

CRUD for tracking banking accounts and its statements

After cloning/downloading:

cd BankingService

docker build -t name .

docker run -p 8080:80 name


Some sample JSON data for POST method

CURL:
curl -X POST "https://localhost:44325/BankAccounts" -H "accept: text/plain" -H "Content-Type: application/json" -d "{\"clientID\":0,\"accountName\":\"Cerol\",\"statements\":[{\"statementID\":0,\"operationType\":\"Expense\",\"amount\":100}]}"


POSTMAN:
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
