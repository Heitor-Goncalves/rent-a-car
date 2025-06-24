az acr login --name acrlab007heitor

docker tag bff-rent-car-local acrlab007heitor.azurecr.io/bff-rent-car-local:v1

docker push acrlab007heitor.azurecr.io/bff-rent-car-local:v1