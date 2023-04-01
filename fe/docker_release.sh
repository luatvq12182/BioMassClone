docker build -t green-way-fe .
# docker container stop green-way-fe
# docker container rm green-way-fe
docker run -d -p 80:5173 --name green_way_fe green-way-fe