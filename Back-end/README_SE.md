Korte uitleg over de gamaakte producten die te vinden zijn onder deze directory

Back-end/API:
Het verbindt stuk van de database naar de buiten.
We hebben in dit project mogelijk gemaakt om een MySQL database server op te zetten in Azure die aangeroepen wordt door de API, 
terwijl de APi en de Database niet op dezelfde machine zitten.

de API is draaiend op SV-01, terwijl de database draaiend is op SV-02. Instructies voor het opstarten van deze machines kunnen gevonden worden in ../../../AzureOmgeving_SC_WIV.docx
De code is gebasseerd op de code die we hadden ontvangen van de heer Amkreutz, met enkele aanpassingen die dan voldoen onze eisen voor het project. 
Inclusief aanpassingen om de code om te zetten van SqLite naar MySql.

Na het activeren van de servers in Azure, is de API bereikbaar via het domein: api.wiv.one


Back-end/Map API:
Het programma is te starten door onder /Map API de map.sln op te starten.
Vervolgens zou er na het opstarten van het programma in Visual Code Community, start de applicatie door de start knop te drukken bovenaan het programma (Dit kan even duren).

Na het opstarten zal er een applicatie ten toon gesteld worden genaamd Maps.
Hierin bevinden er zich 4 knoppen:
    1. Waarneming maken
    2. Navigatie
    3. Profiel
    4. Leaderbord

1. Waarneming maken:
    Onder deze knop is het mogelijk om waarnemingen vast te stellen en er een foot aan toe te voegen. 
    Wanneer u op verzenden klikt zal deze waarneming via de API gePOST worden op de Database.
    Daarbij haalt het programma ook uw locatie wanneer u op 'Waarneming maken' klikt en zal het die meesturen met de waarneming.

2. Navigatie:
    Een kaart van de wereld die wordt aangeroepen via Microsoft Azure op basis van sataliet beelden van Bing.
    Wanneer u op de knop 'Update Marker' klikt, wordt uw locatie weergegeven. Zowel als in de pop-up, als op de kaart met een blauwe marker.

3. Profiel:
    Hierin kunt u alle waarnemingen terug vinden die er zijn gemaakt. (Dit kan even duren met laden)
    Dit kunt u ook testen door de SV-01 & SV-02 te starten samen met de instructies in ../../../Azureomgeving_SC_WIV.docx.

    Als u na het opstarten van de servers een waarneming plaatst, zal die hier ook terug te vinden zijn.
    Bij het dubbel klikken op een waarneming kan meer informatie ingezien worden over die waarneming.
    Daarnaast is het ook mogelijk bij het selecteren van een waarneming, om die te verwijderen door middel van de 'delete' knop.

4. Leaderboard
    Work in Progress...