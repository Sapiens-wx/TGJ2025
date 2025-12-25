using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class SlurmPoint
{
    private SlurmPoint parent;
    public List<SlurmPoint> children;
    public Transform target;
    public Transform output;
    int depth;

    float length;
    Vector3 prevX;
    Vector3 x;
    Vector3 acc;

    public Vector3 PrevX{get=>prevX;}
    public Vector3 X{get=>x;}

    public SlurmPoint(SlurmPoint parent, Transform target, Transform output, int depth) {
        this.parent=parent;
        this.target=target;
        this.output=output;
        this.depth=depth;
        prevX=target.position;
        x=prevX;
        acc=Vector3.zero;
        if(parent==null)
            length=0;
        else
            length=Vector3.Distance(parent.target.position, target.position);
        children=new List<SlurmPoint>();
    }
    public void Update(float deltaTimeSqrd) {
        // verlet integration
        Vector3 oldX=x;
        x+=(x-prevX)*Slurm.inst.dampingFactor+acc*deltaTimeSqrd;
        prevX=oldX;
        // add force
        acc=target.position-x;
        acc*=Slurm.inst.accFactor*Slurm.inst.GetAccFactorByDepth(depth);
        // constraints
        if (parent != null) {
            Vector3 dir=(x-parent.x).normalized;
            x=parent.X+dir*length;
        }
        output.position=x;
        return;
        if (parent != null) {
            Vector3 dir=(x-parent.output.position).normalized;
            output.position=parent.output.position+dir*length;
        } else {
            output.position=x;
        }
    }
}
