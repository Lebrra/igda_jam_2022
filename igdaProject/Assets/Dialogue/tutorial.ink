#mumble
Professor: Oh a new intern!
#mumbleAngry
Professor: You're late!
#mumble
Professor: Welcome to the lab!

->My_Choices

=== My_Choices ===
#mumble
Professor: Are you ready to get started or would you like me to run you through the basics?
+ [Tell me more doc!] ->TutorialBegin
+ [I can figure it out on my own.] ->AskAgain
 
 ===AskAgain===
 #mumble
 Professor: Are you sure?
 +[yes] ->theEnding
 +[no] ->My_Choices
 
 ===theEnding===
#mumbleAngry
  Professor: Okay, good luck intern!
  #mumble
  Professor: Try not to get a papercut.
  #mumble
  Professor: KEKEKEKEKE!
 #TutorialDone
 ->END

===TutorialBegin===
#mumble
Professor: Okay so there are two main tasks we perform.
#mumble
Professor: To help you perform these tasks initially you will be given two different types of animals to begin working with.
#mumble
Professor: The <b>Cat</b> and the <b>dog</b>, simple <b>House</b> creatures.
#mumble
Professor: There are many different types of <b>BIOMES</b>. One we had covered, the <b>House</b> biome.
#mumble
Professor: There are many other <b>BIOMES</b> which you will learn about in your time here which include <b>Forest</b>, <b>Mountain</b>, <b>Ocean</b>, and many more.
#mumble
Professor: The two tasks are <b>Collect Materials(Combat)</b> and <b>Research New Creatures</b>.
#mumble
Professor: Which task would you like to hear about first?
->Tutorial


===Tutorial===
+[Research New Creatures.] ->Tutorial.research
+[Collect Materials]       ->Tutorial.combat
+[I've heard enough.]
#mumble
Professor: Goodluck!
#mumble
Professor: There are even rumors of <b>mythical</b> creatures.
#mumble
Professor: But be careful intern try not to take on more than you can handle. We only have so much tape and band-aids on hand with the recent budget cuts.
#TutorialDone
->END

=research
#openerPanel
#mumble
Professor: First please, take a look at that magnifying glass.
#mumble
#closeOpener
#creatureCreationPanel
Professor: You will be able to click that and pick between the <b>4</b> different body parts.
#mumble
Professor: The <b>Head</b>, the <b>Legs</b>, the <b>Body</b> and the <b>Tail</b>.
#mumble
Professor: You will find many different types of <b>parts</b>
#mumble
Professor: Mix and match to your hearts content.
#mumble
Professor: The possibilities are endless!
#closeAll
#mumble
Professor: Would you like to hear about anything else?
->Tutorial
=combat
#combatPanel
Professor: So your creatures have a certain level of strength. That strength can be augmented by using the parts of other creatures.
#mumble
Professor: This augmentation will introduce new <b>abilities</b> both passively and actively.
#mumbleAngry
Professor: YES!!! New Abilities.
#mumble
Professor: It's very marvelous to experience.
#mumble
Professor: There are two different ways you can combat creatures.
#mumble
Professor: You can go on an <b>Adventure</b> into the nearby wilderness.
#mumble
Professor: Or
#mumble
Professor: You can <b>Randomly Battle</b> creatures created by other lab assistance.
#mumble
Professor: The latter will not yield any new parts but you can see many interesting creatures like the one here.
#mumble
#closeAll
Professor: Would you like to hear about anything else?
->Tutorial