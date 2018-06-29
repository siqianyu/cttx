using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections;

namespace Startech.Utils
{
  	public sealed class TreeNodeUtil
    {
        public  TreeNodeUtil()
        { }
        public static ArrayList GetSelectedTreeNodes(TreeNode root)
        {
            ArrayList nodes = new ArrayList();
            RecursionAdd(nodes, root);
            return nodes;
        }
        private static void RecursionAdd(ArrayList targetToFill, TreeNode node)
        {
            if (node.Checked == true) targetToFill.Add(node);
            foreach (TreeNode child in node.ChildNodes)
            {
                RecursionAdd(targetToFill, child);
            }
        }

        public static TreeNode EnableSelected(TreeNode root, int[] permissionList)
        {
            if (permissionList == null||permissionList.Length==0) return root;
            ArrayList permissions = new ArrayList(permissionList);
            RecursionCheck(root, permissions);
            return root;
            
        }

        private static void RecursionCheck(TreeNode node, ArrayList permissions)
        {
            if (permissions.Count == 0) return;

            int permissionId = Convert.ToInt32(node.Value);
            if (permissions.Contains(permissionId))
            {
                node.ShowCheckBox = false;
                permissions.Remove(permissionId);
            }
            foreach (TreeNode child in node.ChildNodes)
            {
                RecursionCheck(child, permissions);
            }
        }
    }
}
