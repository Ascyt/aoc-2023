This problem requires following a series of navigation instructions to determine the final location of the ship. Here's a possible approach to solve it:

1. Initialize variables to keep track of the ship's position and direction. Set the starting position to (0, 0) and the direction to "east".

2. Read the navigation instructions line by line.

3. For each instruction, extract the action and the value.

4. Implement a set of conditional statements to handle each action:

   - For "N" (move north), add the value to the ship's current north position.
   - For "S" (move south), subtract the value from the ship's current north position.
   - For "E" (move east), add the value to the ship's current east position.
   - For "W" (move west), subtract the value from the ship's current east position.
   - For "L" (turn left), update the direction by rotating it counter-clockwise by the given number of degrees.
   - For "R" (turn right), update the direction by rotating it clockwise by the given number of degrees.
   - For "F" (move forward), update the position based on the ship's current direction and the given value:
     - If the ship is facing "north", add the value to the north position.
     - If the ship is facing "south", subtract the value from the north position.
     - If the ship is facing "east", add the value to the east position.
     - If the ship is facing "west", subtract the value from the east position.

5. After processing all the instructions, calculate the Manhattan distance by taking the absolute values of the east and north positions and summing them.

6. Output the Manhattan distance as the result.

This approach should give you the Manhattan distance between the final location and the ship's starting position.