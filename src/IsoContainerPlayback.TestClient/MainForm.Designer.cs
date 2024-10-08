namespace IsoContainerPlayback.TestClient
{
    partial class mainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            toolStripContainer = new ToolStripContainer();
            statusStrip = new StatusStrip();
            statusToolStripStatusLabel = new ToolStripStatusLabel();
            positionToolStripStatusLabel = new ToolStripStatusLabel();
            videoView = new LibVLCSharp.WinForms.VideoView();
            toolStrip = new ToolStrip();
            openToolStripDropDownButton = new ToolStripDropDownButton();
            openVideoCDISOToolStripMenuItem = new ToolStripMenuItem();
            openDVDISOToolStripMenuItem = new ToolStripMenuItem();
            openBluRayISOToolStripMenuItem = new ToolStripMenuItem();
            playlistToolStripTextBox = new ToolStripTextBox();
            toolStripSeparator = new ToolStripSeparator();
            helpToolStripButton = new ToolStripButton();
            openFileDialog = new OpenFileDialog();
            activeFilenameToolStripStatusLabel = new ToolStripStatusLabel();
            toolStripContainer.BottomToolStripPanel.SuspendLayout();
            toolStripContainer.ContentPanel.SuspendLayout();
            toolStripContainer.TopToolStripPanel.SuspendLayout();
            toolStripContainer.SuspendLayout();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)videoView).BeginInit();
            toolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.BottomToolStripPanel
            // 
            toolStripContainer.BottomToolStripPanel.Controls.Add(statusStrip);
            // 
            // toolStripContainer.ContentPanel
            // 
            toolStripContainer.ContentPanel.Controls.Add(videoView);
            toolStripContainer.ContentPanel.Size = new Size(800, 403);
            toolStripContainer.Dock = DockStyle.Fill;
            toolStripContainer.Location = new Point(0, 0);
            toolStripContainer.Name = "toolStripContainer";
            toolStripContainer.Size = new Size(800, 450);
            toolStripContainer.TabIndex = 0;
            toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            toolStripContainer.TopToolStripPanel.Controls.Add(toolStrip);
            // 
            // statusStrip
            // 
            statusStrip.Dock = DockStyle.None;
            statusStrip.Items.AddRange(new ToolStripItem[] { statusToolStripStatusLabel, activeFilenameToolStripStatusLabel, positionToolStripStatusLabel });
            statusStrip.Location = new Point(0, 0);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(800, 22);
            statusStrip.TabIndex = 0;
            // 
            // statusToolStripStatusLabel
            // 
            statusToolStripStatusLabel.Name = "statusToolStripStatusLabel";
            statusToolStripStatusLabel.Size = new Size(39, 17);
            statusToolStripStatusLabel.Text = "Status";
            // 
            // positionToolStripStatusLabel
            // 
            positionToolStripStatusLabel.Name = "positionToolStripStatusLabel";
            positionToolStripStatusLabel.Size = new Size(50, 17);
            positionToolStripStatusLabel.Text = "Position";
            // 
            // videoView
            // 
            videoView.BackColor = Color.Black;
            videoView.Dock = DockStyle.Fill;
            videoView.Location = new Point(0, 0);
            videoView.MediaPlayer = null;
            videoView.Name = "videoView";
            videoView.Size = new Size(800, 403);
            videoView.TabIndex = 0;
            videoView.Text = "videoView";
            // 
            // toolStrip
            // 
            toolStrip.Dock = DockStyle.None;
            toolStrip.Items.AddRange(new ToolStripItem[] { openToolStripDropDownButton, playlistToolStripTextBox, toolStripSeparator, helpToolStripButton });
            toolStrip.Location = new Point(3, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(172, 25);
            toolStrip.TabIndex = 1;
            // 
            // openToolStripDropDownButton
            // 
            openToolStripDropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openToolStripDropDownButton.DropDownItems.AddRange(new ToolStripItem[] { openVideoCDISOToolStripMenuItem, openDVDISOToolStripMenuItem, openBluRayISOToolStripMenuItem });
            openToolStripDropDownButton.Image = (Image)resources.GetObject("openToolStripDropDownButton.Image");
            openToolStripDropDownButton.ImageTransparentColor = Color.Magenta;
            openToolStripDropDownButton.Name = "openToolStripDropDownButton";
            openToolStripDropDownButton.Size = new Size(29, 22);
            openToolStripDropDownButton.Text = "&Open";
            // 
            // openVideoCDISOToolStripMenuItem
            // 
            openVideoCDISOToolStripMenuItem.Name = "openVideoCDISOToolStripMenuItem";
            openVideoCDISOToolStripMenuItem.Size = new Size(182, 22);
            openVideoCDISOToolStripMenuItem.Text = "Open VideoCD ISO...";
            openVideoCDISOToolStripMenuItem.Click += openVideoCDISOToolStripMenuItem_Click;
            // 
            // openDVDISOToolStripMenuItem
            // 
            openDVDISOToolStripMenuItem.Name = "openDVDISOToolStripMenuItem";
            openDVDISOToolStripMenuItem.Size = new Size(182, 22);
            openDVDISOToolStripMenuItem.Text = "Open DVD ISO...";
            openDVDISOToolStripMenuItem.Click += openDVDISOToolStripMenuItem_Click;
            // 
            // openBluRayISOToolStripMenuItem
            // 
            openBluRayISOToolStripMenuItem.Name = "openBluRayISOToolStripMenuItem";
            openBluRayISOToolStripMenuItem.Size = new Size(182, 22);
            openBluRayISOToolStripMenuItem.Text = "Open BluRay ISO...";
            openBluRayISOToolStripMenuItem.Click += openBluRayISOToolStripMenuItem_Click;
            // 
            // playlistToolStripTextBox
            // 
            playlistToolStripTextBox.Name = "playlistToolStripTextBox";
            playlistToolStripTextBox.Size = new Size(100, 25);
            playlistToolStripTextBox.Text = "00000.MPLS";
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(6, 25);
            // 
            // helpToolStripButton
            // 
            helpToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            helpToolStripButton.Enabled = false;
            helpToolStripButton.Image = (Image)resources.GetObject("helpToolStripButton.Image");
            helpToolStripButton.ImageTransparentColor = Color.Magenta;
            helpToolStripButton.Name = "helpToolStripButton";
            helpToolStripButton.Size = new Size(23, 22);
            helpToolStripButton.Text = "He&lp";
            // 
            // openFileDialog
            // 
            openFileDialog.AddExtension = false;
            openFileDialog.AddToRecent = false;
            openFileDialog.Filter = "ISO files|*.iso";
            openFileDialog.ShowHiddenFiles = true;
            openFileDialog.ShowPinnedPlaces = false;
            openFileDialog.SupportMultiDottedExtensions = true;
            // 
            // activeFilenameToolStripStatusLabel
            // 
            activeFilenameToolStripStatusLabel.Name = "activeFilenameToolStripStatusLabel";
            activeFilenameToolStripStatusLabel.Size = new Size(88, 17);
            activeFilenameToolStripStatusLabel.Text = "ActiveFilename";
            // 
            // mainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(toolStripContainer);
            Name = "mainForm";
            StartPosition = FormStartPosition.CenterScreen;
            toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            toolStripContainer.BottomToolStripPanel.PerformLayout();
            toolStripContainer.ContentPanel.ResumeLayout(false);
            toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer.TopToolStripPanel.PerformLayout();
            toolStripContainer.ResumeLayout(false);
            toolStripContainer.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)videoView).EndInit();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ToolStripContainer toolStripContainer;
        private ToolStrip topToolStrip;
        private LibVLCSharp.WinForms.VideoView videoView;
        private ToolStripButton playIsoToolStripButton;
        private ToolStrip toolStrip;
        private ToolStripDropDownButton openToolStripDropDownButton;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripButton helpToolStripButton;
        private OpenFileDialog openFileDialog;
        private ToolStripTextBox playlistToolStripTextBox;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusToolStripStatusLabel;
        private ToolStripMenuItem openDVDISOToolStripMenuItem;
        private ToolStripMenuItem openBluRayISOToolStripMenuItem;
        private ToolStripStatusLabel positionToolStripStatusLabel;
        private ToolStripMenuItem openVideoCDISOToolStripMenuItem;
        private ToolStripStatusLabel activeFilenameToolStripStatusLabel;
    }
}
