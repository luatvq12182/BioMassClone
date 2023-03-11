sudo docker stop back-end-api
sudo docker rm back-end-api
sudo docker build -t green-way-api .
sudo docker run --name back-end-api -dp 80:80 green-way-api