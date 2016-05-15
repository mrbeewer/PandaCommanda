# PandaCommanda
This is the first game made at our first Game Jam. Theres still work to be done but we will get to that :)

How to build:

Desktop player will always host so when prompted click “Play and Host”. Mobile player will only click join at the bottom. Desktop player can start the game on their own but no one else will be able to join afterwards.
This game is meant for Desktop and mobile but if you build and run for Desktop and press play in the edit (Windows/Mac) They will still play nicely.
When you want to test on an actual mobile device just change to your platform of choice and build.
I have the IP address hard coded to 192.168.1.33 so it could work on my network. This will change but if you are having connectivity issues this very well could be the case. If you want to change it to your own IP address find the “LobbyMainMenu” script and in the function “OnClickJoin” change it to what ever you need. Once this is done no need to play with IPs anymore. 



Concept:

Panda Commanda is inspired by games like Ikaruga where the action is dependent of the “phase” in which the player is in.
I idea behind this game is that in order to destroy X type of object you just hit it will like X bullets. This game is also co-op and meant to have one Desktop player and one mobile player. The desktop players  job is to destroy as many blocks as possible while keeping their energy and health up. The mobile players job is to change to type of any “odd” typed objects to an appropriate one. In the first iteration of Panada Commanda the destroyable objects are either blue or red and the “odd” type objects are black. The mobile play must choose which color to change it to in order for the desktop player to destroy it. The mobile play is also in charge of gathering up and charging energy for the desktop player to use. No energy means no bullets.

Work Done****

Desktop

So far the basics of this concept have been implemented. Desktop players can move around and shoot while changing their bullet types. Firing reduces energy of that type. Blocks of different types also spawn and approach the player in a random fashion. 

Mobile

Mobile player can connect and observe the action in the scene. Player has the ability to change the type of a block by selecting they type they want to change it to and then tapping the block. Once the desktop player has destroy a block there is a chance for a “energy supply” to be dropped which increases the available amount of energy. Player can tap on the supply and allocate it to the energy type of their choice

Work to be done****

Desktop

There is no way to win currently nor any way to lose. 
Would like the game to be in  a Black and White theme to fit the awesome title (Panada Commanda)
Add additional bullet types 
fix spawning issues 

Mobile

create a better way to replenish energy
create system to keep mobile player active in monitoring energy supplies and reacting to it accordingly

Comments***

If anyone has any suggestions or has any questions please let me know!
Also this game is in dire need of an artists TLC!

Over the span of the next week or so I will be editing the scripts to make the code human readable. Currently its not as neat as I would like but in time I will be adding documentation and streamlining the code. If you have any questions on how something works or just would like to let me know of a better way to implement a feature I am all ears!



Credits**

Code:
James Robinson

Design:
James Robinson
