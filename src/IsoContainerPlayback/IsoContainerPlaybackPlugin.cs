using DiscUtils.CoreCompat;
using DiscUtils.OpticalDisk;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Drawing;
using MediaBrowser.Model.Logging;
using System;
using System.IO;

namespace IsoContainerPlayback
{
    /// <summary>
    /// The ISO Container Playback plugin for Emby.
    /// </summary>
    public class IsoContainerPlaybackPlugin : BasePlugin, IHasThumbImage
    {
        #region Constants

        // Define a temporary hardcoded path for the ISO file we'll work with until we switch it to be dynamic.
        public const string IsoPath = @"c:\temp\bluray.iso";

        #endregion

        #region Fields

        private readonly Guid _id = new Guid("CD84A2F5-C01E-464A-8C48-3A407761034F"); // Do not change
        private readonly ILogger _logger;

        #endregion

        #region Construction

        /// <summary>Initializes a new instance of the <see cref="IsoContainerPlaybackPlugin" /> class.</summary>
        /// <param name="logManager">The log manager.</param>
        public IsoContainerPlaybackPlugin(ILogManager logManager)
        {
            _logger = logManager.GetLogger(Name);

            _logger.Info($"{Name} plugin is loading...", 0);

            // We need to setup DiscUtils to register Disc support.
            _logger.Info($"Registering DiscUtils ISO support...", 0);
            DiscUtils.Setup.SetupHelper.RegisterAssembly(ReflectionHelper.GetAssembly(typeof(Disc)));

            _logger.Info($"{Name} plugin is loaded", 0);
        }

        #endregion

        #region Properties

        /// <inheritdoc/>
        public override string Description => "Allows for the playback of videos stored within Blu-Ray and DVD ISO files.";
        /// <inheritdoc/>
        public override Guid Id => _id;
        /// <inheritdoc/>
        public override sealed string Name => "ISO Container Support";
        /// <summary>Gets the thumb image format.</summary>
        /// <value>The thumb image format.</value>
        public ImageFormat ThumbImageFormat => ImageFormat.Png;

        #endregion

        #region Methods

        /// <summary>Gets the thumb image.</summary>
        /// <returns>An image stream.</returns>
        public Stream GetThumbImage()
        {
            var type = GetType();
            return type.Assembly.GetManifestResourceStream(type.Namespace + ".ThumbImage.png");
        }
        /// <inheritdoc/>
        public override void OnUninstalling()
        {
            _logger.Info($"{Name} plugin is getting uninstalled.", 0);
            base.OnUninstalling();
        }

        #endregion
    }
}