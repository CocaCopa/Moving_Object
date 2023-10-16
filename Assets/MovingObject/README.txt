How to Setup:
1. Drag the 'MovingObject' prefab in the scene.

2. Add a (ex:)platform as a child to the scene object.

3. Add empty GameObjects as children, under the Checkpoints holder, to create more checkpoints for the
   object to move towards. (A convinient way to do so is by selecting 'Checkpoint_2' and then Ctrl+D)

4. To save your new 'MovingObject' create an original prefab.


Notes:
1. If you press the Editor's Pause button and then begin Play Mode, the tool will bug out and throw an error.
   Will update the tool once I find what's causing this error. To not encounter this issue, begin play mode
   normaly. No issues when building the project.

2. Do not remove the 'MovingObjectGizmos' script from the GameObject, as it contains the code responsible
   for visualizing the path of the object, based on the given checkpoints.

3. If you want the tool to assign any newly created checkpoint to the checkpoints list automatically,
   assign the 'Checkpoints' GameObject to the 'CheckPointsHolderTransform' reference.