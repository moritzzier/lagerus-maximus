using System;
using System.Collections.Generic;
using System.Text;

namespace lagerus_maximus
{
    interface ICloseWindow
    {
        Action Close { get; set; }
        bool CanClose();
    }
}
