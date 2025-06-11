README de la Bibliothèque Virtuelle — Projet en .NET Core

Oyez, nobles lecteurs, l’exposé fidèle d’un ouvrage conçu en cette glorieuse ère numérique, destiné à l’instruction des esprits studieux et curieux des lettres, tant digitales que classiques.

Dans le royaume savant de l’informatique, fut jadis conçu un programme appelé Bibliothèque, non point pour remiser poussières et grimoires, mais pour gérer avec grâce une collection de livres en formats variés : ebooks, livres papiers, et ce, via une double interface, tant API que console.

Ce noble système permet :

D’enregistrer des ouvrages avec précision ;

D’en consulter les titres avec promptitude ;

D’en supprimer les moins estimés ;

Et de rechercher, selon le nom d’auteur ou le titre, ceux qui méritent d’être lus à voix haute au coin du feu.

Structure de l’œuvre

Bibliotheque/
│
├── 📜 Program.cs               # Point d’entrée de la divine mécanique
├── 📜 appsettings.json         # Parchemin des secrets de configuration
│
├── 🎭 Controllers/
│   └── LivreController.cs     # Maître d’orchestre des routes API
│
├── 📚 Models/
│   ├── Media.cs               # Classe générique pour tout ouvrage
│   ├── Ebook.cs               # Spécialisation numérique
│   ├── PaperBook.cs           # Spécialisation reliée
│   └── IReadable.cs           # Contrat de lisibilité
│
├── 🏛 Data/
│   └── BibliothequeContext.cs # Contexte de la base, noble médiateur avec la SQLite
│
├── 🧭 Repositories/
│   ├── IRepository.cs         # Interface aux promesses maintes fois tenues
│   └── Repository.cs          # Fidèle exécutant des volontés de l’utilisateur
│
├── 🧪 Utils/
│   └── MiniValidator.cs       # Contrôleur de conformité des ouvrages
│
├── 🤖 Clients/
│   └── ClientConsole.cs       # Interface de l’homme de lettres via terminal
│
└── 📁 Migrations/              # Témoins historiques des évolutions de la base

Comment lancer la machine infernale ?

« Il faut des soins constants pour faire parler les machines avec éloquence. »

Assurez-vous d’avoir .NET installé (version 7.0 ou supérieure).

Puis, en commandement clair et ferme :

dotnet restore     # Faites venir les dépendances par la magie
dotnet ef database update  # Préparez la base avec le soin d’un bibliothécaire
dotnet run         # Lancez l’œuvre et observez-la vivre

Fonctionnalités offertes

Voir tous les livres

Rechercher par auteur ou titre

Ajouter un ebook ou un livre papier

Modifier les ouvrages

Supprimer avec doigté

Et tout cela, soit par requête HTTP, soit par interface console pour les plus classiques.

Exemple d’appel HTTP

GET /livres/search?author=Molière&title=Avare

Auteurs et contributeurs

Monsieur Imrane MEBITIL, Mohamed HADDAD, Pierre Godart
