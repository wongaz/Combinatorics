using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    class Node
    {
        public int NodeID;
        public int NodeWeight = Int32.MaxValue;
        public Node predNode = null;
        public List<Arc> connected = new List<Arc>();

        public Node(int nodeID)
        {
            NodeID = nodeID;
        }

        public Node(int nodeID, int nodeWeight)
        {
            NodeID = nodeID;
            NodeWeight = nodeWeight;
        }

        public void AddConnectedNode(Arc newArc)
        {
            connected.Add(newArc);
        }

        public string ToString()
        {
            return "NodeID: " + NodeID;
        }

        public void SetPred(Node n)
        {
            predNode = n;
        }

        public List<Arc> getConnected()
        {
            return connected;
        }

        public void UpdateWeight(int newWeight)
        {
            NodeWeight = newWeight;
        }

        public int getWeight()
        {
            return NodeWeight;
        }

        public void PrintConnected()
        {
            System.Console.WriteLine("Connected NodeID: "+NodeID);
            foreach (Arc ar in connected)
            {
                var temp = ar.ToString();
                System.Console.WriteLine(temp);
            }
            
        }
            
    }

    class Arc
    {
        public Node Node1;
        public int distance;

        public Arc(Node n1, int dist)
        {
            Node1 = n1;
            distance = dist;
        }

        public Node getNode()
        {
            return Node1;
        }

        public int getDistance()
        {
            return distance;
        }

        public Arc(Node n1, Node n2, int dist)
        {
            Arc newArc = new Arc(n2, dist);
            n1.AddConnectedNode(newArc);

        }

        public string ToString()
        {
            return Node1.ToString() + "\n\tDistance: " + distance;
        }
    }
       
        
    class Program
    {
        public static void CallToLaunchA(Node[] NodeArray1)
        {
            Console.WriteLine("Child thread starts");
            //Part a
            Console.WriteLine("Generic Label Correcting Algorithm");
            Stopwatch timer = new Stopwatch();
            timer.Start();
            GenericLabel_CorrectingAlgorithm(NodeArray1);
            timer.Stop();
            Console.WriteLine("\tTime to Complete: " + timer.Elapsed);
            timer.Reset();
            //            for (int k = 1; k < NodeArray1.Length; k++)
            //            {
            //                int nw = NodeArray1[k].NodeWeight;
            //                Console.WriteLine("NodeID: "+k+ "\n\tNodeWeight:"+ nw);
            //            }
        }
        public static void CallToLaunchB(Node[] NodeArray2)
        {
            Console.WriteLine("Child thread starts");
            //Part b
            //            Console.WriteLine("Check");
            //            for (int k = 1; k < NodeArray2.Length; k++)
            //            {
            //                int nw = NodeArray2[k].NodeWeight;
            //                Console.WriteLine("NodeID: " + k + "-----NodeWeight:" + nw);
            //            }
            Stopwatch timer = new Stopwatch();
            Console.WriteLine("Modified Label Correcting Algorithm FIFO");
            timer.Start();
            ModifiedLabel_CorrectingAlgorithmFIFO(NodeArray2);
            timer.Stop();
            Console.WriteLine("\tTime to Complete: " + timer.Elapsed);
            timer.Reset();
            //            for (int k = 1; k < NodeArray2.Length; k++)
            //            {
            //                int nw = NodeArray2[k].NodeWeight;
            //                Console.WriteLine("NodeID: " + k + "\n\tNodeWeight:" + nw);
            //            }
        }
        public static void CallToLaunchC(Node[] NodeArray3)
        {
            Console.WriteLine("Child thread starts");
            //Part c
            //            Console.WriteLine("Check For Reset");
            //            for (int k = 1; k < NodeArray3.Length; k++)
            //            {
            //                int nw = NodeArray3[k].NodeWeight;
            //                Console.WriteLine("NodeID: " + k + "-----NodeWeight:" + nw);
            //            }
            Console.WriteLine("Modified Label Correcting Algorithm Stack");
            Stopwatch timer = new Stopwatch();
            timer.Start();
            ModifiedLabel_CorrectingAlgorithmStack(NodeArray3);
            timer.Stop();
            Console.WriteLine("\tTime to Complete: " + timer.Elapsed);
            timer.Reset();
        }

        public static void CallToLaunchD(Node[] NodeArray4)
        {
            Console.WriteLine("Child thread starts");
            //Part d
            //            Console.WriteLine("Check For Reset");
            //            for (int k = 1; k < NodeArray4.Length; k++)
            //            {
            //                int nw = NodeArray4[k].NodeWeight;
            //                Console.WriteLine("NodeID: " + k + "-----NodeWeight:" + nw);
            //            }
            //        
            Console.WriteLine("Modified Label Correcting Algorithm Dequeue");
            Stopwatch timer = new Stopwatch();
            timer.Start();
            ModifiedLabel_CorrectingAlgorithmDequeue(NodeArray4);
            timer.Stop();
            Console.WriteLine("\tTime to Complete: " + timer.Elapsed);
            timer.Reset();
            //            for (int k = 1; k < NodeArray4.Length; k++)
            //            {
            //                int nw = NodeArray4[k].NodeWeight;
            //                Console.WriteLine("NodeID: " + k + "\n\tNodeWeight:" + nw);
            //            }
        }
        public static void CallToLaunchE(Node[] NodeArray5)
        {
            Console.WriteLine("Child thread starts");
            //Part e
            //            Console.WriteLine("Check For Reset");
            //            for (int k = 1; k < NodeArray5.Length; k++)
            //            {
            //                int nw = NodeArray5[k].NodeWeight;
            //                Console.WriteLine("NodeID: " + k + "-----NodeWeight:" + nw);
            //            }
            Console.WriteLine("Modified Label Correcting Algorithm Min-Dist");
            Stopwatch timer = new Stopwatch();
            timer.Start();
            ModifiedLabel_CorrectingAlgorithmMinDist(NodeArray5);
            timer.Stop();
            Console.WriteLine("\tTime to Complete: " + timer.Elapsed);
            timer.Reset();
            //            for (int k = 1; k < NodeArray5.Length; k++)
            //            {
            //                int nw = NodeArray5[k].NodeWeight;
            //                Console.WriteLine("NodeID: " + k + "\n\tNodeWeight:" + nw);
            //            }
        }

        static void Main(string[] args) { 
        
            //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\wongaz\Documents\USA-road-d.USA.gr");

            // Display the file contents by using a foreach loop.
//            System.Console.WriteLine("Contents of WriteLines2.txt = ");
            Node[] NodeArray1 = null;
            Node[] NodeArray2 = null;
            Node[] NodeArray3 = null;
            Node[] NodeArray4 = null;
            Node[] NodeArray5 = null;

            foreach (string aLine in File.ReadLines(@"C:\Users\wongaz\Documents\MA446ProjectData\NY_Distance.gr"))
            {
                String[] tempString = aLine.Split(' ');

                if (tempString[0] != "c")
                {
                    if (tempString[0] == "p")
                    {
//                        Console.WriteLine("\t" + line);
                        int numofnode = Int32.Parse(tempString[2])+1;
                        NodeArray1 = new Node[numofnode];
                        NodeArray2 = new Node[numofnode];
                        NodeArray3 = new Node[numofnode];
                        NodeArray4 = new Node[numofnode];
                        NodeArray5 = new Node[numofnode];

                        PopulateArray(NodeArray1,numofnode);
                        PopulateArray(NodeArray2, numofnode);
                        PopulateArray(NodeArray3, numofnode);
                        PopulateArray(NodeArray4, numofnode);
                        PopulateArray(NodeArray5, numofnode);
                    }

                    if (tempString[0] == "a")
                    {
                        int node1_ID = Int32.Parse(tempString[1]);
                        int node2_ID = Int32.Parse(tempString[2]);
                        int dist = Int32.Parse(tempString[3]);
                        
                        //For Node Array 1
                        Node Node1a = NodeArray1[node1_ID];
                        Node Node2a = NodeArray1[node2_ID];
                        Arc newArca = new Arc(Node2a,dist);
                        Node1a.AddConnectedNode(newArca);
                        
                        //For Node Array 2
                        Node Node1b = NodeArray2[node1_ID];
                        Node Node2b = NodeArray2[node2_ID];
                        Arc newArcb = new Arc(Node2b, dist);
                        Node1b.AddConnectedNode(newArcb);
                        
                        //For Node Array 3
                        Node Node1c = NodeArray3[node1_ID];
                        Node Node2c = NodeArray3[node2_ID];
                        Arc newArcc = new Arc(Node2c, dist);
                        Node1c.AddConnectedNode(newArcc);
                        //For Node Array 4
                        Node Node1d = NodeArray4[node1_ID];
                        Node Node2d = NodeArray4[node2_ID];
                        Arc newArcd = new Arc(Node2d, dist);
                        Node1d.AddConnectedNode(newArcd);
                        //For Node Array 5
                        Node Node1e = NodeArray5[node1_ID];
                        Node Node2e = NodeArray5[node2_ID];
                        Arc newArce = new Arc(Node2e, dist);
                        Node1e.AddConnectedNode(newArce);
                    }   
                }
            }
            //Part a
            Console.WriteLine("Generic Label Correcting Algorithm");
            Stopwatch timer = new Stopwatch();
            timer.Start();
            GenericLabel_CorrectingAlgorithm(NodeArray1);
            timer.Stop();
            Console.WriteLine("\tTime to Complete: " +timer.Elapsed);
            timer.Reset();
//            for (int k = 1; k < NodeArray1.Length; k++)
//            {
//                int nw = NodeArray1[k].NodeWeight;
//                Console.WriteLine("NodeID: "+k+ "\n\tNodeWeight:"+ nw);
//            }
            //Part b
//            Console.WriteLine("Check");
//            for (int k = 1; k < NodeArray2.Length; k++)
//            {
//                int nw = NodeArray2[k].NodeWeight;
//                Console.WriteLine("NodeID: " + k + "-----NodeWeight:" + nw);
//            }
            Console.WriteLine("Modified Label Correcting Algorithm FIFO");
            timer.Start();
            ModifiedLabel_CorrectingAlgorithmFIFO(NodeArray2);
            timer.Stop();
            Console.WriteLine("\tTime to Complete: " + timer.Elapsed);
            timer.Reset();
//            for (int k = 1; k < NodeArray2.Length; k++)
//            {
//                int nw = NodeArray2[k].NodeWeight;
//                Console.WriteLine("NodeID: " + k + "\n\tNodeWeight:" + nw);
//            }
            //Part c
//            Console.WriteLine("Check For Reset");
//            for (int k = 1; k < NodeArray3.Length; k++)
//            {
//                int nw = NodeArray3[k].NodeWeight;
//                Console.WriteLine("NodeID: " + k + "-----NodeWeight:" + nw);
//            }
            Console.WriteLine("Modified Label Correcting Algorithm Stack");
            timer.Start();
            ModifiedLabel_CorrectingAlgorithmStack(NodeArray3);
            timer.Stop();
            Console.WriteLine("\tTime to Complete: " + timer.Elapsed);
            timer.Reset();
//            for (int k = 1; k < NodeArray3.Length; k++)
//            {
//                int nw = NodeArray3[k].NodeWeight;
//                Console.WriteLine("NodeID: " + k + "\n\tNodeWeight:" + nw);
//            }
            //Part d
//            Console.WriteLine("Check For Reset");
//            for (int k = 1; k < NodeArray4.Length; k++)
//            {
//                int nw = NodeArray4[k].NodeWeight;
//                Console.WriteLine("NodeID: " + k + "-----NodeWeight:" + nw);
//            }
//        
            Console.WriteLine("Modified Label Correcting Algorithm Dequeue");
            timer.Start();
            ModifiedLabel_CorrectingAlgorithmDequeue(NodeArray4);
            timer.Stop();
            Console.WriteLine("\tTime to Complete: " + timer.Elapsed);
            timer.Reset();
//            for (int k = 1; k < NodeArray4.Length; k++)
//            {
//                int nw = NodeArray4[k].NodeWeight;
//                Console.WriteLine("NodeID: " + k + "\n\tNodeWeight:" + nw);
//            }
            //Part e
//            Console.WriteLine("Check For Reset");
//            for (int k = 1; k < NodeArray5.Length; k++)
//            {
//                int nw = NodeArray5[k].NodeWeight;
//                Console.WriteLine("NodeID: " + k + "-----NodeWeight:" + nw);
//            }
            Console.WriteLine("Modified Label Correcting Algorithm Min-Dist");
            timer.Start();
            ModifiedLabel_CorrectingAlgorithmMinDist(NodeArray5);
            timer.Stop();
            Console.WriteLine("\tTime to Complete: " + timer.Elapsed);
            timer.Reset();
//            for (int k = 1; k < NodeArray5.Length; k++)
//            {
//                int nw = NodeArray5[k].NodeWeight;
//                Console.WriteLine("NodeID: " + k + "\n\tNodeWeight:" + nw);
//            }
            


            Console.ReadLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
  
        static void GenericLabel_CorrectingAlgorithm(Node[] nodeArray)
        {
            int iteration = 1;
            while (true)
            {
                if (iteration == 3)
                {
                    break;
                }
                for (int k = 1; k < nodeArray.Length; k++)
                {
                    Node currentNode = nodeArray[k];
                    List<Arc> connectArcs = currentNode.getConnected();
                    foreach (Arc examArc in connectArcs)
                    {
                        Node tempNode = examArc.getNode();
                        int value = tempNode.getWeight();
                        int dist = examArc.getDistance();
                        if (value == Int32.MaxValue)
                        {
                            tempNode.SetPred(currentNode);
                            tempNode.UpdateWeight(currentNode.NodeWeight+dist); 
                        }
                        else if (value>currentNode.NodeWeight+dist)
                        {
                            tempNode.SetPred(currentNode);
                            tempNode.UpdateWeight(currentNode.NodeWeight + dist);
                        }
                    }
                }
                iteration++;
            }
        }

        static void ModifiedLabel_CorrectingAlgorithmFIFO(Node[] nodeArray)
        {
            Queue<Node> ListNode = new Queue<Node>();
            ListNode.Enqueue(nodeArray[1]);
            while (ListNode.Count!=0)
            {
                Node currentNode = ListNode.Dequeue();
                List<Arc> connectArcs = currentNode.getConnected();
                foreach (Arc examArc in connectArcs)
                {
                    Node tempNode = examArc.getNode();
                    int value = tempNode.getWeight();
                    int dist = examArc.getDistance();
                    if (value == Int32.MaxValue || value > currentNode.NodeWeight + dist)
                    {
                        tempNode.SetPred(currentNode);
                        tempNode.UpdateWeight(currentNode.NodeWeight + dist);
                        if (!ListNode.Contains(tempNode))
                        {
                            ListNode.Enqueue(tempNode);
                        }
                    }
                }

            }


        }

        static void ModifiedLabel_CorrectingAlgorithmStack(Node[] nodeArray)
        {
            Stack<Node> ListNode = new Stack<Node>();
            ListNode.Push(nodeArray[1]);
            while (ListNode.Count != 0)
            {
                Node currentNode = ListNode.Pop();
                List<Arc> connectArcs = currentNode.getConnected();
                foreach (Arc examArc in connectArcs)
                {
                    Node tempNode = examArc.getNode();
                    int value = tempNode.getWeight();
                    int dist = examArc.getDistance();
                    if (value == Int32.MaxValue || value > currentNode.NodeWeight + dist)
                    {
                        tempNode.SetPred(currentNode);
                        tempNode.UpdateWeight(currentNode.NodeWeight + dist);
                        if (!ListNode.Contains(tempNode))
                        {
                            ListNode.Push(tempNode);
                        }
                    }
                }

            }
        }

        static void ModifiedLabel_CorrectingAlgorithmDequeue(Node[] nodeArray)
        {
            LinkedList<Node> ListNode = new LinkedList<Node>();
            ListNode.AddFirst(nodeArray[1]);
            List<Node> addedOnce = new List<Node>();
            while (ListNode.Count != 0)
            {
                Node currentNode = ListNode.First();
                ListNode.RemoveFirst();
                List<Arc> connectArcs = currentNode.getConnected();
                foreach (Arc examArc in connectArcs)
                {
                    Node tempNode = examArc.getNode();
                    int value = tempNode.getWeight();
                    int dist = examArc.getDistance();
                    if (value == Int32.MaxValue || value > currentNode.NodeWeight + dist)
                    {
                        tempNode.SetPred(currentNode);
                        tempNode.UpdateWeight(currentNode.NodeWeight + dist);
                        if (!ListNode.Contains(tempNode))
                        {
                            if (addedOnce.Contains(tempNode))
                            {
                                ListNode.AddFirst(tempNode);
                            }
                            else
                            {
                                addedOnce.Add(tempNode);
                                ListNode.AddLast(tempNode);
                            }
                        }
                    }
                }
            }
        }

        static void ModifiedLabel_CorrectingAlgorithmMinDist(Node[] nodeArray)
        {
            List<Node> ListNode = new List<Node>();
            ListNode.Add(nodeArray[1]);
            while (ListNode.Count != 0)
            {
                Node currentNode = minFinder(ListNode);
                ListNode.Remove(currentNode);
                List<Arc> connectArcs = currentNode.getConnected();
                foreach (Arc examArc in connectArcs)
                {
                    Node tempNode = examArc.getNode();
                    int value = tempNode.getWeight();
                    int dist = examArc.getDistance();
                    if (value == Int32.MaxValue || value > currentNode.NodeWeight + dist)
                    {
                        tempNode.SetPred(currentNode);
                        tempNode.UpdateWeight(currentNode.NodeWeight + dist);
                        if (!ListNode.Contains(tempNode))
                        {
                            ListNode.Add(tempNode);
                        }
                    }
                }

            }
        }

        static Node minFinder(List<Node> nodeList)
        {
            Node smallestNodeByWeight = nodeList[0];
            for (int k = 1; k < nodeList.Count; k++)
            {
                Node CurrentNode = nodeList[k];
                if (CurrentNode.NodeWeight < smallestNodeByWeight.NodeWeight)
                {
                    smallestNodeByWeight = CurrentNode;
                }

            }
            return smallestNodeByWeight;
        }

        static void PopulateArray(Node[] arrayNode, int n)
        {
            arrayNode[0] = null;
            for (int i = 1; i < n; i++)
            {
                if (i == 1)
                {
                    arrayNode[i] = new Node(i, 0);
                }
                else
                {
                    arrayNode[i] = new Node(i);
                }

            }
        }
        
    }
}
