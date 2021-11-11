using System;

namespace SharpEngine
{
    public struct Vector
    {
        public float x, y, z;
 
        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0;
        }
 
        public static Vector operator *(Vector v, float f)
        {
            return new Vector(v.x * f, v.y * f, v.z * f);
        }
             
             
        public static Vector operator +(Vector lhs, Vector rhs) {
            return new Vector(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }
        public static Vector operator -(Vector lhs, Vector rhs) {
            return new Vector(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }
 
            
 
        public static Vector operator /(Vector v, float f)
        {
            return new Vector(v.x / f, v.y / f, v.z / f);
        }
 
        public static Vector Max(Vector a, Vector b)
        {
            //return new Vector (Max(a.x, b.x),Max(a.x, b.x));
            float maxX = MathF.Max(a.x, b.x);
            float maxY =  MathF.Max(a.y, b.y);
            float maxZ =  MathF.Max(a.z, b.z);
            return new Vector(maxX, maxY, maxZ);
        }
        public static Vector Min(Vector a, Vector b)
        {
            //return new Vector (Max(a.x, b.x),Max(a.x, b.x));
            float minX = MathF.Min(a.x, b.x);
            float minY =  MathF.Min(a.y, b.y);
            float minZ =  MathF.Min(a.z, b.z);
            return new Vector(minX, minY, minZ);
        }
             
    }
}