using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            //var bt2 = new BinaryTree<string>(
            //    "F", 
            //    new BinaryTree<string>(
            //        "B",
            //        new BinaryTree<string>("A"),
            //        new BinaryTree<string>(
            //            "D", 
            //            new BinaryTree<string>("C"), 
            //            new BinaryTree<string>("E")
            //            ), 
            //    new BinaryTree<string>("G"));
            var bt2 = new BinaryTree<string>(
                "F",
                new BinaryTree<string>(
                    "B",
                    new BinaryTree<string>("A"),
                    new BinaryTree<string>(
                        "D",
                        new BinaryTree<string>("C"),
                        new BinaryTree<string>("E")
                    )
                ),
                new BinaryTree<string>(
                    "G",
                    null,
                    new BinaryTree<string>(
                        "I",
                        new BinaryTree<string>("H"),
                        null))
                );
            bt2.Traverse(BinaryTraverseOrder.PreOrder).ToList().ForEach(t => Console.Write(t.Node));
            Console.WriteLine();
            bt2.Traverse(BinaryTraverseOrder.InOrder).ToList().ForEach(t => Console.Write(t.Node));
            Console.WriteLine();
            bt2.Traverse(BinaryTraverseOrder.OutOrder).ToList().ForEach(t => Console.Write(t.Node));
            Console.WriteLine();
            bt2.Traverse(BinaryTraverseOrder.PostOrder).ToList().ForEach(t => Console.Write(t.Node));
            var ty = args.GetType();


            var rcBt = BinaryTree<string>.RecoverBinaryTreeByPreOrderAndInOrder(
                new string[] { "A", "B", "C", "D", "E", "F", "G", "H" },
                new string[] { "C", "B", "E", "D", "F", "A", "G", "H" }
                );

            var postRcBt = rcBt.Traverse(BinaryTraverseOrder.PostOrder).ToArray();
            Console.WriteLine();
            rcBt.Traverse(BinaryTraverseOrder.PostOrder).ToList().ForEach(t => Console.Write(t.Node));

            Console.ReadLine();

        }
    }

    public class BinaryTree<T>
    {
        public BinaryTree()
        {

        }
        public BinaryTree(T node)
        {
            Node = node;
        }

        public BinaryTree(T node, BinaryTree<T> left, BinaryTree<T> right)
        {
            Node = node;
            LeftNode = left;
            RightNode = right;
        }

        public BinaryTree(T[] nodes, BinaryTraverseOrder order)
        {


        }

        public T Node { get; set; }

        public BinaryTree<T> LeftNode { get; set; }
        public BinaryTree<T> RightNode { get; set; }

        private IEnumerable<BinaryTree<T>> PreOrderTraverse()
        {
            yield return this;
            if (LeftNode != null)
            {
                foreach (var item in LeftNode.Traverse(BinaryTraverseOrder.PreOrder))
                {
                    yield return item;
                }
            }
            if (RightNode != null)
            {
                foreach (var item in RightNode.Traverse(BinaryTraverseOrder.PreOrder))
                {
                    yield return item;
                }
            }
        }

        private IEnumerable<BinaryTree<T>> InOrderTraverse()
        {
            if (LeftNode != null)
            {
                foreach (var item in LeftNode.Traverse(BinaryTraverseOrder.InOrder))
                {
                    yield return item;
                }
            }
            yield return this;
            if (RightNode != null)
            {
                foreach (var item in RightNode.Traverse(BinaryTraverseOrder.InOrder))
                {
                    yield return item;
                }
            }
        }

        private IEnumerable<BinaryTree<T>> OutOrderTraverse()
        {
            if (RightNode != null)
            {
                foreach (var item in RightNode.OutOrderTraverse())
                {
                    yield return item;
                }
            }
            yield return this;
            if (LeftNode != null)
            {
                foreach (var item in LeftNode.OutOrderTraverse())
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<BinaryTree<T>> Traverse(BinaryTraverseOrder order)
        {
            switch (order)
            {
                case BinaryTraverseOrder.PreOrder:
                    { return PreOrderTraverse(); }
                case BinaryTraverseOrder.InOrder:
                    {
                        return InOrderTraverse();
                    }

                case BinaryTraverseOrder.OutOrder:
                    {
                        return OutOrderTraverse();
                    }

                case BinaryTraverseOrder.PostOrder:
                    {
                        return PostOrderTraverse();
                    }
                default:
                    { return Traverse(BinaryTraverseOrder.PreOrder); }
            }
        }

        private IEnumerable<BinaryTree<T>> PostOrderTraverse()
        {

            if (LeftNode != null)
            {
                foreach (var item in LeftNode.Traverse(BinaryTraverseOrder.PostOrder))
                {
                    yield return item;
                }
            }
            if (RightNode != null)
            {
                foreach (var item in RightNode.Traverse(BinaryTraverseOrder.PostOrder))
                {
                    yield return item;
                }
            }
            yield return this;
        }

        /// <summary>
        /// 恢复二叉树(输入前序和中序)
        /// </summary>
        /// <param name="preOrder">前序</param>
        /// <param name="inOrder">中序</param>
        /// <returns></returns>
        public static BinaryTree<T> RecoverBinaryTreeByPreOrderAndInOrder(T[] preOrder, T[] inOrder)
        {
            if (preOrder.Length == 0)
            {
                return null;
            }
            else
            {
                var ne = preOrder[0];
                var neIndex = inOrder.ToList().IndexOf(ne);
                var leftInOrderNodes = inOrder.ToList().GetRange(0, neIndex).ToArray();
                var leftPreOrderNodes = preOrder.ToList().GetRange(1, neIndex).ToArray();
                var rightInOrderNodes = inOrder.ToList().GetRange(neIndex + 1, inOrder.Length - neIndex - 1).ToArray();
                var rightPreOderNodes = preOrder.ToList().GetRange(neIndex + 1, inOrder.Length - neIndex - 1).ToArray();
                BinaryTree<T> resultTree = new BinaryTree<T>()
                {
                    Node = ne,
                    LeftNode = RecoverBinaryTreeByPreOrderAndInOrder(leftPreOrderNodes, leftInOrderNodes),
                    RightNode = RecoverBinaryTreeByPreOrderAndInOrder(rightPreOderNodes, rightInOrderNodes)
                };
                return resultTree;
            }
        }
    }

    public enum BinaryTraverseOrder
    {
        /// <summary>
        /// 前序NLR
        /// </summary>
        PreOrder,
        /// <summary>
        /// 中序LNR
        /// </summary>
        InOrder,
        /// <summary>
        /// RNL
        /// </summary>
        OutOrder,
        /// <summary>
        /// 后序LRN
        /// </summary>
        PostOrder
    }



}
