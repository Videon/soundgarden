[Back to index](Soundgarden%20-%20Documentation.md)


Wind is dynamically controlled by a Perlin noise generator.
The speed of how fast wind intensity changes is currently set to a fixed value but can also be dynamically adjusted for more intricate weather simulation in the future.
As of now, only wind intensity is modified. However, wind direction already exists in the global parameter collection and may also be dynamically controlled in the future.

The wind system affects all foliage in the world, including trees and grass patches. Further, a wind sound generator observes the wind intensity and adjusts the wind ambience accordingly.