namespace BIMTrackWordAddIn
{
    partial class BIMTrackRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public BIMTrackRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BIMTrackRibbon));
            this.BIMTrack = this.Factory.CreateRibbonTab();
            this.Config = this.Factory.CreateRibbonGroup();
            this.txtApiKey = this.Factory.CreateRibbonEditBox();
            this.btnLoadProjects = this.Factory.CreateRibbonButton();
            this.drpDwnProjects = this.Factory.CreateRibbonDropDown();
            this.Actions = this.Factory.CreateRibbonGroup();
            this.btnCreateIssue = this.Factory.CreateRibbonButton();
            this.BIMTrack.SuspendLayout();
            this.Config.SuspendLayout();
            this.Actions.SuspendLayout();
            this.SuspendLayout();
            // 
            // BIMTrack
            // 
            this.BIMTrack.Groups.Add(this.Config);
            this.BIMTrack.Groups.Add(this.Actions);
            this.BIMTrack.Label = "BIM Track";
            this.BIMTrack.Name = "BIMTrack";
            // 
            // Config
            // 
            this.Config.Items.Add(this.txtApiKey);
            this.Config.Items.Add(this.btnLoadProjects);
            this.Config.Items.Add(this.drpDwnProjects);
            this.Config.Label = "Config";
            this.Config.Name = "Config";
            // 
            // txtApiKey
            // 
            this.txtApiKey.Label = "API Key";
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Text = null;
            // 
            // btnLoadProjects
            // 
            this.btnLoadProjects.Label = "Load Projects";
            this.btnLoadProjects.Name = "btnLoadProjects";
            this.btnLoadProjects.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLoadProjects_Click);
            // 
            // drpDwnProjects
            // 
            this.drpDwnProjects.Label = "Projects";
            this.drpDwnProjects.Name = "drpDwnProjects";
            // 
            // Actions
            // 
            this.Actions.Items.Add(this.btnCreateIssue);
            this.Actions.Label = "Actions";
            this.Actions.Name = "Actions";
            // 
            // btnCreateIssue
            // 
            this.btnCreateIssue.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnCreateIssue.Image = ((System.Drawing.Image)(resources.GetObject("btnCreateIssue.Image")));
            this.btnCreateIssue.Label = "Create Issue";
            this.btnCreateIssue.Name = "btnCreateIssue";
            this.btnCreateIssue.ShowImage = true;
            this.btnCreateIssue.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnCreateIssue_Click);
            // 
            // BIMTrackRibbon
            // 
            this.Name = "BIMTrackRibbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.BIMTrack);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.BIMTrackRibbon_Load);
            this.BIMTrack.ResumeLayout(false);
            this.BIMTrack.PerformLayout();
            this.Config.ResumeLayout(false);
            this.Config.PerformLayout();
            this.Actions.ResumeLayout(false);
            this.Actions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab BIMTrack;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Config;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox txtApiKey;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnLoadProjects;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown drpDwnProjects;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Actions;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnCreateIssue;
    }

    partial class ThisRibbonCollection
    {
        internal BIMTrackRibbon BIMTrackRibbon
        {
            get { return this.GetRibbon<BIMTrackRibbon>(); }
        }
    }
}
