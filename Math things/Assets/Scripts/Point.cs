using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomVector;

namespace CustomPoint
{
    public class Point
    {
        // Start is called before the first frame update
        public float x;
        public float y;

        public Point AddVector(Vector v)
        {
            this.x = x + v.x;
            this.y = y + v.y;

            return this;
        }
    }

}

