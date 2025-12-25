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

    float length;
    Vector3 prevX;
    Vector3 acc;

    public SlurmPoint(SlurmPoint parent, Transform target, Transform output) {
        this.parent=parent;
        this.target=target;
        this.output=output;
        prevX=target.position;
        acc=Vector3.zero;
        if(parent==null)
            length=0;
        else
            length=Vector3.Distance(parent.target.position, target.position);
        children=new List<SlurmPoint>();
    }
    public void Update(float deltaTimeSqrd) {
        // verlet integration
        Vector3 x=output.position;
        output.position+=(x-prevX)*Slurm.inst.dampingFactor+acc*deltaTimeSqrd;
        prevX=x;
        // add force
        acc=target.position-output.position;
        acc*=Slurm.inst.accFactor;
        // constraints
        if (parent != null) {
            Vector3 dir=(output.position-parent.output.position).normalized;
            output.position=parent.output.position+dir*length;
        }
    }
}
