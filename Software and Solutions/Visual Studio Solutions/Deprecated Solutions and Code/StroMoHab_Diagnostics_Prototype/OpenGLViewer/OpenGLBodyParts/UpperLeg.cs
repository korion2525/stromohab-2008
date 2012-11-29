using Tao.OpenGl;

namespace Skeleton
{

    class UpperLeg : BodyPart
    {

        private float zLength = 0f;
        private float yLength = 300f;
        private float xLength = 30f;

        protected override void DrawBodyPart()
        {
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (-(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (-(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (-(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (+(xLength)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(xLength)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (-(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (+(xLength)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(xLength)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(zLength / 2)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (+(xLength)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (+(yLength / 2)) / SCALLINGFACTOR, (+(xLength)) / SCALLINGFACTOR);
            Gl.glVertex3f((-(xLength / 2)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(xLength)) / SCALLINGFACTOR);
            Gl.glVertex3f((+(xLength / 2)) / SCALLINGFACTOR, (-(yLength / 2)) / SCALLINGFACTOR, (+(xLength)) / SCALLINGFACTOR);
            Gl.glEnd();


        }


       
    }
}
