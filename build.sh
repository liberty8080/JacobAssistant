containerName="jacob-assistant";imageName="jacob-assistant:latest"
oldContainer=`docker ps -aq -f name=${containerName}`
oldImage=`docker images -aq ${imageName}`
if [ -n ${oldContainer} ]; then docker rm -f ${containerName}; fi
if [ -n ${oldImage} ];  then docker rmi ${oldImage}; fi
docker build . -t ${imageName}
docker run -d -p 8081:8080 --name jacob-assistant ${imageName} --restart=always