using System.Windows.Forms;
using System.IO;
using System.ComponentModel;

namespace FolderBrowser
{
    public partial class FolderBrowserDialog : Form
    {
        public FolderBrowserDialog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            PopulateTreeView();
        }

        /// <summary>
        /// 获取选择的目录的全路径
        /// </summary>
        public string SelectedPath { get; set; }
        /// <summary>
        /// 获取或设置开始浏览的根目录
        /// </summary>
        public string RootFolder { get; set; }

        private void PopulateTreeView()
        {
            TreeNode rootNode;

            if (string.IsNullOrEmpty(RootFolder))
            {
                RootFolder = "/";
            }

            DirectoryInfo info = new DirectoryInfo(RootFolder);
            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode);
                treeView1.Nodes.Add(rootNode);
            }
        }

        private void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subSubDirs;
            foreach (DirectoryInfo subDir in subDirs)
            {
                aNode = new TreeNode(subDir.Name);
                aNode.Tag = subDir;
                subSubDirs = subDir.GetDirectories();
                if (subSubDirs.Length != 0)
                {
                    GetDirectories(subSubDirs, aNode);
                }
                nodeToAddTo.Nodes.Add(aNode);
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            SelectedPath = "";

            this.Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                SelectedPath = "";
            }
            else
            {
                DirectoryInfo info = (DirectoryInfo)treeView1.SelectedNode.Tag;
                SelectedPath = info.FullName;
            }

            textBox1.Text = SelectedPath;
        }
    }
}