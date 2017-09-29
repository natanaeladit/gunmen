# gunmen
Gunmen problem

There is a map of 8x8. 0 is an empty space and 1 is a wall.

```
{0,1,0,1,0,1,0,1 },
{0,0,0,0,0,1,0,0 },
{1,0,1,0,0,1,0,1 },
{0,0,0,0,0,0,0,0 },
{0,0,0,1,0,0,0,0 },
{0,0,0,0,0,1,0,1 },
{0,1,0,0,0,0,0,0 },
{0,0,0,0,1,0,1,0 }
```

A gunman can shoot vertically and horizontally. They cannot see over the wall and diagonal.
We want to place as much as gunmen in this room.

One example of solutions is:

```
2  1  2  1  2  1  2  1
0  0  0  2  0  1  0  2
1  2  1  0  0  1  0  1
0  0  0  0  0  0  0  2
0  0  2  1  0  2  0  0
0  0  0  2  0  1  0  1
2  1  0  0  0  0  0  2
0  2  0  0  1  2  1  0
```