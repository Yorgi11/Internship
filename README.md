# NEX Level Gaming Technical Interview Challenge

The game consists of a simple physics based first person controller. The player has a "gun" that they can use to attack the boss enemy.
The boss enemy also has a gun as well as a melee attack. When the player is within range of the boss it will fire at the player. If the player gets too close to the boss the boss will begin chasing the player and attempt a melee attack
The game level consists of a ramp and some minor 3D platforming, as well as a sweeping platform obsticle.

Instructions
Movement: W A S D
Jumping: space
Sprint: Left Shift
Camera look: Mouse movement
Shooting: Mouse0/Left click

Utilizing ground normal based movement to apply the movement force perpendicular to the current ground normal.
This ensures that even when going up slopes the desired speed and acceleration aren't noticably different from flat ground
