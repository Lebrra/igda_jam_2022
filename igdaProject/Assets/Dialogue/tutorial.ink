#anim talk
Professor: Oh a new intern!
#nameInsert
#anim talk
Professor: You're late!

Professor: Welcome to the lab!

->My_Choices

=== My_Choices ===
Are you Ready to get started or would you like me to run you through the basics?
+ [Please guide me senpai!] ->TutorialBegin
+ [I can figure it out on my own.] ->AskAgain
 
 ===AskAgain===
 Are you sure?
 +[yes] ->theEnding
 +[no] ->My_Choices
 
 ===theEnding===
  Okay, good luck intern!
  Try not to get a papercut.
  KEKEKEKEKE!
 #TutorialDone
 ->END

===TutorialBegin===
Okay so there are two main tasks we perform.
To help you perform these tasks initially you will be given two different types of animals to begin working with.
The <b>Cat and the <b>dog</b>, simple <b>House</b> Creatures.
There are many different types of <b>BIOMES</b>. One we had covered, the <b>House</b> biome.
There are many other <b>BIOMES</b> which you will learn about in your time here which include <b>Forest</b>, <b>Mountain</b>, <b>Ocean</b>, and many more.

The two tasks are <b>Collect Materials(Combat)</b> and <b>Research New Creatures</b>.
Which task would you like to hear about first?
->Tutorial


===Tutorial===
+Research New Creatures. ->Tutorial.research
+Collect Materials       ->Tutorial.combat
+I've hear enough.
Goodluck!
There are even rumors of <b>mythical</b> creatures.
But be careful intern try not to take on more than you can handle. We only have so much tape and band-aids on hand with the recent budget cuts.
#TutorialDone
->END

=research
#openerPanel
First please, take a look at that magnifying glass.
#closeOpener
#creatureCreationPanel
You will be able to click that and pick between the <b>4</b> different body parts.
The <b>Head</b>, the <b>Legs</b>, the <b>Body</b> and the <b>Tail</b>.
You will find many different 
Mix and match to your hearts content.
The possibilities are endless!
#closeAll
Would you like to hear about anything else?
->Tutorial
=combat
#combatPanel
So your creatures have a certain level of strength. That strength can be augmented by using the parts of other creatures.
This will introduce new <b>abilities</b> both passively and actively.
Would you like to hear about anything else?
->Tutorial