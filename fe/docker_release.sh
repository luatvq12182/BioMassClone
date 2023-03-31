docker build -t ncmg-react-app .
docker container stop ncmg_fe
docker container rm ncmg_fe
docker run -d -p 80:5173 --name ncmg_fe ncmg-react-app