
# Backend API

L'objectif de cette application est de reproduire un simili de l'application https://letterboxd.com/.

Letterboxd est un réseau social dont l'intéret principal est d'écrire des reviews de film et de les noter, ainsi que de suivre d'autre utilisateurs pour voir les films qu'ils ont vu récemment, leurs films favoris et leurs avis. 

Ce repo concerne le backend de l'application sous la forme d'une API. L'API est accessible ici en version en test : [Lien de vers l'API deployé sur Azure](https://webapibackend-0xmaxime.azurewebsites.net/Swagger/index.html).


## Technologies utilisés
- .NET Core 6 
- EntityFrameworkCore

## Description des models  
L'application possède 6 models :

- Acteur 
- Directeur
- Film 
- ActeurInMovie : Model qui fait lien entre les films et les acteurs qui ont un Role. 
- Utilisateur 
- Review : Enregistrement d'un film par un utilisateur avec une note et un commentaire.

## Authentification
Certaines actions utilisateur nécessitent d'etre connecté en tant qu'utilisateur. Comme suivre un autre utilisateur ou écrire une review sur un film. 

L'authentification se fait en se connectant via la fonction Loggin qui retourne un token Jwt. Via Swagger le token peut etre ajouter en haut de la page en rentrant : 'Bearer *leTokenARentrer*'
