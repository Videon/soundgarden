[Back to index](Soundgarden_Documentation.md)


The footsteps system is responsible for tracking the ground underneath the player during movement and selecting the appropriate footstep sound. The system uses one audio source per foot for more realistic spatialization. Each step sound comes either slightly from the bottom left or right of the player. In situations where the player might be walking on the edge between multiple surfaces - for example, one foot might step onto sand, and another foot would step onto water - this ensures correct playback of sounds for both surfaces.
How often footsteps can be heard is defined by the player movement speed: faster movement means more step sounds.