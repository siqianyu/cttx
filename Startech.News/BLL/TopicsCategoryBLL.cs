using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;
using Startech.Utils;
using System.Web;

namespace Startech.News
{
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

    public class TopicsCategoryBLL
    {
        private readonly TopicsCategoryDAL dal = new TopicsCategoryDAL();

        public TopicsCategoryBLL()
		{}

		#region  成员方法
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(TopicsCategoryModel model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public void Update(TopicsCategoryModel model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int TopicsCategoryId)
		{
			
			dal.Delete(TopicsCategoryId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public TopicsCategoryModel GetModel(int TopicsCategoryId)
		{
			
			return dal.GetModel(TopicsCategoryId);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public DataSet GetTopicsCategoryList(string fields, string filter, string sort, int currentPage, int pageSize, out int count)
        {
            return dal.GetTopicsCategoryList(fields, filter, sort, currentPage, pageSize, out count);
        }

		#endregion  成员方法

        #region 绑定专题类别的下拉框数据

        public ArrayList GetSortedTopicsCategoryItems(int maxLevel,string strWhere)
        {
            try
            {
                ArrayList sortedCategoryItems = new ArrayList();//定义一个自定义数组
                DataSet allCategoryItems = dal.GetAllCategoryItems();//得到所有类别
                DataRow[] drs = allCategoryItems.Tables[0].Select(strWhere, "TopicsCategoryId");
                sortedCategoryItems.Add(new CategoryEntity("--" + drs[0]["title"].ToString() + "", drs[0]["TopicsCategoryId"].ToString()));//增加一行
                this.RecursionFill(allCategoryItems, sortedCategoryItems, drs[0]["TopicsCategoryId"].ToString(), 1, maxLevel,strWhere);
                return sortedCategoryItems;
            }
            catch
            {
                //Response.Redirect("/sysadmin/error.htm");
            }
            return null;
        }

        private void RecursionFill(DataSet dataSource, ArrayList targetToFill, string parentCategoryId, int currentLevel, int maxLevel,string strWhere)
        {
            if (currentLevel == maxLevel && currentLevel != 1) return;
            DataRow[] childCategoryItems = dataSource.Tables[0].Select(String.Format("ParentCategoryId={0} and {1}", parentCategoryId,strWhere), "Sort desc");//按照父类别序号进行排列
            foreach (DataRow categoryItems in childCategoryItems)
            {
                string categoryName = GetAppropriateCategoryName(categoryItems["Title"].ToString(), currentLevel);
                string categoryId = categoryItems["TopicsCategoryId"].ToString();
                targetToFill.Add(new CategoryEntity(categoryName, categoryId));
                RecursionFill(dataSource, targetToFill, categoryId, currentLevel + 1, maxLevel,strWhere);
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
        public TreeNode GetCategoryTreeAll(int nCategoryId,int typeid)
        {
            string nodeValue = "";
            switch (nCategoryId)
            {
                case 1: nodeValue = "专题类别"; break;
            }
            TreeNode root = new TreeNode(nodeValue, "0");
            root.SelectAction = TreeNodeSelectAction.Expand;
            DataSet categoryItems = dal.GetAllCategoryItems();
            AddChildNode(categoryItems, root, nCategoryId.ToString(),typeid);
            return root;
        }
        /// <summary>
        /// 递归取出根节点下所有节点
        /// </summary>
        /// <param name="dataSource">数据源</param>
        /// <param name="parentNode">父节点</param>
        /// <param name="parentCategoryId">父节点id</param>
        private void AddChildNode(DataSet dataSource, TreeNode parentNode, string parentCategoryId,int typeid)
        {
            DataRow[] childCategoryItems = dataSource.Tables[0].Select(String.Format("ParentCategoryId={0}", parentCategoryId), "Sort desc ");
            if (childCategoryItems.Length > 0)
            {
                foreach (DataRow dr in childCategoryItems)
                {
                    string title = dr["Title"].ToString();
                    string categoryId = dr["TopicsCategoryId"].ToString();
                    TreeNode childNode = new TreeNode(title, categoryId);
                    if (dr["Typeid"].ToString() == typeid.ToString())
                    {
                        childNode.ShowCheckBox = true;
                    }
                    childNode.SelectAction = TreeNodeSelectAction.Expand;
                    parentNode.ChildNodes.Add(childNode);
                    AddChildNode(dataSource, childNode, categoryId,typeid);
                }
            }
        }
        #endregion

        /// <summary>
        /// 添加类别权限
        /// </summary>
        /// <param name="categoryId">类别id</param>
        public void UpdateCategoryPermission(string categoryId)
        {
            dal.UpdateCategoryPermission(categoryId);
        }
        public ArrayList GetSelectedTreeNodes(TreeNode root)
        {
            return TreeNodeUtil.GetSelectedTreeNodes(root);
        }
        #region 删除
        /// <summary>
        /// 如果该节点为根节点，且有子节点则不允许删除
        /// </summary>
        /// <param name="categoryIds"></param>
        /// <returns></returns>
        public bool DeleteCategoryParent(string categoryId)
        {
            return dal.DeleteCategoryParent(categoryId);
        }
        public bool DeleteCategory(int[] categoryIds)
        {
            return dal.DeleteCategory(categoryIds);
        }
        #endregion
    }
}
