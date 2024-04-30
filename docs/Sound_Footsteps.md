[Back to index](Soundgarden_Documentation.md)

# Sound: Footsteps


The footsteps system is responsible for tracking the ground underneath the player during movement and selecting the appropriate footstep sound. The system uses one audio source per foot for more realistic spatialization. Each step sound comes either slightly from the bottom left or right of the player. In situations where the player might be walking on the edge between multiple surfaces - for example, one foot might step onto sand, and another foot would step onto water - having two footstep audio sources, checking ground underneath respectively, ensures correct playback of sounds for both surfaces. Ground checking and sound triggering is handled by logic in the player character blueprint, which sets the parameters of an attached MetaSound that is responsible for sound randomization and playback.
How often footsteps can be heard is defined by the player movement speed: faster movement means more step sounds.

![](attachments/Pasted%20image%2020240430165759.png)
The footstep logic blueprint.

![](attachments/Pasted%20image%2020240430165838.png)
The footstep MetaSound patch.