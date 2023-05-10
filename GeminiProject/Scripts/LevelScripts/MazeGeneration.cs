using System.Collections;
using System.Collections.Generic;
using LevelScripts;
using UnityEngine;

//Author: Lauren Davis
//Reference: https://youtu.be/OutlTTOm17M

public class MazeGeneration : MonoBehaviour
{
    [SerializeField] MazeNode nodePrefab;
    [SerializeField] GameObject teleportPrefab;
    [SerializeField] GameObject playerPrefab;

    [SerializeField] Vector2Int mazeSize;
    [SerializeField] float nodeScale;

    private void Start()
    {
        GenerateMaze(mazeSize);
    }

    void GenerateMaze(Vector2Int size)
    {
        List<MazeNode> nodes = new List<MazeNode>();
        int random = 0;

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePos = new Vector3(x - (size.x / 2f), 0, y - (size.y / 2f)) * nodeScale;
                MazeNode newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);

                nodes.Add(newNode);
            }
        }

        PositionObject(playerPrefab, nodes[0]);
        PositionObject(teleportPrefab, nodes[nodes.Count - 1]);

        List<MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();

        currentPath.Add(nodes[Random.Range(0, nodes.Count)]);

        while (completedNodes.Count < nodes.Count)
        {
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleSirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            if (currentNodeX < size.x - 1)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) && !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleSirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);

                }
            }
            if (currentNodeX > 0)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) && !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleSirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);

                }
            }
            if (currentNodeY < size.y - 1)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) && !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleSirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }

            if (currentNodeY > 0)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) && !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleSirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }

            if (possibleSirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleSirections.Count);
                MazeNode choseNode = nodes[possibleNextNodes[chosenDirection]];
                random = Random.Range(0, 100);

                switch (possibleSirections[chosenDirection])
                {
                    case 1:
                        choseNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);

                        break;
                    case 2:
                        choseNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);

                        break;
                    case 3:
                        choseNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);

                        if (random > 50)
                        {
                            choseNode.ActivateArrowTrap(1);
                        }

                        break;
                    case 4:
                        choseNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                }

                if(random > 70)
                {
                    choseNode.ActivateSpikeTrap();
                }

                currentPath.Add(choseNode);
                nodes[0].DeactivateArrowTrap(1);
                nodes[0].DeactivateSpikeTrap();

            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);

                currentPath.RemoveAt(currentPath.Count - 1);
            }
        
            
        }
    }

    void PositionObject(GameObject prefab, MazeNode node)
    {
        var newObject = Instantiate(prefab, node.transform.position, Quaternion.identity);
        var teleportRef = newObject.GetComponent<Teleport>();
        if (teleportRef != null)
        {
            teleportRef.SetSceneName("Boss");
            teleportRef.GetComponent<InteractableObject>().descriptionString = "Enter Boss Room";
        }

        
    }
}
