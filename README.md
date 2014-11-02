**Hex-Board**
========================================================================================
__________________________________________________________________________________

This is a turn-based/rouge-like/strategy/"boardgame" game built with unity. The game will feature movement based attacking which will require strategic thinking when moving to different places on the "board". The theme is space and will use "lowpoly" style for modeling to keep thingssimple and I also think it looks good.

To start using this repo either clone or download somewhere onto your computer and import the project into unity (currently using free version). What i do is just click on my scene that is located in the assests folder and it will start the project from there.


**Ultimate Todo List:**
=======================================================================================
	
~~Reset tile checker~~ (a little buggy)

~~Change the character movement to something simpler and easier. (See CharController.cs)~~

~~Multiple enemies on board at once~~ (still needs lots of polishing)

~~Graphical representation of health~~

~~Make it so the enemy cannot move to player tile~~ (little on the buggy side but works)

~~Add funtionality to "red tiles" so that the enemy can hurt the player (make an array of all)~~ (very basic)

Update pathfinding for enemies to not be able to run onto same tile as player and enemy

Randomly instantiate enemies onto different tiles at start of game

Fix when enemies can move after the player has moved (this already kind of works..could be more "turn based")

Multiple Types of enemies e.g. (archer, mage, bomber, berzerker...) this also means different AI behavior for each type

Create upgrades e.g. (uses of longshot, sheild, FTL boosts) that can be found on board during gameplay

Player can toggle a longer shot with a button, to kill something further away from him (by 2 tiles and can be upgraded)

Make tiles all the same so that you dont know which tiles to go to (only when prototype is near completion)

Main Menu (either new scene or in the current scene it self chris prefers the latter)

At end of level queue harder level/increase dificulty from stage to stage

Start Screen

End Game

Reset Game

Pause Game

Save game (maybe?)


**Ideas**
==================================================================================

Make a "galaxy map" like a world map of all the levels you can go to but a galaxy map because you go to different galaxies. levels would unlock over time or with certain amount of resources.

Take a more 4xish approach, as in: every tile is a planet (which I want to do regardless of taking this approach) and when going to the planet you get options to get resources or other items or it triggers enemies etc... With this approach i would also make a spawn timer until the enemies would populate the board thus making you rush to get resources and what not before the fighting begins. Fighting could also begin when you first go to the level.

Enemies come in waves and you cant leave world or seek planets if enemies are on board

Spend resources to upgrade your ship.. ie (sheild, ftl boosts, sniper attacks, bombs, ) also upgrades look of ship.

The Story is simple and minimal. You are trapped in the void of space and you must get home. home will be in the last galaxy of the galaxy map. you find "artifacts" to reach more galaxies in the universe. Each universe contains up to 3 artifacts.


**Overall Plan**
==================================================================================

Prototype (current)
 
 	\/

Test/Generate Content
  
  	\/
   
Refine Game

	\/
   
Generate Content

	\/
   
Test/Polish...Forever! or until Done!
 	 
