using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
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
    }
    public void UpdateOutput() {

        if(parent==null)
            output.position=x;

        if (children.Count > 0) {
            // 单个子节点的情况（如链式结构）
            SlurmPoint child = children[0];
            Transform childOutput = child.output;
            
            if (childOutput != null) {
                // 计算子节点当前的世界位置（基于当前父旋转）
                Vector3 currentChildWorldPos = childOutput.position;
                // 子节点期望的世界位置
                Vector3 desiredChildWorldPos = child.X;
                
                // 如果子节点世界位置与期望位置不同，调整父节点旋转
                if (currentChildWorldPos != desiredChildWorldPos) {
                    // 计算从当前子位置到期望位置的向量
                    Vector3 offset = desiredChildWorldPos - currentChildWorldPos;
                    
                    // 如果子节点不在父节点原点，计算旋转轴和角度
                    if (currentChildWorldPos != x) {
                        Vector3 currentDir = (currentChildWorldPos - x).normalized;
                        Vector3 desiredDir = (desiredChildWorldPos - x).normalized;
                        
                        // 计算旋转
                        Quaternion rot = Quaternion.FromToRotation(currentDir, desiredDir);
                        rot*=output.rotation;
                        rot=Quaternion.Lerp(rot, target.rotation, Slurm.inst.errorLerpFactor);
                        output.rotation = rot;
                    }
                }
            }
        }
    }
}
