(written at time of original deadline)

Controls:
click - place selected building type
I - industrial
C - commercial
escape - cancel
enter - end turn with no action

yellow = residential (potential workforce)
dark blue = water
dark green = forest (materials for industry)
fuschia = cultural center	(appears on its own in unchanging areas far from industry)
tan = industry (generates goods, pushes away culture)
teal = commercial (converts goods to wealth)


Pitch:

Create a spatial economy of systems that you guide but don't directly control.

Intent:

I wanted to create an economy that dealt primarily in manipulating space directly rather than currencies. The player only directs the structure of the systems and the metrics they are presented largely represent the balance of the system they've created rather than a bank roll. Additionally I wanted to create a system that would promote values and themes that have been important to Canada: the importance and difficulty of preserving culture, nature, and balancing the right amount of government oversight.

Implementation:

There is a relative abstraction of the hierarchy of needs in how happiness is calculated that I hope can lead to a similar, but challenging hierarchy of objectives. Whether people have jobs is a major factor that is relatively easily solved by building industry to match population growth. This also enables commerce sectors which raise happiness through comfort. But I'm building these two up easily, and raising wealth in the process, culture is put at risk since non residential development hurts it. As culture is also harder to cultivate with very specific conditions for new sectors, this can lead to a challenge of plateauing as people rely on culture and a relative lack of industry for fulfillment which plays a large role in happiness. Ideally this will be naturally highlighted to the player on an early play through as the map naturally starts with an abundance of culture given the population, confronting them with it's importance when it vanishes.

I was not able to implement roads (key feature creating challenge as described below), and more proximity based rather than global calculations given the scope of this prototype.

Room for expansion:

This system can also be used to model more complex interactions as well. I’ve concepted important additional grid types: government (residence will be happy if exactly 1 is within X range, too many or few is bad), education (changes industry within range to tech which is more culture-friendly, costs wealth, can create social strife if educated or uneducated areas are disproportionately close to government), roads (meant to be a main driver of balancing preserving nature as they’d be needed to connect industry to commerce and other related tiles), trade/transport hub (spreads range of distribution of tile benefits and profitability of products).
