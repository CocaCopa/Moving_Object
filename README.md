# Moving_Object
--------------
-HOW TO SETUP-
--------------
1. Drag and drop the 'MovingObject' prefab in the scene.

2. Add a (e.g. platform) as a child of the 'MovingObject' scene GameObject. (It's best not to modify the provided prefab)

3. Add empty GameObjects as children to the Checkpoints holder in order to create additional checkpoints for the object to
   move towards. (A convenient way to do this is by selecting 'Checkpoint_2' and then using 'Ctrl+D' to duplicate it)
   Then, just move the created empty GameObject(s) to the desired position.

4. To save your new 'MovingObject' create an original prefab.


-----------
-SCRIPTING-
-----------
MovingObject class provides the following properties:

1. ObjectTransform  -> The transform of the assigned moving object. (It's best not to alter this reference at runtime)
2. Checkpoints		-> The list that contains all checkpoints.
3. WaitTime			-> The time the object waits before moving to the next checkpoint.
4. ObjectSpeed		-> The speed of the object in meters per second. (m/s)

All of the above properties are both get/set and can be called by your scripts to change the behaviour of the object at runtime.

This package also comes with a 'Utilities' class which is used by the 'MovingObject' class. It contains the following static functions:

1. TickTimer()              -> Start a timer with the specified values. Returns true when the timer value is 0.
2. EvaluateAnimationCurve() -> Interpolate an animation curve based on distance and speed.
3. SwapVectorValues()       -> Swaps the values between two given Vectors.

All of these functions contain a summary to make it easier to understand their purpose if you need to call them.



-------
-NOTES-
-------
1. Do not remove the 'MovingObjectGizmos' script from the provided 'MovingObject' prefab, as it contains the code responsible
   for visualizing the path of the object, based on the given checkpoints.
      
2. Do not clear the 'CheckpointsHolderTransform' reference of the 'MovingObjectGizmos' class. By doing so, you will then need to 
   manually assign all of the created checkpoints to the list called 'Checkpoints' in the Inspector.

3. There are added tooltips to the fields in the Inspector. Hover your mouse over each field to view
   informative tooltips that explain their functionality and purpose.
