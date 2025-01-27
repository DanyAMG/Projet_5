# Express Voitures

**Express Voitures** est une application web de gestion de v�hicules, permettant d�ajouter, modifier, supprimer des v�hicules, de suivre les r�parations effectu�es et de g�rer les annonces associ�es. Elle est con�ue pour faciliter la gestion des v�hicules, des transactions, et des annonces dans une interface simple, tout en respectant les exigences de s�curit�, de conformit� et d'accessibilit�.

## Fonctionnalit�s principales

- **Gestion des v�hicules** : Ajout, modification, suppression des v�hicules avec possibilit� d'ajouter des informations d�taill�es (marque, mod�le, ann�e, finition, etc.).
- **Suivi des r�parations** : Enregistrement des r�parations effectu�es sur chaque v�hicule, avec suivi des co�ts associ�s.
- **Gestion des annonces** : Possibilit� de publier des annonces avec photos, descriptions et disponibilit�.
- **Suivi des transactions** : Enregistrement des achats et ventes, avec calcul automatique des prix de vente.

## Technologies utilis�es

- **Back-end** : .NET Core avec architecture **MVC** (Mod�le-Vue-Contr�leur)
    - S�paration des responsabilit�s pour une meilleure gestion du code
    - S�curit� renforc�e via **Identity Framework** (gestion des r�les et authentification)
    - Facilit� de gestion de la base de donn�es avec **Entity Framework Core**

- **Front-end** : **Bootstrap** pour une interface responsive et intuitive
    - Adaptation automatique � tous les appareils (ordinateurs, tablettes, smartphones)
    - Conformit� aux standards d�accessibilit�

- **Base de donn�es** : **Microsoft SQL Server** avec **Entity Framework Core**
    - Gestion des relations entre les donn�es (v�hicules, annonces, r�parations, transactions)
    - Migrations pour assurer la coh�rence des donn�es

## S�curit� et accessibilit�

- **Authentification et autorisation** : Seul l'administrateur peut ajouter, modifier ou supprimer des v�hicules et des annonces. Le syst�me utilise **Identity Framework** pour g�rer les r�les et les permissions.
- **Accessibilit�** : L'application r�pond aux normes d�accessibilit� avec des labels explicites, des attributs ALT sur les images et une navigation clavier.

## Installation

1. **Clonez le d�p�t** :
git clone https://github.com/DanyAMG/Projet_5

2. **Ouvrez le projet** dans Visual Studio ou Visual Studio Code.

3. **Installez les d�pendances NuGet** :
- Entity Framework Core
- Identity Framework
- Bootstrap (int�gr� via le CDN ou en local)

4. **Configurez la base de donn�es** dans le fichier `appsettings.json` :
- Modifiez les cha�nes de connexion pour correspondre � votre base de donn�es SQL Server.

5. **Ex�cutez les migrations** pour cr�er la base de donn�es :
dotnet ef database update

6. **Lancez l'application** avec la commande suivante :
dotnet run

markdown
Copier

7. L'application sera accessible � l'adresse suivante : `http://localhost:5000` (ou l'URL configur�e).

## D�ploiement

- L'application peut �tre d�ploy�e sur un serveur Web, comme **IIS** ou **Azure**. Assurez-vous de bien configurer les cha�nes de connexion et les certificats SSL pour garantir la s�curit� des donn�es.
- L�authentification et les r�les doivent �tre correctement configur�s pour garantir une gestion s�curis�e des utilisateurs.

## Probl�mes connus

- Actuellement, certaines fonctionnalit�s de gestion des transactions sont limit�es et n�cessitent des am�liorations pour mieux g�rer les �tats de vente.
- L�interface peut �tre am�lior�e pour offrir une meilleure exp�rience utilisateur, notamment pour la gestion des images des v�hicules.

## Contribution

Si vous souhaitez contribuer � ce projet, merci de suivre les �tapes suivantes :
1. Forkez le projet.
2. Cr�ez une branche de fonctionnalit� : `git checkout -b feature/nouvelle-fonctionnalit�`.
3. Faites vos modifications.
4. Soumettez une Pull Request avec une description d�taill�e des changements.

## Auteurs

- MOTA Dany
- **Mentor : Madjid Ouarab**

## Licence

Distribu� sous la licence MIT. Voir le fichier `LICENSE` pour plus de d�tails.