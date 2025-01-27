# Express Voitures

**Express Voitures** est une application web de gestion de véhicules, permettant d’ajouter, modifier, supprimer des véhicules, de suivre les réparations effectuées et de gérer les annonces associées. Elle est conçue pour faciliter la gestion des véhicules, des transactions, et des annonces dans une interface simple, tout en respectant les exigences de sécurité, de conformité et d'accessibilité.

## Fonctionnalités principales

- **Gestion des véhicules** : Ajout, modification, suppression des véhicules avec possibilité d'ajouter des informations détaillées (marque, modèle, année, finition, etc.).
- **Suivi des réparations** : Enregistrement des réparations effectuées sur chaque véhicule, avec suivi des coûts associés.
- **Gestion des annonces** : Possibilité de publier des annonces avec photos, descriptions et disponibilité.
- **Suivi des transactions** : Enregistrement des achats et ventes, avec calcul automatique des prix de vente.

## Technologies utilisées

- **Back-end** : .NET Core avec architecture **MVC** (Modèle-Vue-Contrôleur)
    - Séparation des responsabilités pour une meilleure gestion du code
    - Sécurité renforcée via **Identity Framework** (gestion des rôles et authentification)
    - Facilité de gestion de la base de données avec **Entity Framework Core**

- **Front-end** : **Bootstrap** pour une interface responsive et intuitive
    - Adaptation automatique à tous les appareils (ordinateurs, tablettes, smartphones)
    - Conformité aux standards d’accessibilité

- **Base de données** : **Microsoft SQL Server** avec **Entity Framework Core**
    - Gestion des relations entre les données (véhicules, annonces, réparations, transactions)
    - Migrations pour assurer la cohérence des données

## Sécurité et accessibilité

- **Authentification et autorisation** : Seul l'administrateur peut ajouter, modifier ou supprimer des véhicules et des annonces. Le système utilise **Identity Framework** pour gérer les rôles et les permissions.
- **Accessibilité** : L'application répond aux normes d’accessibilité avec des labels explicites, des attributs ALT sur les images et une navigation clavier.

## Installation

1. **Clonez le dépôt** :
git clone https://github.com/DanyAMG/Projet_5

2. **Ouvrez le projet** dans Visual Studio ou Visual Studio Code.

3. **Installez les dépendances NuGet** :
- Entity Framework Core
- Identity Framework
- Bootstrap (intégré via le CDN ou en local)

4. **Configurez la base de données** dans le fichier `appsettings.json` :
- Modifiez les chaînes de connexion pour correspondre à votre base de données SQL Server.

5. **Exécutez les migrations** pour créer la base de données :
dotnet ef database update

6. **Lancez l'application** avec la commande suivante :
dotnet run

markdown
Copier

7. L'application sera accessible à l'adresse suivante : `http://localhost:5000` (ou l'URL configurée).

## Déploiement

- L'application peut être déployée sur un serveur Web, comme **IIS** ou **Azure**. Assurez-vous de bien configurer les chaînes de connexion et les certificats SSL pour garantir la sécurité des données.
- L’authentification et les rôles doivent être correctement configurés pour garantir une gestion sécurisée des utilisateurs.

## Problèmes connus

- Actuellement, certaines fonctionnalités de gestion des transactions sont limitées et nécessitent des améliorations pour mieux gérer les états de vente.
- L’interface peut être améliorée pour offrir une meilleure expérience utilisateur, notamment pour la gestion des images des véhicules.

## Contribution

Si vous souhaitez contribuer à ce projet, merci de suivre les étapes suivantes :
1. Forkez le projet.
2. Créez une branche de fonctionnalité : `git checkout -b feature/nouvelle-fonctionnalité`.
3. Faites vos modifications.
4. Soumettez une Pull Request avec une description détaillée des changements.

## Auteurs

- MOTA Dany
- **Mentor : Madjid Ouarab**

## Licence

Distribué sous la licence MIT. Voir le fichier `LICENSE` pour plus de détails.