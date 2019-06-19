using System.Collections.Generic;
using System.Linq;
using Sandbox.HelperUtils;
using Xunit;

namespace Sandbox
{
    /// <summary>
    /// https://www.codewars.com/kata/57dd79bff6df9b103b00010f
    /// </summary>
    [Tag(Category.Algorithms | Category.Trees | Category.DataStructures | Category.BinarySearchTrees | Category.Binary)]
    public class IsPerfectTree
    {
        public class TreeNode
        {
            public TreeNode left;
            public TreeNode right;

            public static bool IsPerfect(TreeNode root)
            {
                if (root == null)
                {
                    return true;
                }
                
                var level = new List<TreeNode> {root};
                while (level.All(IsPerfectInner))
                {
                    level = level.SelectMany(node => new[] {node.left, node.right}).ToList();
                }

                return level.All(IsLeaf);
            }

            private static bool IsPerfectInner(TreeNode node) => node.left != null && node.right != null;

            private static bool IsLeaf(TreeNode node) => node.left == null && node.right == null;

            public static TreeNode Leaf()
            {
                return new TreeNode();
            }

            public static TreeNode Join(TreeNode left, TreeNode right)
            {
                return new TreeNode().WithChildren(left, right);
            }

            public TreeNode WithLeft(TreeNode left)
            {
                this.left = left;
                return this;
            }

            public TreeNode WithRight(TreeNode right)
            {
                this.right = right;
                return this;
            }

            public TreeNode WithChildren(TreeNode left, TreeNode right)
            {
                this.left = left;
                this.right = right;
                return this;
            }

            public TreeNode WithLeftLeaf()
            {
                return WithLeft(Leaf());
            }

            public TreeNode WithRightLeaf()
            {
                return WithRight(Leaf());
            }

            public TreeNode WithLeaves()
            {
                return WithChildren(Leaf(), Leaf());
            }
        }

        /**
     * null
     */
        [Fact]
        public void NullTreeShouldBePerfect()
        {
            TreeNode root = null;
            Assert.Equal(true, TreeNode.IsPerfect(root));
        }

        /**
         *   0
         *  / \
         * 0   0
         */
        [Fact]
        public void FullOneLevelTreeShouldBePerfect()
        {
            TreeNode root = TreeNode.Leaf().WithLeaves();
            Assert.Equal(true, TreeNode.IsPerfect(root));
        }

        /**
         *   0
         *  /
         * 0
         */
        [Fact]
        public void OneChildTreeShouldNotBePerfect()
        {
            TreeNode root = TreeNode.Leaf().WithLeftLeaf();
            Assert.Equal(false, TreeNode.IsPerfect(root));
        }
    }
}