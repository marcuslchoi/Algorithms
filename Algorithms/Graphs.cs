using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Algorithms
{
    //3-----4------5
    //|     |      |
    //1---- 2      6
    // \   /
    //   0
    //https://www.udemy.com/course/master-the-coding-interview-data-structures-algorithms/learn/lecture/12371828#overview
    public class Graph
    {
        //assume undirected graph
        int numberOfNodes;
        public Dictionary<int, List<int>> AdjacentList;

        public Graph()
        {
            this.numberOfNodes = 0;
            this.AdjacentList = new Dictionary<int, List<int>>();
        }

        public void AddVertex(int node)
        {
            this.numberOfNodes++;
            this.AdjacentList.Add(node, new List<int>());
        }

        public void AddUndirectedEdge(int node1, int node2)
        {
            if (this.AdjacentList.ContainsKey(node1) &&
                this.AdjacentList.ContainsKey(node2))
            {
                this.AdjacentList[node1].Add(node2);
                this.AdjacentList[node2].Add(node1);
            }
        }

        public void AddDirectedEdge(int fromNode, int toNode)
        {
            if (this.AdjacentList.ContainsKey(fromNode))
            {
                this.AdjacentList[fromNode].Add(toNode);                
            }
        }
    }
    
    public class Graphs
    {
          //  2---0
          // / \
          //1---3

        //the numbers represent nodes (in this case)
        //just a list of edges
        //0 connected to 2, 2 connected to 3, etc
        int[][] edgeList = new int[][]
            {
                new int[] { 0, 2 },
                new int[] { 2, 3 },
                new int[] { 2, 1 },
                new int[] { 1, 3 },
            };
        //adjacency list
        int[][] adjacencyList = new int[][]
            {
                new int[] { 2 },  //index 0 connected to 2
                new int[] { 2, 3 }, //index 1 connected to 2 and 3
                new int[] { 0, 1, 3 }, //index 2 connected to 0, 1, 3
                new int[] { 1, 2 },
            };

        //adjacency matrix
        //also could be dictionary<int, int[]> ie, 0:[0,0,1,0], 1:[0,0,1,1]
        int[][] adjacencyMatrix = new int[][]
            {
                new int[] { 0, 0, 1, 0 }, //row 0 connected to 2
                new int[] { 0, 0, 1, 1 }, //row 1 connected to 2, 3
                new int[] { 1, 1, 0, 1 },
                new int[] { 0, 1, 1, 0 },
            };

        public Graphs()
        {
        }
    }
}
