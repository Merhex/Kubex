Voor het eerste gebruik:
 - cd Kubex.DAL
 - dotnet ef database update -s "<path naar .API folder>"

Standaard wordt er al één gebruiker aangemaakt.
User: admin
Pass: !1Password

Gebruikers die hierna aangemaakt worden zijn automatisch van het type "agent".

Er is een backup van de test data die wij al hebben in de folder, database.sql


Om te starten:
- cd kubex.api
- dotnet run

- cd kubex.spa
- ng serve -o
