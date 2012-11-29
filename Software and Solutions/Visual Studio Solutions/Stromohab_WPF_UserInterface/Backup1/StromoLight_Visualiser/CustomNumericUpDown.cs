using System;

using System.Windows.Forms;

namespace StromoLight_Visualiser
{
    class CustomNumericUpDown : NumericUpDown
    {

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                this.Value += this.Increment;
            }
            else if (e.Delta < 0 && this.Value > this.Minimum)
            {
                this.Value -= this.Increment;
            }
        }

    }
}
