Point TOP_LEFT = new Point(96, -98);
Point BOTTOM_RIGHT = new Point(125, -144);

//Point TOP_LEFT = new Point(20, -5);
//Point BOTTOM_RIGHT = new Point(30, -10);

var MAX_HEIGHT = int.MinValue;
var COUNT_VELOCITY = 0;

for (int i = 1; i < BOTTOM_RIGHT.x + 1; i++)
{
    for (int j = BOTTOM_RIGHT.y; j < 2000; j++)
    {
        if(Simulate(i, j))
        {
            COUNT_VELOCITY++;
            MAX_HEIGHT = Math.Max(MAX_HEIGHT, (j*(j+1))/2);
            Console.WriteLine($"Success: {i}, {j}. MAX_HEIGHT is {MAX_HEIGHT}");
        }
    }
}

Console.WriteLine($"Result is {MAX_HEIGHT}, count of velocity: {COUNT_VELOCITY}");

bool Simulate(int vx, int vy)
{
    int vyMax = vy;
    int x = vx, y = vy;
    while (!IsInTarget(x, y) && x <= BOTTOM_RIGHT.x && y >= BOTTOM_RIGHT.y)
    {
        x = x + int.Max(--vx, 0);
        vyMax = int.Max(y, vyMax);
        y = y + --vy;
    }
    return IsInTarget(x, y);
}
bool IsInTarget(int x, int y)
{
    if (x >= TOP_LEFT.x && x <= BOTTOM_RIGHT.x  && y <= TOP_LEFT.y && y >= BOTTOM_RIGHT.y) return true;
    return false;
}

record Point(int x, int y);