# EscapeRoomVR
**Game Story**
You are a treasure hunter who has stumbled upon an ancient ruin. You enter a dimly lit room and find a door in the center, with three locks. Each lock presents a puzzle that you must solve to escape the room and find the lost treasure. You have only three minutes to complete the puzzles or you will be trapped forever.

Puzzle 1: Numerical Pad Lock The first lock is a numerical pad with numbers 0-9. You must solve a tricky math equation to find the code to unlock the door.

Puzzle 2: Piece Matching Box Lock The second lock is a box with slots for matching pieces that you must collect throughout the room. The pieces fit together to form a shape or image that will unlock the door.

Puzzle 3: Treasure Insertion Lock The third lock is a slot for the treasure item that you must find in order to unlock the door and escape the room. The treasure item is hidden somewhere in the room, and finding it will require careful searching and observation.

As you explore the room, you will encounter props and objects that hint at the lost treasure and the history of the ancient ruin. The environment is designed to be immersive, with cobwebs, cracks in the walls, and a musty smell to give you a sense of being in a forgotten place.

Can you solve the puzzles and find the lost treasure before time runs out? Test your skills and see if you can beat the high score.

**Optimization**
1. To optimize the performance and graphics quality of the game, several steps were taken. The first step was to utilize Unity's Universal Render Pipeline (URP) and the Samples Pipeline Asset. The URP provides a prebuilt solution for high-quality graphics and performance, and the Samples Pipeline Asset provides a set of customizable options for controlling the pipeline's behavior.
     ![img.png](img.png)
 
2. In the Samples Pipeline Asset's quality section, the HDR option was unchecked to reduce the computational load and improve performance. The anti-aliasing option was set to 4x to improve the image quality and reduce visual artifacts.
     ![img_1.png](img_1.png)
 
3. In the player's other settings, the graphics APIs were set to Vulkan and the auto graphics API option was unchecked. By doing this, the Vulkan API was selected specifically to maximize performance. The texture compression format was set to ASTC to reduce the texture size and improve performance on the target platform.
     ![img_2.png](img_2.png)

Player Implementation
For player VR controllers in your project, you utilized the XR Origin component which provides a ready camera and left/right controller. Additionally, you added a locomotion system and teleportation provider to enable movement within the VR environment. Finally, you added Oculus Hands to provide a sense of interaction and control within the VR space. These additions to the VR setup provide a more immersive experience for the player.

Here is the detailed explanation:

XR Origin Component:
The XR Origin component is a useful tool provided in Unity's XR Plugin architecture. It acts as the root transform for all XR components in a scene and provides a convenient starting point for adding VR functionalities to your game or app.

Locomotion System:
The locomotion system provides a means for the player to move around within the VR environment. This can include teleportation, joysticks, or other methods of movement. It's essential for providing the player with a sense of mobility and exploration.

Teleportation Provider:
The teleportation provider allows the player to teleport from one location to another within the VR environment. This is achieved by using VR controllers to point and select a location to teleport to.
     ![img_4.png](img_4.png)

Oculus Hands:
Oculus Hands is a component provided by the Oculus XR Plugin. It enables the player to interact with the VR environment by allowing the player to see their hands and manipulate objects within the environment. It provides a sense of immersion and control within the VR space.
     ![img_5.png](img_5.png)


Environment
For the lighting, in the lobby environment, we have added dimly lit effects to set the mood for the scene. We used a combination of ambient lighting and spotlights to achieve the desired effect. The ambient lighting provides a soft fill light to the entire scene, while the spotlights add highlights to certain objects and areas, creating depth and interest.

To facilitate teleportation, we added teleportation anchor objects throughout the lobby environment. These anchor objects allow the player to teleport to specific locations within the room. We have placed these objects in strategic positions, making it easy for the player to navigate the space and get to where they need to go.
![img_6.png](img_6.png)
![img_8.png](img_8.png)
![img_7.png](img_7.png)
![img_9.png](img_9.png)
![img_10.png](img_10.png)
![img_11.png](img_11.png)

**Fire Torch**

we have added fire torch in the lobby scene when you look gaze it first time it start fire.
![img_12.png](img_12.png)
![img_13.png](img_13.png)

for UI we have menu and high score.
![img_14.png](img_14.png)
![img_15.png](img_15.png)

we use PlayerPref to save the data.
![img_16.png](img_16.png)

---------------------------------------------------------------------------------------------------------
**Part 2**
Lobby Environment Updates.
improve the Quality of Teleportation Anchor with Animation on Hover.
![img_17.png](img_17.png)

we created a menu where player can enter his name and have 3 options start, highscore and quit. which do Jobs as they are named.
![img_18.png](img_18.png)

We created 2nd Scene Name as Escape Room. we created the following environment shows in pictures.
![img_19.png](img_19.png)
![img_20.png](img_20.png)
![img_21.png](img_21.png)
![img_22.png](img_22.png)
![img_23.png](img_23.png)
![img_24.png](img_24.png)
![img_25.png](img_25.png)
![img_26.png](img_26.png)

we created 3 puzzles in the Door.


Puzzle 1 Missing Peace from the Door.
![img_27.png](img_27.png)

Puzzle 2 Finding 3 Coins 
![img_28.png](img_28.png)
![img_29.png](img_29.png)


Numeric Wheel Lock. Puzzle 3
![img_30.png](img_30.png)

solving puzzle 1 and 2 are very easy you just find the peaces and place them in the socket. of the door.

now to solve the wheel lock you need to get the code from this clock
![img_31.png](img_31.png)

you will get the key near the clock and you have to grab it and put it into the clock socket. then it will spin and you can get the a b and c values by looking at the arrows direction.
then applying this code (a+b)*c get the code to add into door.
![img_32.png](img_32.png)


**What we learned and used in making these scene.**
we learned working with Sockets. unity animations.

User Interface of the part 2

Introduction. player is guided to what todo in the game.
![img_33.png](img_33.png)

Game Over Menu
![img_34.png](img_34.png)

in game over section we save the player score. into unity PlayerPrefs LocalData. and we hint the user how to get the game.
we calculate the score by diving the 999 by time spend by the user.
`var score = Mathf.RoundToInt(999 / _timeSpend);
`