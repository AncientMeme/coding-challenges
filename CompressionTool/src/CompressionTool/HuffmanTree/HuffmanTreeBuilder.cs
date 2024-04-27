
public class HuffmanTreeBuilder
{
  public HuffmanTreeBuilder()
  {

  }

  public HuffmanTreeNode BuildTree(Dictionary<char, int> charFrequency)
  {
    // Empty frequency table handling
    if(charFrequency.Count == 0)
    {
      return new HuffmanTreeNode(null, 0);
    }

    // Populate priority queue with nodes
    var queue = new PriorityQueue<HuffmanTreeNode, int>();
    foreach(KeyValuePair<char, int>entry in charFrequency)
    {
      queue.Enqueue(new HuffmanTreeNode(entry.Key, entry.Value), entry.Value);
    }

    // Build tree with proirity queue
    while (queue.Count > 1)
    {
      var nodeA = queue.Dequeue();
      var nodeB = queue.Dequeue();

      // Create inner node and attach smaller weight node on left
      int innerWeight = nodeA.weight + nodeB.weight;
      var innerNode = new HuffmanTreeNode(null, innerWeight);
      if (nodeA.weight < nodeB.weight)
      {
        innerNode.SetLeftNode(nodeA);
        innerNode.SetRightNode(nodeB);
      }
      else
      {
        innerNode.SetLeftNode(nodeB);
        innerNode.SetRightNode(nodeA);
      }

      // Readd into queue
      queue.Enqueue(innerNode, innerWeight);
    }

    return queue.Dequeue();
  }
}