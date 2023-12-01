kubectl delete deployment meal-frontend
kubectl delete deployment meal-backend
docker-compose build 
docker tag mealfrontend jackyma001/mealfrontend
docker tag mealbackend jackyma001/mealbackend
docker push jackyma001/mealfrontend
docker push jackyma001/mealbackend
kubectl apply -f .\frontend-deployment.yml
kubectl apply -f .\backend-deployment.yml