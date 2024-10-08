using IsoContainerPlayback;
using IsoContainerPlayback.Formats.BluRay;
using IsoContainerPlayback.Formats.Dvd;
using IsoContainerPlayback.Formats.VideoCd;
using LibVLCSharp.Shared;

namespace IsoContainerPlayback.TestClient
{
    public partial class mainForm : Form
    {
        #region Constants

        private const string String_NoIsoLoaded = "No ISO Loaded";
        private const string String_Playing = "Playing";
        private const string String_Stopped = "Stopped";
        private const string String_TitleBar = "ISO Container Support Test Client";

        #endregion

        #region Fields

        private readonly LibVLC _libVlc;
        private IsoStream? _isoStream;

        #endregion

        #region Construction

        public mainForm()
        {
            InitializeComponent();

            Text = String_TitleBar;

            _libVlc = new LibVLC();
            videoView.MediaPlayer = new MediaPlayer(_libVlc);
            videoView.MediaPlayer.Playing += MediaPlayer_Playing;
            videoView.MediaPlayer.TimeChanged += MediaPlayer_TimeChanged;

            statusToolStripStatusLabel.Text = String_NoIsoLoaded;
            activeFilenameToolStripStatusLabel.Text = string.Empty;
            positionToolStripStatusLabel.Text = string.Empty;
        }

        #endregion

        #region Methods

        #region Event Handlers

        private void isoStream_ActiveFileChanged(object? sender, string e)
        {
            BeginInvoke(() =>
            {
                activeFilenameToolStripStatusLabel.Text = e;
            });
        }
        private void MediaPlayer_Playing(object? sender, EventArgs e)
        {
            BeginInvoke(() =>
            {
                if (videoView.MediaPlayer!.IsPlaying)
                {
                    statusToolStripStatusLabel.Text = String_Playing;
                }
                else
                {
                    statusToolStripStatusLabel.Text = String_Stopped;
                }
            });
        }
        private void MediaPlayer_TimeChanged(object? sender, MediaPlayerTimeChangedEventArgs e)
        {
            BeginInvoke(() =>
            {
                TimeSpan time = TimeSpan.FromMilliseconds(e.Time);
                positionToolStripStatusLabel.Text = time.ToString("hh':'mm':'ss");
            });
        }
        private void openBluRayISOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _isoStream = new BluRayIsoStream(openFileDialog.FileName, playlistToolStripTextBox.Text);
                    _isoStream.ActiveFileChanged += isoStream_ActiveFileChanged;
                    videoView.MediaPlayer!.Play(new Media(_libVlc, new StreamMediaInput(_isoStream)));
                    Text = $"{String_TitleBar} - {Path.GetFileName(openFileDialog.FileName)}";
                }
                catch (Exception ex)
                {
                    _isoStream?.Dispose();
                    _isoStream = null;
                    Text = String_TitleBar;
                    statusToolStripStatusLabel.Text = String_NoIsoLoaded;
                    MessageBox.Show($"There was a problem playing the selected ISO:\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void openDVDISOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _isoStream = new DvdIsoStream(openFileDialog.FileName, int.Parse(playlistToolStripTextBox.Text));
                    _isoStream.ActiveFileChanged += isoStream_ActiveFileChanged;
                    videoView.MediaPlayer!.Play(new Media(_libVlc, new StreamMediaInput(_isoStream)));
                    Text = $"{String_TitleBar} - {Path.GetFileName(openFileDialog.FileName)}";
                }
                catch (Exception ex)
                {
                    _isoStream?.Dispose();
                    _isoStream = null;
                    Text = String_TitleBar;
                    statusToolStripStatusLabel.Text = String_NoIsoLoaded;
                    MessageBox.Show($"There was a problem playing the selected ISO:\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void openVideoCDISOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _isoStream = new VideoCdIsoStream(openFileDialog.FileName);
                    _isoStream.ActiveFileChanged += isoStream_ActiveFileChanged;
                    videoView.MediaPlayer!.Play(new Media(_libVlc, new StreamMediaInput(_isoStream)));
                    Text = $"{String_TitleBar} - {Path.GetFileName(openFileDialog.FileName)}";
                }
                catch (Exception ex)
                {
                    _isoStream?.Dispose();
                    _isoStream = null;
                    Text = String_TitleBar;
                    statusToolStripStatusLabel.Text = String_NoIsoLoaded;
                    MessageBox.Show($"There was a problem playing the selected ISO:\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Private

        private void Stop()
        {
            if (videoView.MediaPlayer!.IsPlaying)
            {
                videoView.MediaPlayer!.Stop();
            }

            if (_isoStream != null)
            {
                _isoStream.ActiveFileChanged -= isoStream_ActiveFileChanged;
                _isoStream.Dispose();
                _isoStream = null;
            }

            activeFilenameToolStripStatusLabel.Text = string.Empty;
            positionToolStripStatusLabel.Text = string.Empty;
        }

        #endregion

        #endregion
    }
}