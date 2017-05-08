using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;


namespace ConsoleApp1
{
    public class Node : IComparable<Node>
    {
        public int NodeID;
        public int NodeWeight = Int32.MaxValue/2;
        public Node predNode = null;
        public List<Arc> connected = new List<Arc>();
        public Boolean inStructure = false;
        public Boolean addedOnce = false;
        public Boolean visited = false;

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

        public void SetPred(Node n)
        {
            predNode = n;
        }

        public Boolean getInStructure()
        {
            return inStructure;
        }

        public Boolean getAddedOnce()
        {
            return inStructure;
        }

        public Boolean getVisited()
        {
            return visited;
        }

        public void MarkVisited()
        {
            visited = true;
        }

        public void swapInStructure()
        {
            if (inStructure)
            {
                inStructure = false;
            }
            else
            {
                inStructure = true;
            }
        }

        public void swapAddedOnce()
        {
            if (addedOnce)
            {
                addedOnce = false;
            }
            else
            {
                addedOnce = true;
            }
        }

        public void removeArc(Arc A)
        {
            connected.Remove(A);
        }

        public List<Arc> getConnected()
        {
            return connected;
        }

        public void UpdateWeight(int newWeight)
        {
            NodeWeight = newWeight;
        }

        public int getNodeID()
        {
            return NodeID;
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

        public string ToString()
        {
            return "NodeID: " + NodeID;
        }

        public int CompareTo(Node other)
        {
            if (this.NodeWeight < other.NodeWeight) return -1;
            else if (this.NodeWeight > other.NodeWeight) return 1;
            else return 0;
        }
    }

    public class Arc
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
        public static long CallToLaunchA(Node[] NodeArray1,int[] Randpos)
        {
            Console.WriteLine("Child thread starts: Generic Label Correcting Algorithm");
            Stopwatch timer = new Stopwatch();
            timer.Start();
            string temp = GenericLabel_CorrectingAlgorithm(NodeArray1,Randpos);
            timer.Stop();
            Console.WriteLine("\t"+temp);
            long retr = timer.ElapsedMilliseconds;
            Console.WriteLine("\tTime to Complete: " + retr);
            timer.Reset();
            return retr;
        }

        public static void CallToLaunchB(Node[] NodeArray2)
        {
            Console.WriteLine("Child thread starts: Modified Label Correcting Algorithm FIFO");
            Stopwatch timer = new Stopwatch();
            timer.Start();
            string temp=ModifiedLabel_CorrectingAlgorithmFIFO(NodeArray2);
            Console.WriteLine("\t" + temp);
            timer.Stop();
            Console.WriteLine("\tTime to Complete: " + timer.ElapsedMilliseconds);
            timer.Reset();
        }

        public static void CallToLaunchC(Node[] NodeArray3)
        {
            Console.WriteLine("Child thread starts Modified: Label Correcting Algorithm Stack");
            Stopwatch timer = new Stopwatch();
            timer.Start();
            string temp=ModifiedLabel_CorrectingAlgorithmStack(NodeArray3);
            timer.Stop();
            Console.WriteLine("\t" + temp);
            Console.WriteLine("\tTime to Complete: " + timer.ElapsedMilliseconds);
            timer.Reset();
        }

        public static void CallToLaunchD(Node[] NodeArray4)
        {
            Console.WriteLine("Child thread starts: Modified Label Correcting Algorithm Dequeue");
            Stopwatch timer = new Stopwatch();
            timer.Start();
            string temp = ModifiedLabel_CorrectingAlgorithmDequeue(NodeArray4);
            timer.Stop();
            Console.WriteLine("\t" + temp);
            Console.WriteLine("\tTime to Complete: " + timer.ElapsedMilliseconds);
            timer.Reset();
        }

        public static void CallToLaunchE(Node[] NodeArray5)
        {
            Console.WriteLine("Child thread starts: Modified Label Correcting Algorithm Min-Dist");
            Stopwatch timer = new Stopwatch();
            timer.Start();
            string temp = ModifiedLabel_CorrectingAlgorithmMinDist(NodeArray5);
            timer.Stop();
            Console.WriteLine("\t" + temp);
            Console.WriteLine("\tTime to Complete: " + timer.ElapsedMilliseconds);
            timer.Reset();
        }

        public static void Print20(Node[] NodeArray)
        {
            for (int k = 1; k < 20; k++)
            {
                Node cNode = NodeArray[k];
                Console.WriteLine(k+" "+cNode.NodeWeight);
            }
        }

        public static void DebugPrint(Node[] NodeArray)
        {
            for (int k = 1; k < NodeArray.Length-1; k++)
            {
                Console.WriteLine("Node ID: "+ NodeArray[k].NodeID+"-------- Distance: "+ NodeArray[k].NodeWeight+
                                        "-------- Pred Node: ");
            }
        }

        public static void CyclePrinter(Node start)
        {
            Node current = start;
            while (true)
            {
                Console.WriteLine(current.ToString());
                current = current.predNode;
                if (current == start)
                {
                    Console.WriteLine(current.ToString());
                    break;
                }
            }

        }

        public static Boolean CycleDetection(Node[] nodes)
        {
            HashSet<int> stuff = new HashSet<int>();
            //DebugPrint(nodes);
            Console.WriteLine("Running Cycle Detection");
            for (int k = 1; k < nodes.Length; k++)
            {
                stuff.Add(k);
            }
            int n = 1;
            LinkedList<Node> newLinkedList = new LinkedList<Node>();
            while (stuff.Count != 0)
            {
                foreach (int r in stuff)
                {
                    n = r;
                    break;
                }
                int predecessor = n;
                stuff.Remove(n);

                while (true)
                {
                    if (nodes[n].predNode == null && nodes[n].NodeID != -1)
                    {
                        return false;
                    }
                    if (nodes[n].predNode.NodeID == -1)
                    {
                        newLinkedList = new LinkedList<Node>();
                        break;
                    }
                    if (nodes[n].predNode.NodeID == predecessor)
                    {
                        Console.WriteLine("Cycle Detected");
                        CyclePrinter(nodes[n]);
                        return true;
                    }
                    
                    n = nodes[n].predNode.NodeID;
                }
            }
            return false;
        }

        public static string GenericLabel_CorrectingAlgorithm(Node[] nodeArray,int[] randomInts)
        {
            long iter = 0;
            int scalar = 1;
            long distanceLabelUpdates = 0;
            long ArcsScanned = 0;
            
            while (true)
            {
                Boolean updateOccurYet = false;

                foreach (int randomIndex in randomInts)
                { 
                    Node currentNode = nodeArray[randomIndex];
                    int CurWeight = currentNode.getWeight();
                    List<Arc> connectArcs = currentNode.getConnected();
                    foreach (Arc examArc in connectArcs)
                    {
                        Node NextNode = examArc.getNode();
                        int NextNodeID = NextNode.getNodeID();
                        int NextWeight = NextNode.getWeight();
                        int dist = examArc.getDistance();
                        ArcsScanned++;
                        Node MutateNode = nodeArray[NextNodeID];

                        if (MutateNode.getWeight() > currentNode.NodeWeight + dist)
                        {
                           
                            //Console.WriteLine("CurrentNodeID: " + currentNode.NodeID + " Next NodeID: " + NextNodeID);
                            //Console.WriteLine("\tNextNode Weight: " + NextNode.NodeWeight+" Index: ");
                            //var Index = nodeArray.First(x => x.getNodeID() == NextNodeID);
                            //Console.WriteLine("\t"+arrNode.IndexOf(Index));
                            updateOccurYet = true;
                            
                            //NextNode = arrNode.Find(NextNode);
                            MutateNode.SetPred(currentNode);
                            MutateNode.UpdateWeight(CurWeight + dist);
                            distanceLabelUpdates++;
                        }
                    }
                }
                iter++;
                if (scalar < iter)
                {
                    scalar *= 4;
                    Boolean temp = CycleDetection(nodeArray);
                    if (temp)
                    {
                        return " ";
                    }
                }

                if (!updateOccurYet)
                {
                    break;
                }
            }
            return "Arcs Scanned: " + ArcsScanned + ", Distance Label Updates: " + distanceLabelUpdates;
           
        }

        public static string ModifiedLabel_CorrectingAlgorithmFIFO(Node[] nodeArray)
        {
            Queue<Node> ListNode = new Queue<Node>();
            ListNode.Enqueue(nodeArray[1]);
            nodeArray[1].swapInStructure();
            long distanceLabelUpdates = 0;
            long ArcsScanned = 0;
            int iter = 1;
            int scalar = 1;

            while (ListNode.Count !=0)
            {
                Node currentNode = ListNode.Dequeue();
                int CurWeight = currentNode.getWeight();
                currentNode.swapInStructure();
                List<Arc> connectArcs = currentNode.getConnected();
                foreach (Arc examArc in connectArcs)
                {
                    Node NextNode = examArc.getNode();
                    int NextWeight = NextNode.getWeight();
                    int dist = examArc.getDistance();
                    ArcsScanned++;

                    if (NextWeight > CurWeight + dist)
                    {
                        NextNode.SetPred(currentNode);
                        NextNode.UpdateWeight(CurWeight + dist);
                        distanceLabelUpdates++;
                        if (!NextNode.getInStructure())
                        {
                            ListNode.Enqueue(NextNode);
                            NextNode.swapInStructure();
                        }
                    }
                }
                iter++;
                if (iter > scalar * nodeArray.Length)
                {
                    scalar *= 2;
                    Boolean temp =CycleDetection(nodeArray);
                    if (temp)
                    {
                        return " ";
                    }
                }

            }
            return "Arcs Scanned: " + ArcsScanned + ", Distance Label Updates: " + distanceLabelUpdates;
        }

        public static string ModifiedLabel_CorrectingAlgorithmStack(Node[] nodeArray)
        {
            Stack<Node> ListNode = new Stack<Node>();
            ListNode.Push(nodeArray[1]);
            nodeArray[1].swapInStructure();
            long distanceLabelUpdates = 0;
            long ArcsScanned = 0;
            int iter = 1;
            int scalar = 1;
            while (ListNode.Count != 0)
            {
                List<Arc> removal = new List<Arc>();
                Node currentNode = ListNode.Pop();
                currentNode.swapInStructure();
                int CurWeight = currentNode.getWeight();
                List<Arc> connectArcs = currentNode.getConnected();
                foreach (Arc examArc in connectArcs)
                {
                    Node NextNode = examArc.getNode();
                    int NextWeight = NextNode.getWeight();
                    int dist = examArc.getDistance();
                    ArcsScanned++;
          

                    if (NextWeight > currentNode.NodeWeight + dist)
                    {
                      
                        NextNode.SetPred(currentNode);
                        NextNode.UpdateWeight(CurWeight + dist);
                        distanceLabelUpdates++;
                        if (!NextNode.getInStructure())
                        {
                                ListNode.Push(NextNode);
                                NextNode.swapInStructure();
                        }

                    }
   
                }
                iter++;
                if (iter > scalar * nodeArray.Length)
                {
                    scalar *= 8;
                    Boolean temp = CycleDetection(nodeArray);
                    if (temp)
                    {
                        return " ";
                    }
                }

            }
            return "Arcs Scanned: " + ArcsScanned + ", Distance Label Updates: " + distanceLabelUpdates;

        }

        public static string ModifiedLabel_CorrectingAlgorithmDequeue(Node[] nodeArray)
        {
            LinkedList<Node> ListNode = new LinkedList<Node>();
            ListNode.AddFirst(nodeArray[1]);
            nodeArray[1].swapInStructure();
            long distanceLabelUpdate = 0;
            long arcscanned = 0;
            int iter = 1;
            int scalar = 1;
            while (ListNode.Count != 0)

            {
                Node currentNode = ListNode.First();
                ListNode.RemoveFirst();
                currentNode.swapInStructure();
                int CurWeight = currentNode.getWeight();
                List<Arc> connectArcs = currentNode.getConnected();
                foreach (Arc examArc in connectArcs)
                {
                    Node NextNode = examArc.getNode();
                    int NextWeight = NextNode.getWeight();
                    int dist = examArc.getDistance();
                    arcscanned++;

                    if (NextWeight > currentNode.NodeWeight + dist)
                    {
                 
                        NextNode.SetPred(currentNode);
                        NextNode.UpdateWeight(CurWeight + dist);
                        distanceLabelUpdate++;
                        if (!NextNode.getInStructure())
                        {
                            if (NextNode.getAddedOnce())
                            {
                                ListNode.AddFirst(NextNode);
                                NextNode.swapInStructure();
                            }
                            else
                            {
                                NextNode.swapAddedOnce();
                                ListNode.AddLast(NextNode);
                                NextNode.swapInStructure();
                            }
                        }

                    }
                }
                iter++;
                if (iter > scalar * nodeArray.Length)
                {
                    scalar *= 2;
                    Boolean temp = CycleDetection(nodeArray);
                    if (temp)
                    {
                        return " ";
                    }
                }
            }
            return "Arcs Scanned: " + arcscanned + ", Distance Label Updates: " + distanceLabelUpdate;
        }

        public static string ModifiedLabel_CorrectingAlgorithmMinDist(Node[] nodeArray)
        {
            
            List<Node> ListNode = new List<Node>();
            ListNode.Add(nodeArray[1]);
            nodeArray[1].swapInStructure();
            long ArcsScanned = 0;
            long distLabelUpdates = 0;
            int iter = 1;
            int scalar = 2;
            while (ListNode.Count != 0)
            {
                //Console.WriteLine(ListNode.Count);
                Node currentNode = ListNode.Min();
//                for (int k = 0; k < ListNode.Count; k++)
//                {
//                    Node Current = ListNode[k];
//                    if (currentNode.NodeWeight < Current.NodeWeight)
//                    {
//                        currentNode = Current;
//                    }
//
//                }


                ListNode.Remove(currentNode);
                currentNode.swapInStructure();
                int CurWeight = currentNode.getWeight();

                List<Arc> connectArcs = currentNode.getConnected();
                
                foreach (Arc examArc in connectArcs)
                {
                    Node NextNode = examArc.getNode();
                    int NextWeight = NextNode.getWeight();
                    int dist = examArc.getDistance();
                    ArcsScanned++;

                    if (NextWeight > CurWeight + dist)
                    {
                        NextNode.SetPred(currentNode);
                        NextNode.UpdateWeight(CurWeight + dist);
                        distLabelUpdates++;
                        if (!NextNode.getInStructure())
                        {
                            ListNode.Add(NextNode);
                            NextNode.swapInStructure();
                        }
                    }
                }
                iter++;
                if (iter > scalar * nodeArray.Length)
                {
                    scalar *= 2;
                    Boolean temp = CycleDetection(nodeArray);
                    if (temp)
                    {
                        return " ";
                    }
                }
            }
            return "Arcs Scanned: " + ArcsScanned + ", Distance Label Updates: " + distLabelUpdates;
        }
            
        public static void PopulateArray(Node[] arrayNode, int n)
        {
            arrayNode[0] = new Node(-1,0);
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

        public static void CheckAllArrays(Node[] n1, Node[] n2, Node[] n3, Node[] n4, Node[] n5)
        {
            for(int k = 1; k < n1.Length; k++)
            {
                Node NodeAt1 = n1[k];
                Node NodeAt2 = n2[k];
                Node NodeAt3 = n3[k];
                Node NodeAt4 = n4[k];
                Node NodeAt5 = n5[k];

                int W1 = NodeAt2.NodeWeight;
                int W2 = NodeAt2.NodeWeight;
                int W3 = NodeAt2.NodeWeight;
                int W4 = NodeAt4.NodeWeight;
                int W5 = NodeAt5.NodeWeight;

                if (W1 != W2 || W3 != W4 || W1 != W3 || W1 != W5)
                {
                    Console.WriteLine("CheckFailed");
                    Console.WriteLine("\tIndex of Failure: " +k);
                    Console.WriteLine("\tValue From Generic: "+ W1);
                    Console.WriteLine("\tValue From Queue: " + W2);
                    Console.WriteLine("\tValue From Stack: " + W3);
                    Console.WriteLine("\tValue From Dequeue: " + W4);
                    Console.WriteLine("\tValue From Min-Dist: " + W5);
                    return;
                }
//                if (k != 1)
//                {
//                    int Pred1 = NodeAt1.predNode.getNodeID();
//                    int Pred2 = NodeAt2.predNode.getNodeID();
//                    int Pred3 = NodeAt3.predNode.getNodeID();
//                    int Pred4 = NodeAt4.predNode.getNodeID();
//                    int Pred5 = NodeAt5.predNode.getNodeID();
//
//                    if (Pred1 != Pred2 || Pred3 != Pred4 || Pred1 != Pred3 || Pred1 != Pred5)
//                    {
//                        Console.WriteLine("CheckFailed For PredIDs");
//                        Console.WriteLine("\tValue From Generic: " + Pred1);
//                        Console.WriteLine("\tValue From Queue: " + Pred2);
//                        Console.WriteLine("\tValue From Stack: " + Pred3);
//                        Console.WriteLine("\tValue From Dequeue: " + Pred4);
//                        Console.WriteLine("\tValue From Min-Dist: " + Pred5);
//                        return;
//                    }


//                }
               
            }
            Console.WriteLine("Check Complete All Values are the same");

        }

        public static Node[] Shuffle(Node[] ArrayOfNodes)
        {
            List<Node> randomized = new List<Node>();
            List<Node> original = new List<Node>(ArrayOfNodes);
            Random r = new Random();
            while (original.Count > 0)
            {
                int index = r.Next(original.Count);
                randomized.Add(original[index]);
                original.RemoveAt(index);
            }

            return randomized.ToArray();
        }

        public static void resetNodes(Node[] arraNodes)
        {
            for (int k = 1; k < arraNodes.Length; k++)
            {
                arraNodes[k].NodeWeight = Int32.MaxValue / 2;
            }
            arraNodes[1].NodeWeight = 0;
        }

        public static void Shuffle2<T>(T[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                // NextDouble returns a random number between 0 and 1.
                // ... It is equivalent to Math.random() in Java.
                int r = i + (int)(_random.NextDouble() * (n - i));
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
        }


        static Random _random = new Random();

        public static void Main(string[] args)
        {

           
            Node[] NodeArray1 = null;
            Node[] NodeArray2 = null;
            Node[] NodeArray3 = null;
            Node[] NodeArray4 = null;
            Node[] NodeArray5 = null;
            Node[] RandomArray1 = null;
            Node[] RandomArray2 = null;

            //TODO
            string FileName = "FLA_Distance.gr";
            //TODO CHANG FIle Locations
            foreach (string aLine in File.ReadLines(@"C:\Users\wongaz\Documents\MA446ProjectData\"+FileName))
            {
                String[] tempString = aLine.Split(' ');

                if (tempString[0] != "c")
                {
                    if (tempString[0] == "p")
                    {
                        //                        Console.WriteLine("\t" + line);
                        int numofnode = Int32.Parse(tempString[2]) + 1;
                        NodeArray1 = new Node[numofnode];
                        NodeArray2 = new Node[numofnode];
                        NodeArray3 = new Node[numofnode];
                        NodeArray4 = new Node[numofnode];
                        NodeArray5 = new Node[numofnode];
                        RandomArray1 = new Node[numofnode];
                        RandomArray2 = new Node[numofnode];
                        PopulateArray(NodeArray1, numofnode);
                        PopulateArray(NodeArray2, numofnode);
                        PopulateArray(NodeArray3, numofnode);
                        PopulateArray(NodeArray4, numofnode);
                        PopulateArray(NodeArray5, numofnode);
                        PopulateArray(RandomArray1, numofnode);
                        PopulateArray(RandomArray2, numofnode);
                    }

                    if (tempString[0] == "a")
                    {
                        int node1_ID = Int32.Parse(tempString[1]);
                        int node2_ID = Int32.Parse(tempString[2]);
                        int dist = Int32.Parse(tempString[3]);

                        //For Node Array 1
                        Node Node1a = NodeArray1[node1_ID];
                        Node Node2a = NodeArray1[node2_ID];
                        Arc newArca = new Arc(Node2a, dist);
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

                        //For Random Array
                        Node Node1r = RandomArray1[node1_ID];
                        Node Node2r = RandomArray1[node2_ID];
                        Arc newArcr = new Arc(Node2r, dist);
                        Node1r.AddConnectedNode(newArce);

                        Node Node1r1 = RandomArray2[node1_ID];
                        Node Node2r1 = RandomArray2[node2_ID];
                        Arc newArcrr = new Arc(Node2r1, dist);
                        Node1r1.AddConnectedNode(newArce);
                    }
                }
            }

            //DebugPrint(RandomArray1);
            IEnumerable<int> squares = Enumerable.Range(1, RandomArray1.Length-1);
            int[] orInt = squares.ToArray();
           
            Shuffle2(orInt);
           
            //DebugPrint(RandomArray2);

            Console.WriteLine("Done with Populating");

            
            long time2 = CallToLaunchA(RandomArray1,orInt);
            Shuffle2(orInt);
            long time3 = CallToLaunchA(RandomArray2,orInt);
            //DebugPrint(NodeArray1);
            Console.WriteLine("On Average the Generic Algorithm took: "+((time2+time3)/2));
            
            CallToLaunchB(NodeArray2);
            //DebugPrint(NodeArray2);
            CallToLaunchC(NodeArray3);
            //DebugPrint(NodeArray3);
            CallToLaunchD(NodeArray4);

            resetNodes(NodeArray5);
            CallToLaunchE(NodeArray5);

            Console.ReadLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
