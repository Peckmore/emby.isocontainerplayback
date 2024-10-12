using System;

namespace IsoContainerPlayback
{
    /// <summary>
    /// Represents the entry for a file or directory on an ISO.
    /// </summary>
    public class IsoDirectoryEntryInfo
    {
        #region Construction

        /// <summary>
        /// Creates a new <see cref="IsoDirectoryEntryInfo" /> instance.
        /// </summary>
        /// <param name="name">The name of the file or directory.</param>
        /// <param name="fullName">The full name of the file or directory, including path.</param>
        /// <param name="isDirectory">A <see cref="bool" /> indicating whether this entry is a file or directory.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public IsoDirectoryEntryInfo(string name, string fullName, bool isDirectory)
        {
            // Check that name is not null.
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            // Check that fullName is not null.
            if (string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            // Set our properties.
            Name = name;
            FullName = fullName;
            IsDirectory = isDirectory;
        }

        #endregion

        #region Properties

        /// <summary>
        /// A <see cref="bool" /> indicating whether this entry is a file or directory.
        /// </summary>
        /// <returns><see langword="true" /> if the entry is a directory; otherwise <see langword="false" />.</returns>
        public bool IsDirectory { get; }
        /// <summary>
        /// A <see cref="bool" /> indicating whether this entry is a file or directory.
        /// </summary>
        /// <returns><see langword="true" /> if the entry is a file; otherwise <see langword="false" />.</returns>
        public bool IsFile => !IsDirectory;
        /// <summary>
        /// The full name of the file or directory, including path.
        /// </summary>
        /// <returns>A <see cref="string" /> containing the full name of the file or directory.</returns>
        public string FullName { get; }
        /// <summary>
        /// The name of the file or directory.
        /// </summary>
        /// <returns>A <see cref="string" /> containing the name of the file or directory.</returns>
        public string Name { get; }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public override string ToString()
        {
            return FullName;
        }

        #endregion
    }
}