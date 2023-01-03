using System.Numerics;

namespace Math
{
    class Math
    {
        static public Vector2 DoRotation(Vector2 vector, Matrix3x2 rotationMatrix)
        {
            Vector2 newVec = new Vector2();
            newVec.X = rotationMatrix.M11 * vector.X + rotationMatrix.M12 * vector.Y;
            newVec.Y = rotationMatrix.M21 * vector.X + rotationMatrix.M22 * vector.Y;
            return newVec;
        }

        static public float distanceBetweenTwoPoints(Vector2 A, Vector2 B)
        {
            Vector2 AB = (B - A);
            float distance = AB.Length();
            return distance;
        }
    }
}