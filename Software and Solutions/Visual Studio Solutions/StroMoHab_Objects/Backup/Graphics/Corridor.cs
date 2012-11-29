using System;
using System.Collections.Generic;

namespace StroMoHab_Objects.Graphics
{
    /// <summary>
    /// A corridor.
    /// </summary>
    [Serializable]
    public class Corridor : OpenGlObject
    {
        Quad m_leftWall, m_rightWall, m_floor, m_ceiling;

        /// <summary>
        /// Constructor for corridor. TODO: Add user-selectable textures.
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="yMin"></param>
        /// <param name="zMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMax"></param>
        /// <param name="zMax"></param>
        public Corridor(float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
            : base(xMin, yMin, zMin, xMax, yMax, zMax)
        {
            XMin = xMin;
            YMin = yMin;
            ZMin = zMin;
            XMax = xMax;
            YMax = yMax;
            ZMax = zMax;

            m_leftWall = new Quad(8, 100, 3,
                                            new Vector3f(XMin, YMin, ZMin),
                                            new Vector3f(XMin, YMin, ZMax),
                                            new Vector3f(XMin, YMax, ZMax),
                                            new Vector3f(XMin, YMax, ZMin)
                                         );

            m_rightWall = new Quad(8, 100, 3,
                                                        new Vector3f(XMax, YMin, ZMax),
                                                        new Vector3f(XMax, YMin, ZMin),
                                                        new Vector3f(XMax, YMax, ZMin),
                                                        new Vector3f(XMax, YMax, ZMax)
                                                      );

            m_floor = new Quad(5, 500, 5,
                                                    new Vector3f(XMin, YMin, ZMax),
                                                    new Vector3f(XMin, YMin, ZMin),
                                                    new Vector3f(XMax, YMin, ZMin),
                                                    new Vector3f(XMax, YMin, ZMax)
                                                  );

            m_ceiling = new Quad(1, 500, 5,
                                                        new Vector3f(XMin, YMax, ZMin),
                                                        new Vector3f(XMin, YMax, ZMax),
                                                        new Vector3f(XMax, YMax, ZMax),
                                                        new Vector3f(XMax, YMax, ZMin)
                                                    );
            m_ceiling.Colour = new Colour(0.8f, 0.8f, 0.8f, 0.0f);



            //CollisionModel = new List<BoundingBox>();

            //Enlarge collision models to 3D
            m_leftWall.CollisionModel.XMin -= 2.0f;
            m_rightWall.CollisionModel.XMax += 2.0f;
            m_floor.CollisionModel.YMin -= 2.0f;
            m_ceiling.CollisionModel.YMax += 2.0f;

            //CollisionModel.Add(m_leftWall.CollisionModel);
            //CollisionModel.Add(m_rightWall.CollisionModel);
            //CollisionModel.Add(m_floor.CollisionModel);
            //CollisionModel.Add(m_ceiling.CollisionModel);

        }

        /// <summary>
        /// Constructor taking a width, height and depth.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="depth"></param>
        public Corridor(float width, float height, float depth)
            : base(-width / 2, 0.0f, 4.0f, width / 2, height, -depth + 4.0f)
        {
            XMin = -width/2;
            YMin = 0.0f;
            ZMin = 4.0f;
            XMax = width/2;
            YMax = height;
            ZMax = -depth + 4.0f;

            //texture no: 5, length 16, height 1
            m_leftWall = new Quad(7, 100, 3,
                                            new Vector3f(XMin, YMin, ZMin),
                                            new Vector3f(XMin, YMin, ZMax),
                                            new Vector3f(XMin, YMax, ZMax),
                                            new Vector3f(XMin, YMax, ZMin)
                                         );

            //texture no: 0, length 16, height 1
            m_rightWall = new Quad(7, 100, 3,
                                                        new Vector3f(XMax, YMin, ZMax),
                                                        new Vector3f(XMax, YMin, ZMin),
                                                        new Vector3f(XMax, YMax, ZMin),
                                                        new Vector3f(XMax, YMax, ZMax)
                                                      );

            m_floor = new Quad(4, 500, 5,
                                                    new Vector3f(XMin, YMin, ZMax),
                                                    new Vector3f(XMin, YMin, ZMin),
                                                    new Vector3f(XMax, YMin, ZMin),
                                                    new Vector3f(XMax, YMin, ZMax)
                                                  );

            m_ceiling = new Quad(0, 500, 5,
                                                        new Vector3f(XMin, YMax, ZMin),
                                                        new Vector3f(XMin, YMax, ZMax),
                                                        new Vector3f(XMax, YMax, ZMax),
                                                        new Vector3f(XMax, YMax, ZMin)
                                                    );
            m_ceiling.Colour = new Colour(0.8f, 0.8f, 0.8f, 0.0f);



            //CollisionModel = new List<BoundingBox>();

            //Enlarge collision models to 3D
            m_leftWall.CollisionModel.XMin -= 2.0f;
            m_rightWall.CollisionModel.XMax += 2.0f;
            m_floor.CollisionModel.YMin -= 2.0f;
            m_ceiling.CollisionModel.YMax += 2.0f;

        }


        /// <summary>
        /// Draws the corridor.
        /// </summary>
        public override void Draw()
        {
            m_leftWall.Draw();
            m_rightWall.Draw();
            m_floor.Draw();
            m_ceiling.Draw();

        }

        /// <summary>
        /// Moves the corridor. Currently does nothing.
        /// </summary>
        public override void Move()
        {

        }
    }
}