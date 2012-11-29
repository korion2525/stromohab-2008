using System;

namespace StroMoHab_Objects.Graphics
{
    interface IOpenGlObject
    {
       // void PrepareToDraw();
        void Draw();
        void RotateX(float increment);
        void RotateY(float increment);
        void RotateZ(float increment);
    }
}
