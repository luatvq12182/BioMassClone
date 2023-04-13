sudo docker stop green-way-cms-ins
sudo docker rm green-way-cms-ins
sudo docker image rm green-way-cms
sudo docker build -t green-way-cms .
sudo docker run --name green-way-cms-ins -dp 3000:3000 green-way-cms