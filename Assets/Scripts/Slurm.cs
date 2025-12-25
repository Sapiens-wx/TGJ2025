using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slurm : Singleton<Slurm>
{
    public Transform target;
    public Transform output;
    [Header("Config")]
    public float accFactor;
    public float dampingFactor;

    SlurmPoint root;
    List<SlurmPoint> slurmBreadthTraversal; //breadth traversal of the tree starting from [root]
    void Start()
    {
        ConstructTree();
        ConstructBreadthTraversal();
        ResetOutputPosition();
        foreach(SlurmPoint p in slurmBreadthTraversal) {
            //p.output.SetParent(slurmBreadthTraversal[0].output.parent);
        }
    }

    void FixedUpdate()
    {
        UpdateSlurm();
    }
    void ConstructTree() {
        List<Transform> processedObjects=new List<Transform>();
        slurmBreadthTraversal=new List<SlurmPoint>();

        root=new SlurmPoint(null, target, output);
        ConstructTreeRecursive(root, target, output);
    }
    void ConstructBreadthTraversal() {
        slurmBreadthTraversal=new List<SlurmPoint>();
        Queue<SlurmPoint> q=new Queue<SlurmPoint>();
        q.Enqueue(root);
        while (q.Count > 0) {
            for(int i = 0, n=q.Count; i < n; ++i) {
                SlurmPoint cur=q.Dequeue();
                slurmBreadthTraversal.Add(cur);
                foreach(SlurmPoint child in cur.children)
                    q.Enqueue(child);
            }
        }
    }
    void ResetOutputPosition() {
        foreach(SlurmPoint p in slurmBreadthTraversal) {
            p.output.position=p.target.position;
        }
    }
    void ConstructTreeRecursive(SlurmPoint parentPoint, Transform targetTransform, Transform outputTransform) {
        for(int i = 0; i < targetTransform.childCount; ++i) {
            Transform targetTransformChild=targetTransform.GetChild(i);
            Transform outputTransformChild=outputTransform.GetChild(i);
            SlurmPoint point=new SlurmPoint(parentPoint, targetTransformChild, outputTransformChild);
            parentPoint.children.Add(point);
            ConstructTreeRecursive(point, targetTransformChild, outputTransformChild);
        }
    }
    void UpdateSlurm() {
        float deltaTimeSqrd=Time.fixedDeltaTime*Time.fixedDeltaTime;
        foreach(SlurmPoint p in slurmBreadthTraversal) {
            p.Update(deltaTimeSqrd);
        }
    }
}
