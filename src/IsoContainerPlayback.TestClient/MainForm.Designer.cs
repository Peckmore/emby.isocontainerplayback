namespace IsoContainerPlayback.TestClient
{
    partial class MainForm
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            toolStripContainer = new ToolStripContainer();
            statusStrip = new StatusStrip();
            statusToolStripStatusLabel = new ToolStripStatusLabel();
            activeFilenameToolStripStatusLabel = new ToolStripStatusLabel();
            positionToolStripStatusLabel = new ToolStripStatusLabel();
            videoView = new LibVLCSharp.WinForms.VideoView();
            toolStrip = new ToolStrip();
            openToolStripButton = new ToolStripButton();
            ejectToolStripButton = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            playToolStripButton = new ToolStripButton();
            pauseToolStripButton = new ToolStripButton();
            stopToolStripButton = new ToolStripButton();
            openFileDialog = new OpenFileDialog();
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
            // activeFilenameToolStripStatusLabel
            // 
            activeFilenameToolStripStatusLabel.Name = "activeFilenameToolStripStatusLabel";
            activeFilenameToolStripStatusLabel.Size = new Size(88, 17);
            activeFilenameToolStripStatusLabel.Text = "ActiveFilename";
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
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip.Items.AddRange(new ToolStripItem[] { openToolStripButton, ejectToolStripButton, toolStripSeparator1, playToolStripButton, pauseToolStripButton, stopToolStripButton });
            toolStrip.Location = new Point(3, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(155, 25);
            toolStrip.TabIndex = 1;
            // 
            // openToolStripButton
            // 
            openToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openToolStripButton.Image = (Image)resources.GetObject("openToolStripButton.Image");
            openToolStripButton.ImageTransparentColor = Color.Magenta;
            openToolStripButton.Name = "openToolStripButton";
            openToolStripButton.Size = new Size(23, 22);
            openToolStripButton.Text = "&Open";
            openToolStripButton.Click += openToolStripButton_Click;
            // 
            // ejectToolStripButton
            // 
            ejectToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ejectToolStripButton.Enabled = false;
            ejectToolStripButton.Image = (Image)resources.GetObject("ejectToolStripButton.Image");
            ejectToolStripButton.ImageTransparentColor = Color.Magenta;
            ejectToolStripButton.Name = "ejectToolStripButton";
            ejectToolStripButton.Size = new Size(23, 22);
            ejectToolStripButton.Text = "&Close";
            ejectToolStripButton.Click += ejectToolStripButton_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // playToolStripButton
            // 
            playToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            playToolStripButton.Enabled = false;
            playToolStripButton.Image = (Image)resources.GetObject("playToolStripButton.Image");
            playToolStripButton.ImageTransparentColor = Color.Magenta;
            playToolStripButton.Name = "playToolStripButton";
            playToolStripButton.Size = new Size(23, 22);
            playToolStripButton.Text = "&Play";
            playToolStripButton.Click += playToolStripButton_Click;
            // 
            // pauseToolStripButton
            // 
            pauseToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            pauseToolStripButton.Enabled = false;
            pauseToolStripButton.Image = (Image)resources.GetObject("pauseToolStripButton.Image");
            pauseToolStripButton.ImageTransparentColor = Color.Magenta;
            pauseToolStripButton.Name = "pauseToolStripButton";
            pauseToolStripButton.Size = new Size(23, 22);
            pauseToolStripButton.Text = "P&ause";
            pauseToolStripButton.Click += pauseToolStripButton_Click;
            // 
            // stopToolStripButton
            // 
            stopToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            stopToolStripButton.Enabled = false;
            stopToolStripButton.Image = (Image)resources.GetObject("stopToolStripButton.Image");
            stopToolStripButton.ImageTransparentColor = Color.Magenta;
            stopToolStripButton.Name = "stopToolStripButton";
            stopToolStripButton.Size = new Size(23, 22);
            stopToolStripButton.Text = "&Stop";
            stopToolStripButton.Click += stopToolStripButton_Click;
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(toolStripContainer);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
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
        private ToolStripButton openToolStripButton;
        private OpenFileDialog openFileDialog;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusToolStripStatusLabel;
        private ToolStripStatusLabel positionToolStripStatusLabel;
        private ToolStripStatusLabel activeFilenameToolStripStatusLabel;
        private ToolStripButton ejectToolStripButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton playToolStripButton;
        private ToolStripButton pauseToolStripButton;
        private ToolStripButton stopToolStripButton;
    }
}
