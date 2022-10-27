#anim talk
Professor: Oh a new intern!
#anim talk
Professor: You're late!

Professor: Welcome to the lab!

->My_Choices

=== My_Choices ===
Professor: Are you ready to get started or would you like me to run you through the basics?
+ [Please guide me senpai!] ->TutorialBegin
+ [I can figure it out on my own.] ->AskAgain
 
 ===AskAgain===
 Professor: Are you sure?
 +[yes] ->theEnding
 +[no] ->My_Choices
 
 ===theEnding===
  Professor: Okay, good luck intern!
  Professor: Try not to get a papercut.
  Professor: KEKEKEKEKE!
 #TutorialDone
 ->END

===TutorialBegin===
Professor: Okay so there are two main tasks we perform.
Professor: To help you perform these tasks initially you will be given two different types of animals to begin working with.
Professor: The <b>Cat</b> and the <b>dog</b>, simple <b>House</b> creatures.
Professor: There are many different types of <b>BIOMES</b>. One we had covered, the <b>House</b> biome.
Professor: There are many other <b>BIOMES</b> which you will learn about in your time here which include <b>Forest</b>, <b>Mountain</b>, <b>Ocean</b>, and many more.

Professor: The two tasks are <b>Collect Materials(Combat)</b> and <b>Research New Creatures</b>.
Professor: Which task would you like to hear about first?
->Tutorial


===Tutorial===
+[Research New Creatures.] ->Tutorial.research
+[Collect Materials]       ->Tutorial.combat
+[I've hear enough.]
Professor: Goodluck!
Professor: There are even rumors of <b>mythical</b> creatures.
Professor: But be careful intern try not to take on more than you can handle. We only have so much tape and band-aids on hand with the recent budget cuts.
#TutorialDone
->END

=research
#openerPanel
Professor: First please, take a look at that magnifying glass.
#closeOpener
#creatureCreationPanel
Professor: You will be able to click that and pick between the <b>4</b> different body parts.
Professor: The <b>Head</b>, the <b>Legs</b>, the <b>Body</b> and the <b>Tail</b>.
Professor: You will find many different 
Professor: Mix and match to your hearts content.
Professor: The possibilities are endless!
#closeAll
Professor: Would you like to hear about anything else?
->Tutorial
=combat
#combatPanel
Professor: So your creatures have a certain level of strength. That strength can be augmented by using the parts of other creatures.
Professor: This will introduce new <b>abilities</b> both passively and actively.
Professor: Would you like to hear about anything else?
->Tutorial