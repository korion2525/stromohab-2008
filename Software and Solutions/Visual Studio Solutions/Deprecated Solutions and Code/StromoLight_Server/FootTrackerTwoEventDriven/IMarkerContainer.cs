using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stromohab_MCE
{
    interface IMarkerContainer
    {
        double xCoordinate
        {
            get;
            set;
        }

        double yCoordinate
        {
            get;
            set;
        }

        double zCoordinate
        {
            get;
            set;
        }

        double prevXCoordinate
        {
            get;
        }

        double prevYCoordinate
        {
            get;
        }

        double prevZCoordinate
        {
            get;
        }

    }
}
