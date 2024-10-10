using BDInfo;
using DiscUtils.Udf;
using IsoContainerPlayback.Formats.BluRay;
using IsoContainerPlayback.Formats.Dvd;
using IsoContainerPlayback.Formats.VideoCd;
using LibVLCSharp.Shared;
using System.IO.IsolatedStorage;

namespace IsoContainerPlayback.TestClient
{
    public partial class MainForm : Form
    {
        #region Constants

        private const string String_NoIsoLoaded = "No ISO Loaded";
        private const string String_Playing = "Playing";
        private const string String_Paused = "Paused";
        private const string String_Stopped = "Stopped";
        private const string String_TitleBar = "ISO Container Support Test Client";

        #endregion

        #region Fields

        private readonly LibVLC _libVlc;
        private IsoStream? _isoStream;

        #endregion

        #region Construction

        public MainForm()
        {
            InitializeComponent();

            Text = String_TitleBar;

            _libVlc = new LibVLC();
            videoView.MediaPlayer = new MediaPlayer(_libVlc);
            videoView.MediaPlayer.Playing += MediaPlayer_Playing;
            videoView.MediaPlayer.Paused += MediaPlayer_Paused;
            videoView.MediaPlayer.Stopped += MediaPlayer_Stopped;
            videoView.MediaPlayer.TimeChanged += MediaPlayer_TimeChanged;

            statusToolStripStatusLabel.Text = String_NoIsoLoaded;
            activeFilenameToolStripStatusLabel.Text = string.Empty;
            positionToolStripStatusLabel.Text = string.Empty;
        }

        #endregion

        #region Methods

        #region Event Handlers

        private void ejectToolStripButton_Click(object sender, EventArgs e)
        {
            Eject();
        }
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
                statusToolStripStatusLabel.Text = String_Playing;
                activeFilenameToolStripStatusLabel.Text = _isoStream!.ActiveFile;
            });
        }
        private void MediaPlayer_Stopped(object? sender, EventArgs e)
        {
            BeginInvoke(() =>
            {
                statusToolStripStatusLabel.Text = _isoStream == null ? String_NoIsoLoaded : String_Stopped;
                activeFilenameToolStripStatusLabel.Text = string.Empty;
                positionToolStripStatusLabel.Text = string.Empty;
            });
        }
        private void MediaPlayer_Paused(object? sender, EventArgs e)
        {
            BeginInvoke(() =>
            {
                statusToolStripStatusLabel.Text = String_Paused;
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
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenDisc();
        }
        private void playToolStripButton_Click(object sender, EventArgs e)
        {
            Play();
        }
        private void pauseToolStripButton_Click(object sender, EventArgs e)
        {
            Pause();
        }
        private void stopToolStripButton_Click(object sender, EventArgs e)
        {
            Stop();
        }

        #endregion

        #region Private

        private void Eject()
        {
            Stop();

            if (_isoStream != null)
            {
                _isoStream.ActiveFileChanged -= isoStream_ActiveFileChanged;
                _isoStream.Dispose();
                _isoStream = null;
            }

            Text = String_TitleBar;
            statusToolStripStatusLabel.Text = String_NoIsoLoaded;
            ejectToolStripButton.Enabled = false;
            playToolStripButton.Enabled = false;
            pauseToolStripButton.Enabled = false;
            stopToolStripButton.Enabled = false;
        }
        private void OpenDisc()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Eject();

                // Manually figure out the ISO type
                // BluRay first...
                try
                {
                    var bluRayIso = new BDROM(openFileDialog.FileName);
                    bluRayIso.Scan();

                    if (bluRayIso.PlaylistFiles.Any())
                    {
                        var picker = new PickerForm
                        {
                            Text = "Pick Playlist"
                        };

                        foreach (var playlist in bluRayIso.PlaylistFiles)
                        {
                            picker.AddItem(playlist.Value.Name);
                        }

                        if (picker.ShowDialog() != DialogResult.OK || picker.SelectedItem == null)
                        {
                            return;
                        }

                        _isoStream = new BluRayIsoStream(openFileDialog.FileName, picker.SelectedItem);
                    }
                }
                catch
                {
                    _isoStream = null;
                }

                // If _isoStream is null, try DVD
                if (_isoStream == null)
                {
                    try
                    {
                        var dvdStream = File.OpenRead(openFileDialog.FileName);
                        var dvdReader = new UdfReader(dvdStream);

                        var picker = new PickerForm
                        {
                            Text = "Pick Title"
                        };

                        for (var x = 1; x < 100; x++)
                        {
                            var vobPath = $@"VIDEO_TS\VTS_{x:d2}_1.VOB";
                            if (dvdReader.FileExists(vobPath))
                            {
                                picker.AddItem(x.ToString());
                            }
                        }

                        if (picker.ShowDialog() != DialogResult.OK || picker.SelectedItem == null)
                        {
                            return;
                        }

                        _isoStream = new DvdIsoStream(openFileDialog.FileName, int.Parse(picker.SelectedItem));
                    }
                    catch
                    {
                        _isoStream = null;
                    }
                }

                // If _isoStream is still null, try VideoCD
                if (_isoStream == null)
                {
                    try
                    {
                        _isoStream = new VideoCdIsoStream(openFileDialog.FileName);
                    }
                    catch
                    {
                        _isoStream = null;
                    }
                }

                if (_isoStream != null)
                {
                    try
                    {
                        _isoStream.ActiveFileChanged += isoStream_ActiveFileChanged;
                        videoView.MediaPlayer!.Play(new Media(_libVlc, new StreamMediaInput(_isoStream)));
                        Text = $"{String_TitleBar} - {Path.GetFileName(openFileDialog.FileName)}";

                        ejectToolStripButton.Enabled = true;
                        playToolStripButton.Enabled = true;
                        pauseToolStripButton.Enabled = true;
                        stopToolStripButton.Enabled = true;
                    }
                    catch
                    {
                        Eject();
                    }
                }

                // Couldn't figure it out, or problem playing it.
                if (_isoStream == null)
                {
                    MessageBox.Show("There was a problem playing the selected ISO.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Play()
        {
            if (_isoStream != null)
            {
                videoView.MediaPlayer!.Play();
            }
        }
        private void Pause()
        {
            if (_isoStream != null)
            {
                videoView.MediaPlayer!.Pause();
            }
        }
        private void Stop()
        {
            if (_isoStream != null)
            {
                BeginInvoke(() =>
                {
                    videoView.MediaPlayer!.Stop();
                });
            }
        }

        #endregion

        #endregion

    }
}