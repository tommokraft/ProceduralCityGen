using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Visualizer;

public class Visualizer1 : MonoBehaviour
{
    public LSystemGen lsystem;
    List<Vector3> pos = new List<Vector3>();
    public Structure structure;
    public Road road;
    public int length = 40;
    public float angle = 90;
    
    private void Start()
    {
        var seq = lsystem.genSentence();
        visualizeSeq(seq);
    }
    public int Length
    {
        get
        {
            if (length > 0)
            {
                return length;
            }
            else
            {
                return 1;
            }
        }
        set => length = value;
    }

    private void visualizeSeq(string seq)
    {
        string axiom = lsystem.axiom;
        bool radial = lsystem.Radial;
        Stack<Parameters> saves = new Stack<Parameters>();
        var currentPos = Vector3.zero;
        Vector3 direction = Vector3.forward;
        Vector3 tempPos = Vector3.zero;
        pos.Add(currentPos);
        
        foreach (var letter in seq)
        {
            
            letters l = (letters)letter;
            switch (l)
            {
                case letters.save:
                    saves.Push(new Parameters
                    {
                        pos = currentPos,
                        direction = direction,
                        length = Length
                    });
                    break;
                case letters.load:
                    if (saves.Count > 0)
                    {
                        var Parameters = saves.Pop();
                        currentPos = Parameters.pos;
                        direction = Parameters.direction;
                        Length = Parameters.length;
                    }
                    break;
                case letters.draw:
                    tempPos = currentPos;
                    currentPos += direction * length;
                    road.Place(tempPos, Vector3Int.RoundToInt(direction), length);
                    if (seq != axiom)
                    {
                        Length -= 1;

                    }
                    
                    pos.Add(currentPos);
                    break;
                case letters.turnRight:
                    if (radial)
                    {
                        angle = 60;
                        Debug.Log("entry");
                    }
                    direction = Quaternion.AngleAxis(angle, Vector3.up) * direction;
                    Debug.Log(direction);
                    break;
                case letters.turnLeft:
                    if (lsystem.Radial && seq == axiom)
                    {
                        angle = 60;
                    }
                    direction = Quaternion.AngleAxis(-angle, Vector3.up) * direction;
                    Debug.Log(direction);
                    break;
                default:
                    break;
            }
        }
        structure.Place(road.getRoadsPos());
    }
}
