sudo docker stop green-way-api-ins
sudo docker rm green-way-api-ins
sudo docker image rm green-way-api
sudo docker build -t green-way-api .
sudo docker run --name green-way-api-ins -dp 7000:80 green-way-api