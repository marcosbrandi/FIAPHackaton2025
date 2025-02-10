#!/bin/bash

# Vari�veis
DOCKER_HUB_USERNAME="seu-dockerhub-username"
DOCKER_HUB_TOKEN="seu-dockerhub-token"
IMAGE_NAME="hm-api"
TAG="latest"
SSH_HOST="seu-servidor"
SSH_USERNAME="seu-usuario"
SSH_KEY_PATH="/caminho/para/sua/chave/privada"

# 1. Fazer login no Docker Hub
echo "Fazendo login no Docker Hub..."
docker login -u $DOCKER_HUB_USERNAME -p $DOCKER_HUB_TOKEN

# 2. Construir as imagens usando docker-compose
echo "Construindo as imagens Docker..."
docker-compose -f docker-compose.yml build

# 3. Executar testes (se houver)
echo "Executando testes..."
docker-compose -f docker-compose.yml up --abort-on-container-exit

# 4. Fazer push das imagens para o Docker Hub
echo "Fazendo push das imagens para o Docker Hub..."
docker tag ${DOCKER_REGISTRY-}hmapi $DOCKER_HUB_USERNAME/$IMAGE_NAME:$TAG
docker push $DOCKER_HUB_USERNAME/$IMAGE_NAME:$TAG

# 5. Deploy no servidor (usando SSH)
echo "Fazendo deploy no servidor..."
ssh -i $SSH_KEY_PATH $SSH_USERNAME@$SSH_HOST << 'EOF'
  docker-compose -f docker-compose.prod.yml down
  docker-compose -f docker-compose.prod.yml pull
  docker-compose -f docker-compose.prod.yml up -d
EOF

echo "CI/CD conclu�do com sucesso!"