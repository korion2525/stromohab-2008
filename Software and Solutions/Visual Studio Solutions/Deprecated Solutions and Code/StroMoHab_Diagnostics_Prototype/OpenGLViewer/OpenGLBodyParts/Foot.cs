using Tao.OpenGl;

namespace Skeleton 
{
    class Foot : BodyPart
    {
        private float zLength = 200f;
        private float yLength = 0.0f;
        private float yDepth = 70f;
        private float xLength = 70f;

        
        protected override void DrawBodyPart()
        {
            
            
            // Begin drawing the Polygon based on x/y/z lengths and the ScallingFactor
            Gl.glBegin(Gl.GL_POLYGON);

            //Top
            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (-(zLength / 2) + zLength / 3.2f) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 4)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (-(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 4)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (-(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (-(zLength / 2) + zLength / 3.2f) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(zLength / 3f)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 3f)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 3f)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(zLength / 3f)) / SCALLINGFACTOR);
            Gl.glEnd();
            Gl.glBegin(Gl.GL_POLYGON);
            //Bottom
            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (-(zLength / 2) + zLength / 3.2f) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 4)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (-(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 4)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (-(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (-(zLength / 2) + zLength / 3.2f) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (+(zLength / 3f)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 3f)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (+(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 3f)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (+(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (+(zLength / 3f)) / SCALLINGFACTOR);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            //Sides -- Need to change to QUAD_STRIP !
            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(zLength / 3f)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (-(zLength / 2) + zLength / 3.2f) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (-(zLength / 2) + zLength / 3.2f) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (+(zLength / 3f)) / SCALLINGFACTOR);
            


            Gl.glVertex3f((+(xLength / 3f)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(zLength / 3f)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (+(zLength / 3f)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 3f)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (+(zLength / 2)) / SCALLINGFACTOR);
            

            Gl.glVertex3f((-(xLength / 3f)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 3f)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 3f)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (+(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 3f)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (+(zLength / 2)) / SCALLINGFACTOR);
            


            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(zLength / 3f)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 3f)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 3f)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (+(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (+(zLength / 3f)) / SCALLINGFACTOR);
            

            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (-(zLength / 2) + zLength / 3.2f) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(zLength / 3f)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (+(zLength / 3f)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (-(zLength / 2) + zLength / 3.2f) / SCALLINGFACTOR);
            

            Gl.glVertex3f((-(xLength / 4)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (-(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (-(zLength / 2) + zLength / 3.2f) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (-(zLength / 2) + zLength / 3.2f) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 4)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (-(zLength / 2)) / SCALLINGFACTOR);
            


            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (-(zLength / 2) + zLength / 3.2f) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 4)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (-(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 4)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (-(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (-(zLength / 2) + zLength / 3.2f) / SCALLINGFACTOR);


            Gl.glVertex3f((+(xLength / 4)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (-(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 4)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (-(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 4)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (-(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 4)) / SCALLINGFACTOR, (-(yDepth / 2)) / SCALLINGFACTOR, (-(zLength / 2)) / SCALLINGFACTOR);
           
            Gl.glEnd();


            
 
        }
    }
}
