FROM node:latest as build

# Set working directory
WORKDIR /usr/app

#Install PNPM
RUN curl -f https://get.pnpm.io/v6.16.js | node - add --global pnpm

# Copy all files
COPY ./ ./

# Install Modules
RUN pnpm install

# Build app
RUN pnpm run build

# nginx state for serving content
FROM nginx:latest as final
EXPOSE 80
EXPOSE 443
WORKDIR /usr/share/nginx/html
COPY --from=build /usr/app/dist .
# Containers run nginx with global directives and daemon off
ENTRYPOINT ["nginx", "-g", "daemon off;"]