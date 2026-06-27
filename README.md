<!-- Provide an overview of what your template package does and how to get started.
Consider previewing the README before uploading (https://learn.microsoft.com/en-us/nuget/nuget-org/package-readme-on-nuget-org#preview-your-readme). -->

# Notes

## check if template is installed

¨¨¨
dotnet new list console
¨¨¨

## Install

¨¨¨
dotnet new install ./
¨¨¨

## uninstall

¨¨¨
dotnet new uninstall ./
¨¨¨

## pack

¨¨¨
dotnet pack -c Release
¨¨¨

## install pack

¨¨¨
dotnet new install bin/Release/alainranger.consoledi.template.1.0.0.nupkg
¨¨¨

## uninstall pack

¨¨¨
dotnet new uninstall bin/Release/alainranger.consoledi.template.1.0.0.nupkg
¨¨¨

## ⚙️ Comment fonctionne ce pipeline (Explications)

• Déclenchement par Tag (on.push.tags) : C'est la méthode la plus propre pour gérer les releases. Le pipeline ne va pas publier sur NuGet à chaque simple commit sur main. Il attendra que vous créiez une version officielle (ex: git tag v1.0.0 suivi de git push --tags).
• Extraction dynamique de la version : L'étape Extract Version from Tag prend le nom de votre tag Git (ex: v2.4.1), retire le "v" pour obtenir 2.4.1, et l'injecte directement dans la commande dotnet pack via l'argument -p:PackageVersion. Plus besoin de modifier manuellement le fichier .csproj à chaque fois !
• --no-restore et --no-build : Ces drapeaux permettent d'éviter que .NET ne re-compile le projet inutilement à chaque étape (restore -> build -> test -> pack), ce qui fait gagner de précieuses secondes sur GitHub Actions.
• --skip-duplicate : Sécurité très pratique. Si pour une raison quelconque le pipeline s'exécute deux fois pour la même version, cette option évite que le script ne plante en bloquant poliment la republication d'un package identique.
