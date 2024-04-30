[Back to index](Soundgarden_Documentation.md)

# Sound: Wind


The wind system affects the movement and sound of all foliage in the world, including trees and grass patches. Further, a wind sound generator observes the [wind intensity](Sound_Global_parameters.md) and adjusts the wind ambience accordingly. The lower the wind intensity, the more upper frequencies are filtered out from a generated noise sound. At high wind intensities, more upper frequencies are present, with an additional distorted wind sound being faded in, giving the wind sound a whistling effect. 

Wind strength is dynamically modified by a Perlin noise generator.
Other variables, such as the speed of changes in wind intensity or wind direction are currently set to fixed values but can also be dynamically adjusted for more intricate weather simulation in the future.

![](attachments/Pasted%20image%2020240430161418.png)