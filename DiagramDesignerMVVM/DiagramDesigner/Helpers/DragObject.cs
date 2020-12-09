﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DiagramDesigner
{
    // Wraps info of the dragged object into a class
     class DragObject
    {
        public Size? DesiredSize { get; set; }
        public Type ContentType { get; set; }

    }
}
