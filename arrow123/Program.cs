using arrow123;
using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;

Console.WriteLine("how many spinning lines do you want? (please enter a number)");
int amount;
amount = int.Parse(Console.ReadLine());
Console.WriteLine("what are the size ratios?");
List<double> Lratios = new List<double>();
double current;
for (int i = 0; i < amount; i++)
{
    current = 1 / double.Parse(Console.ReadLine());
    Lratios.Add(current);
    Console.Write(current + ": ");
}

Console.WriteLine("what are the spinning speeds ratios?");
List<double> Sratios = new List<double>();
double Scurrent;
for (int i = 0; i < amount; i++)
{
    Scurrent = double.Parse(Console.ReadLine());
    Sratios.Add(Scurrent);
    Console.Write(Scurrent + ": ");
}

Arrow[] arrows = new Arrow[amount];
arrows[0] = new Arrow(200, 200, 200, (int)(200 + 75 * Lratios[0]), Sratios[0] * 0.06 * 60);
for (int i = 0; i < amount; i++)
{
    arrows[i] = new Arrow(200, 200, 200, (int)(200 + 75 * Lratios[i]), Sratios[i] * 0.06 * 60);
}

Console.WriteLine("how much of the trace do you want to see? (0 - 5)");
int tracing = int.Parse(Console.ReadLine());
if (tracing != 0 && tracing != 5)
{
    tracing = (int)(Math.Pow(tracing, 2) * 5);
}
else
    tracing = tracing * 1000;


    //starting

    Raylib.InitWindow(400, 400, "arrows");
Raylib.SetTargetFPS(60);

var trail = new List<Vector2>();



double theta = 90;
double theta2 = 90;

int mouseX;
int mouseY;

double[] thetas = new double[amount];

while (!Raylib.WindowShouldClose() && !Raylib.IsKeyDown(KeyboardKey.Grave))
{

    double dt = Raylib.GetFrameTime();

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);
    foreach (Arrow a in arrows)
        a.DrawArrow();

    Vector2? prev = null;

    foreach (var p in trail)
    {
        if (prev.HasValue)Raylib.DrawLine((int)prev.Value.X, (int)prev.Value.Y, (int)p.X, (int)p.Y, Color.White);
        prev = p;
    }
        
    Raylib.EndDrawing();
    if (Raylib.IsKeyDown(KeyboardKey.Space))
    {

        for (int i = 0; i < thetas.Length - 1; i++)
        {
            arrows[i].RotateArrow(thetas[i]);
            thetas[i] += arrows[i].GetAngularVelocity() * dt;
            arrows[i + 1].SetStartX(arrows[i].GetEndX());
            arrows[i + 1].SetStartY(arrows[i].GetEndY());
        }

        arrows[^1].RotateArrow(thetas[^1]);
        thetas[^1] += arrows[^1].GetAngularVelocity() * dt;

        trail.Add(new Vector2(arrows[^1].GetEndX(), arrows[^1].GetEndY()));
        if (trail.Count > tracing)
            trail.RemoveAt(0);
    }

    if (Raylib.IsKeyPressed(KeyboardKey.R) && Raylib.GetMouseX() != arrows[0].GetStartX() && Raylib.GetMouseY() != arrows[0].GetStartY())
    {
        mouseX = Raylib.GetMouseX();
        mouseY = Raylib.GetMouseY();
        double angle = Math.Atan2(mouseY - arrows[0].GetStartY(), mouseX - arrows[0].GetStartX());
        arrows[0].RotateArrow(angle);

        theta = angle;
    }

    
}
Raylib.CloseWindow();