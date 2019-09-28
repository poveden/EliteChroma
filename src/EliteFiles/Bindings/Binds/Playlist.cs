using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for the playlist.
    /// </summary>
    public static class Playlist
    {
#pragma warning disable 1591, SA1600
        public const string PlayPause = "GalnetAudio_Play_Pause";
        public const string SkipForward = "GalnetAudio_SkipForward";
        public const string SkipBackward = "GalnetAudio_SkipBackward";
        public const string ClearQueue = "GalnetAudio_ClearQueue";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="Playlist"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(Playlist));
    }
}
