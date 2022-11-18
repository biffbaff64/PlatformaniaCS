namespace Lugh.Maths;

public abstract class VectorUtils
{
    public static Vector3 Set( float x, float y, float z )
    {
        var tmpVec = new Vector3
        {
                X = x,
                Y = y,
                Z = z
        };

        return tmpVec;
    }

    public static Vector3 Set( Vector3 vector3 )
    {
        var tmpVec = new Vector3
        {
                X = vector3.X,
                Y = vector3.Y,
                Z = vector3.Z
        };

        return tmpVec;
    }

    public static Vector3 Scl( Vector3 vector3, float scalar )
    {
        var tmpVec = new Vector3
        {
                X = vector3.X * scalar,
                Y = vector3.Y * scalar,
                Z = vector3.Z * scalar
        };

        return tmpVec;
    }

    public static Vector3 Scl( Vector3 vector3, float vx, float vy, float vz )
    {
        var tmpVec = new Vector3
        {
                X = vector3.X * vx,
                Y = vector3.Y * vy,
                Z = vector3.Z * vz
        };

        return tmpVec;
    }

    public static Vector3 Scl( Vector3 vector3, Vector3 other )
    {
        var tmpVec = new Vector3
        {
                X = vector3.X * other.X,
                Y = vector3.Y * other.Y,
                Z = vector3.Z * other.Z
        };

        return tmpVec;
    }

    public static Vector3 Mul( Vector3 v, Matrix4 matrix )
    {
        var l_mat = matrix.Val;

        return Set
        (
            v.X * l_mat[Matrix4.M00] + v.Y * l_mat[Matrix4.M01] + v.Z * l_mat[Matrix4.M02] + l_mat[Matrix4.M03],
            v.X * l_mat[Matrix4.M10] + v.Y * l_mat[Matrix4.M11] + v.Z * l_mat[Matrix4.M12] + l_mat[Matrix4.M13],
            v.X * l_mat[Matrix4.M20] + v.Y * l_mat[Matrix4.M21] + v.Z * l_mat[Matrix4.M22] + l_mat[Matrix4.M23]        );
    }
}
