using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    public LSystemGen lsystem;
    List<Vector3> pos = new List<Vector3>();
    public GameObject path;
    public Material lineMat;

    public int length = 15;
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
                    else
                    {
                        throw new System.Exception("save point not in  stack");
                    }
                    break;
                case letters.draw:
                    tempPos = currentPos;
                    currentPos += direction * length;
                    draw(tempPos, currentPos, Color.black);
                    Length -= 1;
                    pos.Add(currentPos);
                    break;
                case letters.turnRight:
                    if (lsystem.Radial)
                    {
                        angle = 60;
                    }
                    direction = Quaternion.AngleAxis(angle, Vector3.up) * direction;
                    break;
                case letters.turnLeft:
                    if (lsystem.Radial)
                    {
                        angle = 60;
                    }
                    direction = Quaternion.AngleAxis(-angle, Vector3.up) * direction;
                    break;
                default:
                    break;
            }
        }
        foreach (var position in pos)
        {
            Instantiate(path, position, Quaternion.identity);
        }
    }

    private void draw(Vector3 start, Vector3 end, Color colour)
    {
        GameObject line = new GameObject("line");
        line.transform.position = start;
        var LineRenderer = line.AddComponent<LineRenderer>();
        LineRenderer.material = lineMat;
        LineRenderer.startColor = colour;
        LineRenderer.endColor = colour;
        LineRenderer.startWidth = 3.0f;
        LineRenderer.endWidth = 3.0f;
        LineRenderer.SetPosition(0, start);
        LineRenderer.SetPosition(1, end);
    }

    public enum letters
    {
        unknown = '1',
        save = '(',
        load = ')',
        draw = 'd',
        turnRight = '>',
        turnLeft = '<'
    }
}
