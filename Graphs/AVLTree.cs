namespace AcuAlgoNet;

public static partial class Graphs
{
    private class AVLTree<T> where T : IComparable<T>
    {
        private class Node(T value)
        {
            public T Value { get; set; } = value;
            public Node? Left { get; set; }
            public Node? Right { get; set; }
            public int Height { get; set; } = 1;
        }

        private Node? root;

        private void UpdateHeight(Node node)
        {
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        }

        private int GetHeight(Node? node) => node?.Height ?? 0;

        private int GetBalanceFactor(Node node) => GetHeight(node.Left) - GetHeight(node.Right);

        private Node Balance(Node node)
        {
            UpdateHeight(node);
            int balance = GetBalanceFactor(node);

            if (balance > 1)
            {
                if (GetBalanceFactor(node.Left) < 0)
                    node.Left = RotateLeft(node.Left!);
                return RotateRight(node);
            }

            if (balance < -1)
            {
                if (GetBalanceFactor(node.Right) > 0)
                    node.Right = RotateRight(node.Right!);
                return RotateLeft(node);
            }

            return node;
        }

        private Node RotateRight(Node y)
        {
            Node x = y.Left!;
            Node? z = x.Right;

            x.Right = y;
            y.Left = z;

            UpdateHeight(y);
            UpdateHeight(x);

            return x;
        }

        private Node RotateLeft(Node x)
        {
            Node y = x.Right!;
            Node? z = y.Left;

            y.Left = x;
            x.Right = z;

            UpdateHeight(x);
            UpdateHeight(y);

            return y;
        }

        public void Insert(T value)
        {
            root = Insert(root, value);
        }

        private Node Insert(Node? node, T value)
        {
            if (node == null) return new Node(value);

            if (value.CompareTo(node.Value) < 0)
                node.Left = Insert(node.Left, value);
            else if (value.CompareTo(node.Value) > 0)
                node.Right = Insert(node.Right, value);
            else
                return node;

            return Balance(node);
        }

        public bool Find(T value)
        {
            return Find(root, value) != null;
        }

        private Node? Find(Node? node, T value)
        {
            if (node == null || node.Value.Equals(value)) return node;
            return value.CompareTo(node.Value) < 0 ? Find(node.Left, value) : Find(node.Right, value);
        }

        public void Delete(T value)
        {
            root = Delete(root, value);
        }

        private Node? Delete(Node? node, T value)
        {
            if (node == null) return null;

            if (value.CompareTo(node.Value) < 0)
                node.Left = Delete(node.Left, value);
            else if (value.CompareTo(node.Value) > 0)
                node.Right = Delete(node.Right, value);
            else
            {
                if (node.Left == null) return node.Right;
                if (node.Right == null) return node.Left;

                Node minLargerNode = GetMinNode(node.Right);
                node.Value = minLargerNode.Value;
                node.Right = Delete(node.Right, minLargerNode.Value);
            }

            return Balance(node);
        }

        private Node GetMinNode(Node node)
        {
            while (node.Left != null)
                node = node.Left;
            return node;
        }

        public void PrintTree()
        {
            PrintTree(root, "", true);
        }

        private void PrintTree(Node? node, string indent, bool last)
        {
            if (node != null)
            {
                Console.Write(indent);
                Console.Write(last ? "└── " : "├── ");
                Console.WriteLine(node.Value);

                indent += last ? "    " : "│   ";

                PrintTree(node.Left, indent, false);
                PrintTree(node.Right, indent, true);
            }
        }
    }
}
