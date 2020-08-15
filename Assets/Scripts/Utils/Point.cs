using System;

public class Point
{
    public double x, y;
    public Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public static Point operator +(Point p1, Point p2)
    {
        return new Point(p1.x+p2.x, p1.y+p2.y);
    }
    public static Point operator *(Point p1, double n)
    {
        return new Point(p1.x*n, p1.y*n);
    }
    public static Point operator /(Point p1, double n)
    {
        return new Point(p1.x/n, p1.y/n);
    }

    public Point Floor()
    {
        return new Point(Math.Floor(this.x), Math.Floor(this.y));
    }
}