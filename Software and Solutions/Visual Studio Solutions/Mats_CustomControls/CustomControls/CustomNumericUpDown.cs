using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomControls
{
    public partial class CustomNumericUpDown : NumericUpDown
    {
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta > 0 && (this.Value+this.Increment < this.Maximum))
            {
                this.Value += this.Increment;
            }
            else if (e.Delta <0 && (this.Value-this.Increment > this.Minimum))
            {
                this.Value -=this.Increment;
            }
        }
    }
}
