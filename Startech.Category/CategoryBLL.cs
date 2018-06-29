using System;
using System.Collections.Generic;
using System.Text;
using System.Data ;
using System.Web .UI .WebControls ;
using System.Collections;
using System.Web;
using Startech.Utils;


namespace Startech.Category
{
    /// <summary>
    /// 业务逻辑类CategoryBLL 的摘要说明。
    /// </summary>
    public class CategoryEntity
    {
        public CategoryEntity(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }
        public string Name;
        public string Id;
    }
    public class CategoryBLL
    {
        private readonly CategoryDAL dal = new CategoryDAL();
        public CategoryBLL()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(CategoryModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(CategoryModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CategoryModel GetModel(int CategoryId)
        {

            return dal.GetModel(CategoryId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }


        #region 绑定新闻类别的下拉框数据

        public ArrayList GetSortedArticleCategoryItems(int maxLevel, string strCategoryId)
        {
            try
            {
                ArrayList sortedCategoryItems = new ArrayList();//定义一个自定义数组
                // DataSet allCategoryItems = dal.GetAllCategoryItems();//得到所有文章类别
                DataSet allCategoryItems = dal.GetCategoryItems();
                DataRow[] drs = allCategoryItems.Tables[0].Select(string.Format("categoryId={0}", strCategoryId), "sort");
                sortedCategoryItems.Add(new CategoryEntity("--" + drs[0]["categoryname"].ToString() + "", drs[0]["categoryId"].ToString()));//增加一行
                this.RecursionFill(allCategoryItems, sortedCategoryItems, drs[0]["categoryId"].ToString(), 1, maxLevel);
                return sortedCategoryItems;
            }
            catch
            {
                //Response.Redirect("/sysadmin/error.htm");
            }
            return null;
        }

        private void RecursionFill(DataSet dataSource, ArrayList targetToFill, string parentCategoryId, int currentLevel, int maxLevel)
        {
            if (currentLevel == maxLevel && currentLevel != 1) return;
            DataRow[] childCategoryItems = dataSource.Tables[0].Select(String.Format("ParentCategoryId={0}", parentCategoryId), "Sort desc");//按照父类别序号进行排列
            foreach (DataRow categoryItems in childCategoryItems)
            {
                string categoryName = GetAppropriateCategoryName(categoryItems["categoryname"].ToString(), currentLevel);
                string categoryId = categoryItems["CategoryId"].ToString();
                targetToFill.Add(new CategoryEntity(categoryName, categoryId));
                RecursionFill(dataSource, targetToFill, categoryId, currentLevel + 1, maxLevel);
            }
        }

        private string GetAppropriateCategoryName(string categoryName, int level)
        {
            StringBuilder sb = new StringBuilder();
            for (; level > 0; level--)
            {
                sb.Append("－－");
            }
            sb.Append(categoryName);
            return sb.ToString();
        }

        #endregion

        #region  获取类别树
        /// <summary>
        /// 获得所有类别节点
        /// </summary>
        /// <returns></returns>
        public TreeNode GetCategoryTreeAll()
        {
            TreeNode root = new TreeNode("所有类别", "0");
            root.SelectAction = TreeNodeSelectAction.Expand;
            DataSet categoryItems = GetList(" 1=1  order by sort");
            AddChildNode(categoryItems, root, root.Value);
            return root;
        }
        //绑定模块类别
        public TreeNode GetCategoryTree(int nCategoryId)
        {
            string nodeValue = "";
            switch (nCategoryId)
            {
                case 1: nodeValue = "文章类别"; break;
                case 2: nodeValue = "新闻类别"; break;
            }
            TreeNode root = new TreeNode(nodeValue, "0");
            root.SelectAction = TreeNodeSelectAction.Expand;

            // DataSet categoryItems = dal.GetAllCategoryItems();
            DataSet categoryItems = dal.GetCategoryItems();

            AddChildNode(categoryItems, root, nCategoryId.ToString());
            return root;
        }
        /// <summary>
        /// 递归取出根节点下所有节点
        /// </summary>
        /// <param name="dataSource">数据源</param>
        /// <param name="parentNode">父节点</param>
        /// <param name="parentCategoryId">父节点id</param>
        private void AddChildNode(DataSet dataSource, TreeNode parentNode, string parentCategoryId)
        {
            DataRow[] childCategoryItems = dataSource.Tables[0].Select(String.Format("ParentCategoryId={0}", parentCategoryId), "Sort desc ");

            if (childCategoryItems.Length > 0)
            {
                foreach (DataRow dr in childCategoryItems)
                {
                    string title = dr["categoryname"].ToString();
                    string categoryId = dr["CategoryId"].ToString();
                    TreeNode childNode = new TreeNode(title, categoryId);
                    childNode.ShowCheckBox = true;
                    childNode.SelectAction = TreeNodeSelectAction.Expand;
                    parentNode.ChildNodes.Add(childNode);
                    AddChildNode(dataSource, childNode, categoryId);
                }
            }
        }
        #endregion

        public ArrayList GetSelectedTreeNodes(TreeNode root)
        {
            return TreeNodeUtil.GetSelectedTreeNodes(root);
        }
        public int AddCategory(CategoryModel detail)
        {
            return Add(detail);
        }

        public bool UpdateCategory(CategoryModel detail)
        {
            return Update(detail);
        }

        public bool DeleteCategory(int[] categoryIds)
        {
            return dal.DeleteCategory(categoryIds);
        }
        /// <summary>
        /// 如果该节点为根节点，且有子节点则不允许删除
        /// </summary>
        /// <param name="categoryIds"></param>
        /// <returns></returns>
        public bool DeleteCategoryParent(string categoryId)
        {
            return dal.DeleteCategoryParent(categoryId);
        }

        /// <summary>
        /// 如果该节点为二级结点，则不允许删除
        /// </summary>
        /// <param name="categoryIds"></param>
        /// <returns></returns>
        public bool DeleteCategorySecond(string categoryId, string parentId)
        {
            return dal.DeleteCategorySecond(categoryId, parentId);
        }
        /// <summary>
        /// 添加类别权限
        /// </summary>
        /// <param name="categoryId">类别id</param>
        public void UpdateCategoryPermission(string categoryId)
        {
            dal.UpdateCategoryPermission(categoryId);
        }
        #endregion
    }
}