# nagp-albumapp
The aplication retrieves the list of music albums from database.

Docker image Url: https://hub.docker.com/repository/docker/prateeksharma03/albumapi/general 

# Steps to create docker image:

1. Login to docker account:

   docker login

2. Build docker image:

   docker build -t prateeksharma03/albumapi .

3. Push docker image to your docker hub account:

   docker push prateeksharma03/albumapi


# Execute below kubectl commands to run the application from root directory :

 kubectl apply -f ./k8/albumapp/namespace.yml
 
 kubectl config set-context --current --namespace=album
 
 kubectl apply -f ./k8/mongo/secret.yml
 
 kubectl apply -f ./k8/albumapp/configmap.yml
 
 kubectl apply -f ./k8/mongo/mongo-pv.yml
 
 kubectl apply -f ./k8/mongo/mongo-statefulset.yml
 
 kubectl apply -f ./k8/mongo/mongo-service.yml
 
 kubectl apply -f ./k8/albumapp/albumapp.yml
 
 
 # Url: http://localhost/api/album/getall      //To get list of albums from mongo db
 
 
 
