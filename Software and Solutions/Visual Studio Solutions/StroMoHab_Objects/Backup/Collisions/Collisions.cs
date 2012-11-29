using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StroMoHab_Objects.Graphics
{
    /// <summary>
    /// Contains collision detection methods
    /// </summary>
    public static class Collisions
    {
        /// <summary>
        /// Returns bool indicating if two bounding boxes have collided
        /// </summary>
        /// <param name="rect1"></param>
        /// <param name="rect2"></param>
        /// <param name="currentX"></param>
        /// <param name="currentY"></param>
        /// <param name="currentZ"></param>
        /// <returns></returns>
        static public bool HaveCollided(BoundingBox rect1, BoundingBox rect2, float currentX, float currentY, float currentZ)
        {
            bool collisionHasOccured = false;

            //for each cube2 vertex, check if vertex is inside cube 1
            
            //Front Bottom Left
            collisionHasOccured = IsInside(rect2.XMin + currentX, rect2.YMin + currentY, rect2.ZMin + currentZ, rect1);

            //Use IF to prevent further unnecessary searching
            //Back Bottom Left
            if (collisionHasOccured == false)
            {
                collisionHasOccured = IsInside(rect2.XMin + currentX, rect2.YMin + currentY, rect2.ZMax + currentZ, rect1);
            }
            //Back Top Left
            if (collisionHasOccured == false)
            {
                collisionHasOccured = IsInside(rect2.XMin + currentX, rect2.YMax + currentY, rect2.ZMax + currentZ, rect1);
            }
            //Front Top Left
            if (collisionHasOccured == false)
            {
                collisionHasOccured = IsInside(rect2.XMin + currentX, rect2.YMax + currentY, rect2.ZMin + currentZ, rect1);
            }
            //Front Bottom Right
            if (collisionHasOccured == false)
            {
                collisionHasOccured = IsInside(rect2.XMax + currentX, rect2.YMin + currentY, rect2.ZMin + currentZ, rect1);
            }
            //Back Bottom Right
            if (collisionHasOccured == false)
            {
                collisionHasOccured = IsInside(rect2.XMax + currentX, rect2.YMin + currentY, rect2.ZMax + currentZ, rect1);
            }
            //Back Top Right
            if (collisionHasOccured == false)
            {
                collisionHasOccured = IsInside(rect2.XMax + currentX, rect2.YMax + currentY, rect2.ZMax + currentZ, rect1);
            }
            //Front Top Right
            if (collisionHasOccured == false)
            {
                collisionHasOccured = IsInside(rect2.XMax + currentX, rect2.YMax + currentY, rect2.ZMin + currentZ, rect1);
            }

            return collisionHasOccured;
        }

        static private bool IsInside(float pointX, float pointY, float pointZ, BoundingBox rect1)
        {
            if (pointX >= rect1.XMin && pointX <= rect1.XMax)
            {
                if (pointY >= rect1.YMin && pointY <= rect1.YMax)
                {
                    if (pointZ >= rect1.ZMax && pointZ <= rect1.ZMin)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
