using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomPoint;
using CustomVector;

public class MathFunctions : MonoBehaviour
{
    [SerializeField] Point p2 = new Point();
    [SerializeField] Vector v = new Vector();
    [SerializeField] Point p = new Point();
    [SerializeField] Point i = new Point();
    Vector vc = new Vector();

    void Start()
    {
        //simpleVector();

        subsTractingVector();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void simpleVector()
    {
        p.x = 3;
        p.y = 3;
        print(p.x);

        v.x = 2;
        v.y = 3;
        print(v.x);

        p2 = p.AddVector(v);

        print("X:" + p2.x + "Y:" + p2.y);
    }

    void subsTractingVector ()
    {
        p.x = 0;
        p.y = -1;

        i.x = 1;
        i.y = 1;

        v = substractVector(p, i);

        print("VX:" + v.x + "VY:" + v.y);

    }


    Vector substractVector(Point a, Point b)
    {
        
        vc.x = a.x - b.x;
        vc.y = a.y - b.y;

        return vc;
    }


}
